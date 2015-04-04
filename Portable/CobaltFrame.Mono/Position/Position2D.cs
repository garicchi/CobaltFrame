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

        public Position2D(Position2D position)
        {
            this._drawRect = position._drawRect;
        }

        public static Position2D operator- (Position2D left, Position2D right)
        {
            return new Position2D(
                new Rectangle(
                    left.GetPosition().X-right.GetPosition().X,
                    left.GetPosition().Y-right.GetPosition().Y,
                    left.GetPosition().Width,
                    left.GetPosition().Height
                    )
                );
        }

        public static Position2D operator+ (Position2D left, Position2D right)
        {
            return new Position2D(
                new Rectangle(
                    left.GetPosition().X + right.GetPosition().X,
                    left.GetPosition().Y + right.GetPosition().Y,
                    left.GetPosition().Width,
                    left.GetPosition().Height
                    )
                );
        }

        public static Position2D operator* (Position2D left, float right)
        {
            return new Position2D(
                new Rectangle(
                    (int)(left.GetPosition().X * right),
                    (int)(left.GetPosition().Y * right),
                    left.GetPosition().Width,
                    left.GetPosition().Height
                    )
                );
        }

        public virtual void SetPosition(Rectangle newRect)
        {
            this._drawRect = newRect;
        }

        public virtual Rectangle GetPosition()
        {
            return this._drawRect;
        }

        public virtual Vector2 GetLocation()
        {
            return new Vector2(this._drawRect.X,this._drawRect.Y);
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
