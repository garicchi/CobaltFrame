using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
    public abstract class FrameContextBase
    {
        GameTime _gameTime;

        public GameTime GameTime { get { return this._gameTime; } }
        public FrameContextBase(GameTime gameTime)
        {
            this._gameTime = gameTime;
        }
    }
}
