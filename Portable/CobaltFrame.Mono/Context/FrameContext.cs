using CobaltFrame.Core.Context;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Context
{
    public class FrameContext:IFrameContext
    {
        protected GameTime _gameTime;
        public FrameContext(GameTime gameTime)
        {
            this._gameTime = gameTime;
        }


        public TimeSpan TotalGameTime
        {
            get { return this._gameTime.TotalGameTime; }
        }
    }
}
