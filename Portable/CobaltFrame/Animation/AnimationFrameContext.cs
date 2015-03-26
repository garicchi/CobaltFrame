using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
    public class AnimationFrameContext
    {
        private GameTime _gameTime;
        public AnimationFrameContext(GameTime gameTime)
        {
            this._gameTime = gameTime;
        }
    }
}
