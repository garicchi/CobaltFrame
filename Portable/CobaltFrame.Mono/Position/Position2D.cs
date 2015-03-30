using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Position
{
    public class Position2D
    {
        private Rectangle _drawRect;

        
        public Position2D(Rectangle drawRect)
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

        public bool Contains(Position2D position)
        {
            return this._drawRect.Contains(position.GetPosition());
        }

        public bool Contains(int x,int y)
        {
            return this._drawRect.Contains((float)x, (float)y);
        }

        public bool Intersects(Position2D position)
        {
            return this._drawRect.Intersects(position.GetPosition());
        }


    }
}
