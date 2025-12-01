using MiniVisionInspector.Services;
using MiniVisionInspector.Forms;
using MiniVisionInspector.Models;
using System.IO;
using System.Drawing.Imaging;

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

        //private readonly Stack<Bitmap> _history = new Stack<Bitmap>();
        private readonly List<HistoryStep> _historySteps = new();
        private int _currentHistoryIndex = -1;

        // true  : 원본 이미지 표시
        // false : 현재(_currentImage) 이미지 표시
        private bool _showOriginal = false;

        public MainForm()
        {
            InitializeComponent();
            toolStripStatusLabelInfo.Text = "이미지를 Open 버튼으로 불러오세요.";
        }

        private void ApplyNewImage(Bitmap newImage, string process)
        {
            if (_currentHistoryIndex < _historySteps.Count - 1)
            {
                for (int i = _historySteps.Count - 1; i > _currentHistoryIndex; i--)
                {
                    _historySteps[i].Image.Dispose();
                    _historySteps.RemoveAt(i);
                }
            }

            var step = new HistoryStep(process, newImage);
            _historySteps.Add(step);
            _currentHistoryIndex = _historySteps.Count - 1;

            _currentImage = step.Image;
            _showOriginal = false;

            RefreshImage();
            UpdateHistoryListBox();
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

        //리스트박스 클릭 시 해당 단계로 이동
        private void listBoxHistory_SelectedChanged(object sender, EventArgs e)
        {
            int idx = listBoxHistory.SelectedIndex;

            if (idx < 0 || idx >= _historySteps.Count) return;

            _currentHistoryIndex = idx;
            _currentImage = _historySteps[idx].Image;
            _showOriginal = false;

            RefreshImage();
            toolStripStatusLabelInfo.Text = $"line[{_historySteps[idx].Process}]";
        }

        //// 새 이미지 열 때, 히스토리 초기화
        private void ClearHistory()
        {
            foreach (var step in _historySteps)
            {
                step.Image.Dispose();
            }

            _historySteps.Clear();
            _currentHistoryIndex = -1;
            listBoxHistory.Items.Clear();
        }

        /// <summary>
        /// 현재 상태(_showOriginal)에 맞게 pictureBoxMain에 이미지 표시
        /// </summary>
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

                    ClearHistory();

                    var firstImage = (Bitmap)_originalImage.Clone();
                    var step = new HistoryStep("Source Image", firstImage);
                    _historySteps.Add(step);
                    _currentHistoryIndex = 0;

                    _currentImage = step.Image;
                    _showOriginal = false;

                    RefreshImage();
                    UpdateHistoryListBox();

                    toolStripStatusLabelInfo.Text = $"이미지 로드: {ofd.FileName}";
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (_originalImage is null || _historySteps.Count == 0)
            {
                toolStripStatusLabelInfo.Text = "원본 이미지가 없습니다";
                return;
            }

            ClearHistory();

            var firstImage = (Bitmap)_originalImage.Clone();
            var step = new HistoryStep("Source Image", firstImage);
            _historySteps.Add(step);

            _currentHistoryIndex = 0;
            _currentImage = step.Image;
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

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.ToGrayScale(src);

            ApplyNewImage(processed, "GrayScale");
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

                var src = _currentImage ?? _originalImage;
                var processed = ImageProcessor.Threshold(src, th, invert);

                ApplyNewImage(processed, "Binary");
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

                var src = _currentImage ?? _originalImage;
                var processed = ImageProcessor.AdjustBrightnessContrast(src, b, c);

                ApplyNewImage(processed, $"Brightness = {b}/Contrast = {c}");
                toolStripStatusLabelInfo.Text = $"밝기={b}, 대비={c} 적용완료";
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_currentHistoryIndex <= 0)
            {
                toolStripStatusLabelInfo.Text = "되돌릴 작업이 없습니다";
                return;
            }

            var stepToRemove = _historySteps[_currentHistoryIndex];
            stepToRemove.Image.Dispose();
            _historySteps.RemoveAt(_currentHistoryIndex);

            _currentHistoryIndex--;
            _currentImage = _historySteps[_currentHistoryIndex].Image;
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

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.Blur(src);

            ApplyNewImage(processed, "Blur");
            toolStripStatusLabelInfo.Text = "3x3  평균 블러 적용 완료";
        }

        private void btnSharpen_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.Sharpen(src);

            ApplyNewImage(processed, "Sharpening");
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

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.Canny(src, 100, 200);

            ApplyNewImage(processed, "Canny");
            toolStripStatusLabelInfo.Text = "Canny 엣지 검출 완료";
        }

        private void btnErode_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.Erode(src);

            ApplyNewImage(processed, "Erode");
            toolStripStatusLabelInfo.Text = "Erode 적용 완료";
        }

        private void btnDilate_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.Dilate(src, 3);

            ApplyNewImage(processed, "Dilate");
            toolStripStatusLabelInfo.Text = "Dilate 적용 완료";
        }

        private void btnOpenMorph_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.OpenMorph(src, 3);

            ApplyNewImage(processed, "OpenMorph");
            toolStripStatusLabelInfo.Text = "Open 모폴로지 적용 완료";
        }

        private void btnCloseMorph_Click(object sender, EventArgs e)
        {
            if (_currentImage is null && _originalImage is null)
            {
                toolStripStatusLabelInfo.Text = "이미지를 먼저 열어주세요";
                return;
            }

            var src = _currentImage ?? _originalImage;
            var processed = ImageProcessor.CloseMorph(src, 3);

            ApplyNewImage(processed, "CloseMorph");
            toolStripStatusLabelInfo.Text = "Close 모폴로지 적용 완료";
        }
    }
}
