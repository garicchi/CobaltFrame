using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
    public static class CobaltFrameExtension
    {
        public static Rectangle GetRect(this Rectangle rect,Vector2 origin)
        {
            rect.X += (int)origin.X;
            rect.Y += (int)origin.Y;
            return rect;
        }

        public static Point GetPosition(this Rectangle rect)
        {
            return new Point(rect.X,rect.Y);
        }

        public static Point GetCenter(this Rectangle rect)
        {
            return new Point((int)rect.Width/2,(int)rect.Height/2);
        }

    }
}
