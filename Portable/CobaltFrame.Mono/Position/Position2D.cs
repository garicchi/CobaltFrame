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

        public Position2D(int x,int y,int width,int height)
        {
            this._drawRect = new Rectangle(x,y,width,height);
        }
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

        public virtual void SetLocation(Vector2 newPos)
        {
            this._drawRect = new Rectangle(
                (int)newPos.X,
                (int)newPos.Y,
                this._drawRect.Width,
                this._drawRect.Height
                );
        }

        public virtual Rectangle GetPosition()
        {
            return this._drawRect;
        }

        public virtual Rectangle GetPosition(Vector2 origin)
        {
            return new Rectangle(this.GetPosition().X + (int)origin.X, this.GetPosition().Y + (int)origin.Y, this.GetPosition().Width, this.GetPosition().Height);
        }

        public virtual Vector2 GetLocation()
        {
            return new Vector2(this._drawRect.X,this._drawRect.Y);
        }

        public virtual bool Contains(Position2D position)
        {
            return this.GetPosition().Contains(position.GetPosition());
        }

        public virtual bool Contains(int x,int y)
        {
            return this.GetPosition().Contains((float)x, (float)y);
        }

        public virtual bool Intersects(Position2D position)
        {
            return this.GetPosition().Intersects(position.GetPosition());
        }


    }
}
