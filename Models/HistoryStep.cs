using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MiniVisionInspector.Models
{
    internal class HistoryStep
    {
        public string Process { get; }
        public Func<Bitmap, Bitmap>? Operation { get; }
        public Bitmap? Image { get; set; }

        public HistoryStep(string process, Func<Bitmap, Bitmap>? operation)
        {
            Process = process;
            Operation = operation;
        }
    }
}
