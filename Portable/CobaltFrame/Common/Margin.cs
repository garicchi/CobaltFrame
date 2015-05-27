using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
    public struct Margin
    {
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Right { get; set; }
        public int Left { get; set; }

        public Margin(int top,int bottom,int right,int left)
            :this()
        {
            Top = top;
            Bottom = bottom;
            Right = right;
            Left = left;
        }

        

        public Margin Zero
        {
            get { return new Margin(0, 0, 0, 0); }
        }

    }
}
