using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVisionInspector.Models
{
    internal class HistoryStep
    {
        public string Process { get; }
        public Bitmap Image { get; }

        public HistoryStep(string process, Bitmap image)
        {
            Process = process;
            Image = image;
        }
    }
}
