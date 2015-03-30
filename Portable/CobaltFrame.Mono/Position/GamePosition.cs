using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Position
{
    public class GamePosition
    {
        private Rectangle _drawRect;

        
        public GamePosition(Rectangle drawRect)
        {
            this._drawRect = drawRect;
        }

        public virtual void SetPosition(Rectangle newRect)
        {
            this._drawRect = newRect;
        }

        public virtual Rectangle GetPosition()
        {
            return this._drawRect;
        }
    }
}
