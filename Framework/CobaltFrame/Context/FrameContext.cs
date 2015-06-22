using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Context
{
    public class FrameContext
    {
        
        public FrameContext()
        {
            this.CameraTrans = Matrix.Identity;
        }
        public GameTime GameTime { get; set; }
        public Matrix ScreenScale { get; set; }

        public Matrix ScreenMargin { get; set; }

        public Matrix CameraTrans { get; set; }

        public Matrix GetScreenTrans()
        {
            return ScreenScale * ScreenMargin * CameraTrans;
        }

        public Matrix GetScreenTransInvert()
        {
            return Matrix.Invert(GetScreenTrans());
        }

        public TimeSpan TotalGameTime
        {
            get { return this.GameTime.TotalGameTime; }
        }

        public TimeSpan ElapsedGameTime
        {
            get { return this.GameTime.ElapsedGameTime; }
        }

        public TimeSpan ElapsedScreenTime { get; set; }
    }
}
