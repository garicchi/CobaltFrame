using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Common
{
    public class Margin
    {
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Right { get; set; }
        public int Left { get; set; }

        public Margin(int top,int bottom,int right,int left)
        {
            this.Top = top;
            this.Bottom = bottom;
            this.Right = right;
            this.Left = left;
        }

        public Margin()
        {
            this.Top = 0;
            this.Bottom = 0;
            this.Right = 0;
            this.Left = 0;
        }

    }
}
