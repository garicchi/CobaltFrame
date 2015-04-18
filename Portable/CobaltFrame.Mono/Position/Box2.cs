using CobaltFrame.Core.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Position
{
    public class Box2:IBox2
    {
        private Rectangle _rect;
        public Box2(int x, int y, int w, int h)
        {
            this._rect = new Rectangle(x,y,w,h);
        }

        public Box2(Box2 box)
        {
            this._rect = box.GetRect();
        }

        public Box2(Rectangle rect)
        {
            this._rect = rect;
        }

        public void SetRect(Microsoft.Xna.Framework.Rectangle rect)
        {
            this._rect = rect;
        }

        public Microsoft.Xna.Framework.Rectangle GetRect()
        {
            return this._rect;
        }

        public void SetLocation(Microsoft.Xna.Framework.Vector2 vec)
        {
            this._rect.X = (int)vec.X;
            this._rect.Y = (int)vec.Y;
        }

        public Microsoft.Xna.Framework.Vector2 GetLocation()
        {
            return new Vector2(this._rect.X,this._rect.Y);
        }

        public Microsoft.Xna.Framework.Vector2 GetCenter()
        {
            return new Vector2(this._rect.X + this._rect.Width / 2, this._rect.Y + this._rect.Height / 2);
        }

        public void SetCenter(Microsoft.Xna.Framework.Vector2 vec)
        {
            this._rect.X = (int)vec.X - this._rect.Width;
            this._rect.Y = (int)vec.Y - this._rect.Height;
        }

        public void MoveRect(int up = 0, int left = 0, int down = 0, int right = 0)
        {
            this.SetLocation(new Vector2(this._rect.X + right - left, this._rect.Y + down - up));
        }

        public IBox2 TryMoveRect(int up = 0, int left = 0, int down = 0, int right = 0)
        {
            var box = new Box2(this);
            box.MoveRect(up,left,down,right);
            return box;
        }

        public bool Contains(int x, int y)
        {
            return this._rect.Contains(x,y);
        }

        public bool Contains(Microsoft.Xna.Framework.Vector2 vec)
        {
            return this._rect.Contains(vec);
        }

        public bool Contains(IBox2 box)
        {
            return this._rect.Contains(box.GetRect());
        }

        public bool Intersects(IBox2 box)
        {
            return this._rect.Intersects(box.GetRect());
        }

        public RelativeBox2 GetRelativeBox(Margin margin)
        {
            return new RelativeBox2(
                this.GetRect().X + margin.Left,
                this.GetRect().Y + margin.Top,
                this.GetRect().Width - margin.Right,
                this.GetRect().Height - margin.Bottom
                ,this);
        }


        public Rectangle GetRect(Vector2 origin)
        {
            return new Rectangle(
                this.GetRect().X+(int)origin.X,
                this.GetRect().Y + (int)origin.Y,
                this.GetRect().Width,
                this.GetRect().Height
                );
        }
    }
}
