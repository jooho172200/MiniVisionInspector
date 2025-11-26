namespace MiniVisionInspector.Forms
{
    partial class BrightnessContrast
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelBrightness = new Label();
            labelContrast = new Label();
            trackBarBrightness = new TrackBar();
            trackBarContrast = new TrackBar();
            numericBrightness = new NumericUpDown();
            numericContrast = new NumericUpDown();
            btnCancel = new Button();
            btnOk = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericBrightness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericContrast).BeginInit();
            SuspendLayout();
            // 
            // labelBrightness
            // 
            labelBrightness.AutoSize = true;
            labelBrightness.Location = new Point(82, 72);
            labelBrightness.Name = "labelBrightness";
            labelBrightness.Size = new Size(62, 15);
            labelBrightness.TabIndex = 0;
            labelBrightness.Text = "Brightness";
            // 
            // labelContrast
            // 
            labelContrast.AutoSize = true;
            labelContrast.Location = new Point(82, 145);
            labelContrast.Name = "labelContrast";
            labelContrast.Size = new Size(52, 15);
            labelContrast.TabIndex = 1;
            labelContrast.Text = "Contrast";
            // 
            // trackBarBrightness
            // 
            trackBarBrightness.Location = new Point(150, 72);
            trackBarBrightness.Maximum = 100;
            trackBarBrightness.Minimum = -100;
            trackBarBrightness.Name = "trackBarBrightness";
            trackBarBrightness.Size = new Size(289, 45);
            trackBarBrightness.TabIndex = 2;
            trackBarBrightness.Scroll += trackBarBrightness_Scroll;
            // 
            // trackBarContrast
            // 
            trackBarContrast.Location = new Point(150, 145);
            trackBarContrast.Maximum = 100;
            trackBarContrast.Minimum = -100;
            trackBarContrast.Name = "trackBarContrast";
            trackBarContrast.Size = new Size(289, 45);
            trackBarContrast.TabIndex = 3;
            trackBarContrast.Scroll += trackBarContrast_Scroll;
            // 
            // numericBrightness
            // 
            numericBrightness.Location = new Point(445, 72);
            numericBrightness.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            numericBrightness.Name = "numericBrightness";
            numericBrightness.Size = new Size(65, 23);
            numericBrightness.TabIndex = 4;
            numericBrightness.ValueChanged += numericBrightness_ValueChanged;
            // 
            // numericContrast
            // 
            numericContrast.Location = new Point(445, 145);
            numericContrast.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            numericContrast.Name = "numericContrast";
            numericContrast.Size = new Size(65, 23);
            numericContrast.TabIndex = 5;
            numericContrast.ValueChanged += numericContrast_ValueChanged;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(302, 196);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(79, 27);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(197, 196);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(79, 27);
            btnOk.TabIndex = 6;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // BrightnessContrast
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(584, 261);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(numericContrast);
            Controls.Add(numericBrightness);
            Controls.Add(trackBarContrast);
            Controls.Add(trackBarBrightness);
            Controls.Add(labelContrast);
            Controls.Add(labelBrightness);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BrightnessContrast";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Brightness/Contrast";
            ((System.ComponentModel.ISupportInitialize)trackBarBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarContrast).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericBrightness).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericContrast).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelBrightness;
        private Label labelContrast;
        private TrackBar trackBarBrightness;
        private TrackBar trackBarContrast;
        private NumericUpDown numericBrightness;
        private NumericUpDown numericContrast;
        private Button btnCancel;
        private Button btnOk;
    }
}