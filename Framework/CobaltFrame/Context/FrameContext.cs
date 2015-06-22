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
        private GameTime _gameTime;

        
        public FrameContext(GameTime gameTime)
        {
            this._gameTime = gameTime;
            
        }

        public Matrix ScreenScale { get; set; }

        public Matrix ScreenMargin { get; set; }

        public Matrix ScreenTrans
        {
            get
            {
                return ScreenScale * ScreenMargin;
            }
        }

        public Matrix GetScreenTrans()
        {
            return ScreenScale * ScreenMargin;
        }

        public Matrix GetScreenTransInvert()
        {
            return Matrix.Invert(ScreenScale * ScreenMargin);
        }

        public TimeSpan TotalGameTime
        {
            get { return this._gameTime.TotalGameTime; }
        }

        public TimeSpan ElapsedGameTime
        {
            get { return this._gameTime.ElapsedGameTime; }
        }

        public TimeSpan ElapsedScreenTime { get; set; }
    }
}
