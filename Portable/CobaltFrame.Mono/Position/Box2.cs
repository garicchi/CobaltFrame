using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Position
{
    public class Box2
    {
        private Rectangle _drawRect;

        public Box2(int x,int y,int width,int height)
        {
            this._drawRect = new Rectangle(x,y,width,height);
        }
        public Box2(Rectangle drawRect)
        {
            this._drawRect = drawRect;
        }

        public Box2(Box2 box)
        {
            this._drawRect = box._drawRect;
        }

        

        public virtual void SetRect(Rectangle newRect)
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

        public virtual Rectangle GetRect()
        {
            return this._drawRect;
        }

        public virtual Rectangle GetRect(Vector2 origin)
        {
            return new Rectangle(this.GetRect().X + (int)origin.X, this.GetRect().Y + (int)origin.Y, this.GetRect().Width, this.GetRect().Height);
        }

        public virtual Vector2 GetLocation()
        {
            return new Vector2(this._drawRect.X,this._drawRect.Y);
        }

        public virtual bool Contains(Box2 position)
        {
            return this.GetRect().Contains(position.GetRect());
        }

        public virtual bool Contains(int x,int y)
        {
            return this.GetRect().Contains((float)x, (float)y);
        }

        public virtual bool Intersects(Box2 position)
        {
            return this.GetRect().Intersects(position.GetRect());
        }

        public static Box2 operator -(Box2 left, Box2 right)
        {
            return new Box2(
                new Rectangle(
                    left.GetRect().X - right.GetRect().X,
                    left.GetRect().Y - right.GetRect().Y,
                    left.GetRect().Width,
                    left.GetRect().Height
                    )
                );
        }

        public static Box2 operator +(Box2 left, Box2 right)
        {
            return new Box2(
                new Rectangle(
                    left.GetRect().X + right.GetRect().X,
                    left.GetRect().Y + right.GetRect().Y,
                    left.GetRect().Width,
                    left.GetRect().Height
                    )
                );
        }



        public static Box2 operator *(Box2 left, float right)
        {
            return new Box2(
                new Rectangle(
                    (int)(left.GetRect().X * right),
                    (int)(left.GetRect().Y * right),
                    left.GetRect().Width,
                    left.GetRect().Height
                    )
                );
        }
    }
}
