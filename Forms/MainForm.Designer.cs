namespace MiniVisionInspector
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnCloseMorph = new Button();
            btnOpenMorph = new Button();
            btnDilate = new Button();
            btnErode = new Button();
            btnCanny = new Button();
            btnSharpen = new Button();
            btnBlur = new Button();
            btnUndo = new Button();
            btnBrightnessContrast = new Button();
            btnSave = new Button();
            btnThresh = new Button();
            btnGray = new Button();
            btnReset = new Button();
            btnOpen = new Button();
            pictureBoxMain = new PictureBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelInfo = new ToolStripStatusLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            splitMain = new SplitContainer();
            panelHistory = new Panel();
            listBoxHistory = new ListBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            panelHistory.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(224, 224, 224);
            panel1.Controls.Add(btnCloseMorph);
            panel1.Controls.Add(btnOpenMorph);
            panel1.Controls.Add(btnDilate);
            panel1.Controls.Add(btnErode);
            panel1.Controls.Add(btnCanny);
            panel1.Controls.Add(btnSharpen);
            panel1.Controls.Add(btnBlur);
            panel1.Controls.Add(btnUndo);
            panel1.Controls.Add(btnBrightnessContrast);
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(btnThresh);
            panel1.Controls.Add(btnGray);
            panel1.Controls.Add(btnReset);
            panel1.Controls.Add(btnOpen);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1188, 64);
            panel1.TabIndex = 0;
            // 
            // btnCloseMorph
            // 
            btnCloseMorph.BackColor = Color.FromArgb(128, 255, 128);
            btnCloseMorph.ImageAlign = ContentAlignment.MiddleLeft;
            btnCloseMorph.Location = new Point(708, 34);
            btnCloseMorph.Name = "btnCloseMorph";
            btnCloseMorph.Size = new Size(84, 24);
            btnCloseMorph.TabIndex = 13;
            btnCloseMorph.Text = "CloseMorph";
            btnCloseMorph.UseVisualStyleBackColor = false;
            btnCloseMorph.Click += btnCloseMorph_Click;
            // 
            // btnOpenMorph
            // 
            btnOpenMorph.BackColor = Color.FromArgb(128, 255, 128);
            btnOpenMorph.ImageAlign = ContentAlignment.MiddleLeft;
            btnOpenMorph.Location = new Point(618, 35);
            btnOpenMorph.Name = "btnOpenMorph";
            btnOpenMorph.Size = new Size(84, 24);
            btnOpenMorph.TabIndex = 12;
            btnOpenMorph.Text = "OpenMorph";
            btnOpenMorph.UseVisualStyleBackColor = false;
            btnOpenMorph.Click += btnOpenMorph_Click;
            // 
            // btnDilate
            // 
            btnDilate.BackColor = Color.FromArgb(128, 255, 128);
            btnDilate.ImageAlign = ContentAlignment.MiddleLeft;
            btnDilate.Location = new Point(537, 35);
            btnDilate.Name = "btnDilate";
            btnDilate.Size = new Size(75, 23);
            btnDilate.TabIndex = 11;
            btnDilate.Text = "Dilate";
            btnDilate.UseVisualStyleBackColor = false;
            btnDilate.Click += btnDilate_Click;
            // 
            // btnErode
            // 
            btnErode.BackColor = Color.FromArgb(128, 255, 128);
            btnErode.ImageAlign = ContentAlignment.MiddleLeft;
            btnErode.Location = new Point(456, 35);
            btnErode.Name = "btnErode";
            btnErode.Size = new Size(75, 23);
            btnErode.TabIndex = 10;
            btnErode.Text = "Erode";
            btnErode.UseVisualStyleBackColor = false;
            btnErode.Click += btnErode_Click;
            // 
            // btnCanny
            // 
            btnCanny.BackColor = Color.FromArgb(128, 255, 128);
            btnCanny.ImageAlign = ContentAlignment.MiddleLeft;
            btnCanny.Location = new Point(375, 35);
            btnCanny.Name = "btnCanny";
            btnCanny.Size = new Size(75, 23);
            btnCanny.TabIndex = 9;
            btnCanny.Text = "Canny";
            btnCanny.UseVisualStyleBackColor = false;
            btnCanny.Click += btnCanny_Click;
            // 
            // btnSharpen
            // 
            btnSharpen.Location = new Point(747, 6);
            btnSharpen.Name = "btnSharpen";
            btnSharpen.Size = new Size(75, 23);
            btnSharpen.TabIndex = 8;
            btnSharpen.Text = "Sharpen";
            btnSharpen.UseVisualStyleBackColor = true;
            btnSharpen.Click += btnSharpen_Click;
            // 
            // btnBlur
            // 
            btnBlur.Location = new Point(666, 6);
            btnBlur.Name = "btnBlur";
            btnBlur.Size = new Size(75, 23);
            btnBlur.TabIndex = 7;
            btnBlur.Text = "Blur";
            btnBlur.UseVisualStyleBackColor = true;
            btnBlur.Click += btnBlur_Click;
            // 
            // btnUndo
            // 
            btnUndo.Location = new Point(174, 6);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(75, 23);
            btnUndo.TabIndex = 6;
            btnUndo.Text = "Undo";
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // btnBrightnessContrast
            // 
            btnBrightnessContrast.Location = new Point(537, 6);
            btnBrightnessContrast.Name = "btnBrightnessContrast";
            btnBrightnessContrast.Size = new Size(123, 23);
            btnBrightnessContrast.TabIndex = 5;
            btnBrightnessContrast.Text = "Brightness/Contrast";
            btnBrightnessContrast.UseVisualStyleBackColor = true;
            btnBrightnessContrast.Click += btnBrightnessContrast_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(93, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnThresh
            // 
            btnThresh.Location = new Point(456, 6);
            btnThresh.Name = "btnThresh";
            btnThresh.Size = new Size(75, 23);
            btnThresh.TabIndex = 3;
            btnThresh.Text = "Binary";
            btnThresh.UseVisualStyleBackColor = true;
            btnThresh.Click += btnThresh_Click;
            // 
            // btnGray
            // 
            btnGray.Location = new Point(375, 6);
            btnGray.Name = "btnGray";
            btnGray.Size = new Size(75, 23);
            btnGray.TabIndex = 2;
            btnGray.Text = "Gray";
            btnGray.UseVisualStyleBackColor = true;
            btnGray.Click += btnGray_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(255, 6);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(75, 23);
            btnReset.TabIndex = 1;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // btnOpen
            // 
            btnOpen.Location = new Point(12, 6);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(75, 23);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "Open\r\n";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // pictureBoxMain
            // 
            pictureBoxMain.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxMain.Dock = DockStyle.Fill;
            pictureBoxMain.Location = new Point(3, 3);
            pictureBoxMain.Name = "pictureBoxMain";
            pictureBoxMain.Size = new Size(938, 610);
            pictureBoxMain.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxMain.TabIndex = 1;
            pictureBoxMain.TabStop = false;
            pictureBoxMain.MouseMove += pictureBoxMain_MouseMove;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(224, 224, 224);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelInfo });
            statusStrip1.Location = new Point(0, 680);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1188, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelInfo
            // 
            toolStripStatusLabelInfo.Name = "toolStripStatusLabelInfo";
            toolStripStatusLabelInfo.Size = new Size(135, 17);
            toolStripStatusLabelInfo.Text = "toolStripStatusLabelInfo";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(pictureBoxMain, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(944, 616);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 64);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(panelHistory);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(tableLayoutPanel1);
            splitMain.Size = new Size(1188, 616);
            splitMain.SplitterDistance = 240;
            splitMain.TabIndex = 6;
            // 
            // panelHistory
            // 
            panelHistory.BorderStyle = BorderStyle.FixedSingle;
            panelHistory.Controls.Add(listBoxHistory);
            panelHistory.Dock = DockStyle.Fill;
            panelHistory.Location = new Point(0, 0);
            panelHistory.Name = "panelHistory";
            panelHistory.Size = new Size(240, 616);
            panelHistory.TabIndex = 0;
            // 
            // listBoxHistory
            // 
            listBoxHistory.FormattingEnabled = true;
            listBoxHistory.Location = new Point(32, 25);
            listBoxHistory.Name = "listBoxHistory";
            listBoxHistory.Size = new Size(172, 529);
            listBoxHistory.TabIndex = 0;
            listBoxHistory.SelectedIndexChanged += listBoxHistory_SelectedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1188, 702);
            Controls.Add(splitMain);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Name = "MainForm";
            Text = "Mini Vision Inspector";
            KeyDown += MainForm_KeyDown;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            panelHistory.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnReset;
        private Button btnOpen;
        private Button btnThresh;
        private Button btnGray;
        private PictureBox pictureBoxMain;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelInfo;
        private Button btnSave;
        private Button btnBrightnessContrast;
        private Button btnUndo;
        private Button btnBlur;
        private Button btnSharpen;
        private Button btnCanny;
        private TableLayoutPanel tableLayoutPanel1;
        private SplitContainer splitMain;
        private Panel panelHistory;
        private Button btnDilate;
        private Button btnErode;
        private Button btnCloseMorph;
        private Button btnOpenMorph;
        private ListBox listBoxHistory;
    }
}
