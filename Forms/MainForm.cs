using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using MiniVisionInspector.Forms;
using MiniVisionInspector.Models;
using MiniVisionInspector.Services;

namespace MiniVisionInspector
{
    public partial class MainForm : Form
    {
        private Bitmap _originalImage;
        private Bitmap _currentImage;

        private int _lastThreshold = 127;
        private bool _lastInvert = false;

        private int _lastBrightness = 0;
        private int _lastContrast = 0;

        private readonly List<HistoryStep> _historySteps = new();
        private int _currentHistoryIndex = -1;
                
        private bool _showOriginal = false;

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            toolStripStatusLabelInfo.Text = "이미지를 Open 버튼으로 불러오세요.";
        }
                
        /// 히스토리에 새 스텝 추가(연산 단위로 기록)
        private void ApplyNewStep(string process, Func<Bitmap, Bitmap> operation)
        {
            if (_originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "원본 이미지가 없습니다";
                return;
            }

            // 히스토리가 비어있으면 Source 스텝 생성
            if (_historySteps.Count == 0)
            {
                var sourceStep = new HistoryStep("Source Image", null);
                _historySteps.Add(sourceStep);
                _currentHistoryIndex = 0;
            }

            // 현재 단계 바로 뒤에 삽입 (중간 삽입 가능)
            int insertIndex = (_currentHistoryIndex < 0)
                ? _historySteps.Count
                : _currentHistoryIndex + 1;

            var step = new HistoryStep(process, operation);

            if (insertIndex >= _historySteps.Count)
                _historySteps.Add(step);
            else
                _historySteps.Insert(insertIndex, step);

            _currentHistoryIndex = insertIndex;

            RebuildHistoryImages();

            _showOriginal = false;
            RefreshImage();
            UpdateHistoryListBox();
        }


        /// 원본 이미지와 HistoryStep.Operation들을 이용해서
        /// 0번 스텝부터 순서대로 이미지 다시 계산
        private void RebuildHistoryImages()
        {
            if (_originalImage is null || _historySteps.Count == 0)
                return;

            // 0번 스텝: 항상 Source Image
            var first = _historySteps[0];
            first.Image?.Dispose();
            first.Image = (Bitmap)_originalImage.Clone();

            Bitmap current = first.Image;

            for (int i = 1; i < _historySteps.Count; i++)
            {
                var step = _historySteps[i];

                step.Image?.Dispose();

                if (step.Operation is null)
                {
                    step.Image = (Bitmap)current.Clone();
                }
                else
                {
                    Bitmap src = (Bitmap)current.Clone();
                    Bitmap result = step.Operation(src);

                    if (!ReferenceEquals(result, src))
                        src.Dispose();

                    step.Image = result;
                }

                current = step.Image;
            }

            if (_currentHistoryIndex < 0 || _currentHistoryIndex >= _historySteps.Count)
                _currentHistoryIndex = _historySteps.Count - 1;

            _currentImage = _historySteps[_currentHistoryIndex].Image;
        }

        private void UpdateHistoryListBox()
        {
            listBoxHistory.Items.Clear();

            for (int i = 0; i < _historySteps.Count; i++)
            {
                listBoxHistory.Items.Add($"{i + 1}. {_historySteps[i].Process}");
            }

            UpdateHistoryListSelection();
        }

        private void UpdateHistoryListSelection()
        {
            if (_currentHistoryIndex >= 0 && _currentHistoryIndex < listBoxHistory.Items.Count)
            {
                listBoxHistory.SelectedIndex = _currentHistoryIndex;
                listBoxHistory.TopIndex = _currentHistoryIndex;
            }
        }

        // 리스트박스 클릭 시 해당 단계로 이동
        private void listBoxHistory_SelectedValueChanged(object sender, EventArgs e)
        {
            int idx = listBoxHistory.SelectedIndex;

            if (idx < 0 || idx >= _historySteps.Count) return;

            _currentHistoryIndex = idx;
            _currentImage = _historySteps[idx].Image;
            _showOriginal = false;

            RefreshImage();
            toolStripStatusLabelInfo.Text = $"line[{_historySteps[idx].Process}]";
        }

        // 새 이미지 열 때, 히스토리 초기화
        private void ClearHistory()
        {
            foreach (var step in _historySteps)
            {
                step.Image?.Dispose();
            }

            _historySteps.Clear();
            _currentHistoryIndex = -1;
            listBoxHistory.Items.Clear();

            _currentImage = null;
            pictureBoxMain.Image = null;
        }

        /// 현재 상태(_showOriginal)에 맞게 pictureBoxMain에 이미지 표시
        private void RefreshImage()
        {
            if (_originalImage is null)
            {
                pictureBoxMain.Image = null;
                return;
            }

            if (_showOriginal)
            {
                pictureBoxMain.Image = _originalImage;
            }
            else
            {
                // 아직 가공된 이미지가 없다면 원본을 대신 보여줌
                pictureBoxMain.Image = _currentImage ?? _originalImage;
            }
        }

        private void ToggleOriginalView()
        {
            if (_originalImage is null) return;

            _showOriginal = !_showOriginal;
            RefreshImage();

            toolStripStatusLabelInfo.Text = _showOriginal ? "원본 이미지" : "처리 이미지";
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oem3 || e.KeyCode == Keys.Oemtilde)
            {
                ToggleOriginalView();
                e.Handled = true;
            }
        }

        ////////////////////////////////////////////버튼 구현///////////////////////////////////////////////////////////////

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp|All Files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _originalImage?.Dispose();
                    _currentImage?.Dispose();

                    _originalImage = new Bitmap(ofd.FileName);
                    _currentImage = null;

                    ClearHistory();

                    // 0번 스텝: Source Image (연산 없음)
                    var step = new HistoryStep("Source Image", null);
                    _historySteps.Add(step);
                    _currentHistoryIndex = 0;

                    RebuildHistoryImages();

                    _showOriginal = false;
                    RefreshImage();
                    UpdateHistoryListBox();

                    toolStripStatusLabelInfo.Text = $"이미지 로드: {ofd.FileName}";
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "원본 이미지가 없습니다";
                return;
            }

            ClearHistory();

            var step = new HistoryStep("Source Image", null);
            _historySteps.Add(step);
            _currentHistoryIndex = 0;

            RebuildHistoryImages();

            _showOriginal = false;
            RefreshImage();
            UpdateHistoryListBox();

            toolStripStatusLabelInfo.Text = "변경 사항 폐기 완료";
        }

        private void btnGray_Click(object sender, EventArgs e)
        {
            if (_originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "원본 이미지가 없습니다";
                return;
            }

            ApplyNewStep("GrayScale", img => ImageProcessor.ToGrayScale(img));
            toolStripStatusLabelInfo.Text = "그레이스케일 변환 완료";
        }

        private void btnThresh_Click(object sender, EventArgs e)
        {
            if (_originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "원본 이미지가 없습니다";
                return;
            }

            using (var dlg = new Threshold(_lastThreshold))
            {
                var result = dlg.ShowDialog(this);

                if (result != DialogResult.OK)
                {
                    toolStripStatusLabelInfo.Text = "이진화 취소";
                    return;
                }

                int th = dlg.SelectedThreshold;
                bool invert = dlg.BitInvert;

                _lastThreshold = th;
                _lastInvert = invert;

                ApplyNewStep("Binary", img => ImageProcessor.Threshold(img, th, invert));
                toolStripStatusLabelInfo.Text = $"이진화(threshold = {th}) 완료";
            }
        }

        // 사진창 위에서 마우스 움직일 때 좌표와 RGB 값 표시용
        private void pictureBoxMain_MouseMove(object sender, MouseEventArgs e)
        {
            // 현재 보여주고 있는 이미지 선택
            var img = _showOriginal ? _originalImage : _currentImage;

            if (img == null || pictureBoxMain.Image == null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요.";
                return;
            }

            // 사진창과 이미지의 비율 계산
            float imgAspect = (float)img.Width / img.Height;
            float boxAspect = (float)pictureBoxMain.Width / pictureBoxMain.Height;

            int drawWidth, drawHeight;
            int offsetX, offsetY;

            if (imgAspect > boxAspect)  // 가로가 더 긴 경우
            {
                drawWidth = pictureBoxMain.Width;
                drawHeight = (int)(pictureBoxMain.Width / imgAspect);
                offsetX = 0;
                offsetY = (pictureBoxMain.Height - drawHeight) / 2;
            }
            else
            {
                drawHeight = pictureBoxMain.Height;
                drawWidth = (int)(pictureBoxMain.Height * imgAspect);
                offsetY = 0;
                offsetX = (pictureBoxMain.Width - drawWidth) / 2;
            }

            // 마우스 좌표 -> 이미지 좌표 변환
            int x = (int)((e.X - offsetX) * (float)img.Width / drawWidth);
            int y = (int)((e.Y - offsetY) * (float)img.Height / drawHeight);

            // 이미지 영역 밖인 경우
            if (x < 0 || y < 0 || x >= img.Width || y >= img.Height)
            {
                return;
            }

            Color c = img.GetPixel(x, y);
            toolStripStatusLabelInfo.Text = $"X={x}, Y={y}       R={c.R}, G={c.G}, B={c.B}";
        }

        // 결과 이미지 저장
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_currentImage is null)
            {
                toolStripStatusLabelInfo.Text = "저장할 이미지 없음";
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "이미지 저장";
                sfd.Filter = "PNG 이미지|*.png|JPEG 이미지|*.jpg;*.jpeg|BMP 이미지|*.bmp|모든 파일|*.*";
                sfd.FileName = "result.png";

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                        ImageFormat format;

                        switch (ext)
                        {
                            case ".jpg":
                            case ".jpeg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                            case ".png":
                            default:
                                format = ImageFormat.Png;
                                break;
                        }

                        _currentImage.Save(sfd.FileName, format);
                        toolStripStatusLabelInfo.Text = $"이미지 저장 완료: {sfd.FileName}";
                    }
                    catch (Exception)
                    {
                        toolStripStatusLabelInfo.Text = "이미지 저장 중 오류 발생";
                    }
                }
                else
                {
                    toolStripStatusLabelInfo.Text = "이미지 저장 취소";
                }
            }
        }

        private void btnBrightnessContrast_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요.";
                return;
            }

            using (var dlg = new BrightnessContrast(_lastBrightness, _lastContrast))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK)
                {
                    toolStripStatusLabelInfo.Text = "밝기/대비 조정 취소";
                    return;
                }

                int b = dlg.Brightness;
                int c = dlg.Contrast;

                _lastBrightness = b;
                _lastContrast = c;

                ApplyNewStep($"Brightness = {b}/Contrast = {c}",
                    img => ImageProcessor.AdjustBrightnessContrast(img, b, c));

                toolStripStatusLabelInfo.Text = $"밝기={b}, 대비={c} 적용완료";
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            // 0번(Source Image)은 지우면 안 됨
            if (_historySteps.Count <= 1 || _currentHistoryIndex <= 0)
            {
                toolStripStatusLabelInfo.Text = "되돌릴 작업이 없습니다";
                return;
            }

            _historySteps[_currentHistoryIndex].Image?.Dispose();
            _historySteps.RemoveAt(_currentHistoryIndex);

            _currentHistoryIndex--;

            RebuildHistoryImages();
            _showOriginal = false;
            RefreshImage();
            UpdateHistoryListBox();

            toolStripStatusLabelInfo.Text = "Undo 완료";
        }

        private void btnBlur_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요.";
                return;
            }

            ApplyNewStep("Blur", img => ImageProcessor.Blur(img));
            toolStripStatusLabelInfo.Text = "3x3  평균 블러 적용 완료";
        }

        private void btnSharpen_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            ApplyNewStep("Sharpening", img => ImageProcessor.Sharpen(img));
            toolStripStatusLabelInfo.Text = "샤프닝 필터 적용 완료";
        }

        // OpenCV
        private void btnCanny_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            ApplyNewStep("Canny", img => ImageProcessor.Canny(img, 100, 200));
            toolStripStatusLabelInfo.Text = "Canny 엣지 검출 완료";
        }

        private void btnErode_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            ApplyNewStep("Erode", img => ImageProcessor.Erode(img));
            toolStripStatusLabelInfo.Text = "Erode 적용 완료";
        }

        private void btnDilate_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            ApplyNewStep("Dilate", img => ImageProcessor.Dilate(img, 3));
            toolStripStatusLabelInfo.Text = "Dilate 적용 완료";
        }

        private void btnOpenMorph_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            ApplyNewStep("OpenMorph", img => ImageProcessor.OpenMorph(img, 3));
            toolStripStatusLabelInfo.Text = "Open 모폴로지 적용 완료";
        }

        private void btnCloseMorph_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            ApplyNewStep("CloseMorph", img => ImageProcessor.CloseMorph(img, 3));
            toolStripStatusLabelInfo.Text = "Close 모폴로지 적용 완료";
        }
    }
}
