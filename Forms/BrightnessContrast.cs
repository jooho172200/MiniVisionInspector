using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniVisionInspector.Forms
{
    public partial class BrightnessContrast : Form
    {
        public int Brightness
        {
            get => (int)numericBrightness.Value;
            set
            {
                if (value < numericBrightness.Minimum) value = (int)numericBrightness.Minimum;
                if (value > numericBrightness.Maximum) value = (int)numericBrightness.Maximum;

                numericBrightness.Value = value;
                trackBarBrightness.Value = value;
            }
        }

        public int Contrast
        {
            get => (int)numericContrast.Value;
            set
            {
                if (value < numericContrast.Minimum) value = (int)numericContrast.Minimum;
                if (value > numericContrast.Maximum) value = (int)numericContrast.Maximum;

                numericContrast.Value = value;
                trackBarContrast.Value = value;
            }
        }

        public BrightnessContrast(int initialBrightness, int initialContrast)
        {
            InitializeComponent();
            Brightness = initialBrightness;
            Contrast = initialContrast;
        }

        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            numericBrightness.Value = trackBarBrightness.Value;
        }

        private void numericBrightness_ValueChanged(object sender, EventArgs e)
        {
            trackBarBrightness.Value = (int)numericBrightness.Value;
        }

        private void trackBarContrast_Scroll(object sender, EventArgs e)
        {
            numericContrast.Value = trackBarContrast.Value;
        }

        private void numericContrast_ValueChanged(object sender, EventArgs e)
        {
            trackBarContrast.Value = (int)numericContrast.Value;
        }
    }
}