using CobaltFrame.Core.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Position
{
    public class RelativeBox2:IBox2
    {
        private IBox2 _sourceBox;
        private Rectangle _relativeRect;

        public RelativeBox2(int x, int y, int w, int h, IBox2 source)
        {
            this._sourceBox = source;
            this._relativeRect = new Rectangle(x,y,w,h);
        }

        public RelativeBox2(RelativeBox2 box)
        {
            this._sourceBox = box._sourceBox;
            this._relativeRect = box.GetRelativeRect();
        }

        public RelativeBox2(Rectangle rect,IBox2 source)
        {
            this._sourceBox = source;
            this._relativeRect = rect;
        }

        public IBox2 GetSourceBox()
        {
            return this._sourceBox;
        }
        public Rectangle GetRelativeRect()
        {
            return this._relativeRect;
        }

        public void SetAbsoluteRect(Rectangle rect)
        {
            this._relativeRect.X = rect.X - this._sourceBox.GetRect().X;
            this._relativeRect.Y = rect.Y - this._sourceBox.GetRect().Y;
            this._relativeRect.Width = rect.Width - this._sourceBox.GetRect().Width;
            this._relativeRect.Height = rect.Height - this._sourceBox.GetRect().Height;
        }

        public void SetAbsoluteLocation(Vector2 vec)
        {
            this._relativeRect.X = (int)vec.X - this._sourceBox.GetRect().X;
            this._relativeRect.Y = (int)vec.Y - this._sourceBox.GetRect().Y;
            
        }
        public void SetRect(Microsoft.Xna.Framework.Rectangle rect)
        {
            this._relativeRect = rect;
        }

        public Microsoft.Xna.Framework.Rectangle GetRect()
        {
            return new Rectangle(
                this._relativeRect.X+this._sourceBox.GetRect().X,
                this._relativeRect.Y+this._sourceBox.GetRect().Y,
                this._relativeRect.Width,
                this._relativeRect.Height
                );
        }

        public void SetLocation(Microsoft.Xna.Framework.Vector2 vec)
        {
            this._relativeRect.X = (int)vec.X;
            this._relativeRect.Y = (int)vec.Y;

        }

        public Microsoft.Xna.Framework.Vector2 GetLocation()
        {
            return new Vector2(
                this._relativeRect.X+this._sourceBox.GetRect().X,
                this._relativeRect.Y+this._sourceBox.GetRect().Y

                );
        }

        public Microsoft.Xna.Framework.Vector2 GetCenter()
        {
            var center = this.GetRect().Center;
            return new Vector2(
                center.X,center.Y
                );
        }

        public void SetCenter(Microsoft.Xna.Framework.Vector2 vec)
        {
            this._relativeRect.X = (int)vec.X + this._relativeRect.Width / 2;
            this._relativeRect.Y = (int)vec.Y + this._relativeRect.Height / 2;
        }

        public void MoveRect(int up = 0, int left = 0, int down = 0, int right = 0)
        {
            this.SetLocation(new Vector2(this._relativeRect.X + right - left, this._relativeRect.Y + down - up));
        }

        public IBox2 TryMoveRect(int up = 0, int left = 0, int down = 0, int right = 0)
        {
            var box = new RelativeBox2(this);
            box.MoveRect(up,left,down,right);
            return box;
        }

        public bool Contains(int x, int y)
        {
            return this.GetRect().Contains(x, y);
        }

        public bool Contains(Microsoft.Xna.Framework.Vector2 vec)
        {
            return this.GetRect().Contains(vec);
        }

        public bool Contains(IBox2 box)
        {
            return this.GetRect().Contains(box.GetRect());
        }

        public bool Intersects(IBox2 box)
        {
            return this.GetRect().Intersects(box.GetRect());
        }

        public RelativeBox2 GetRelativeBox(Margin margin)
        {
            return new RelativeBox2(
                this.GetRect().X + margin.Left,
                this.GetRect().Y + margin.Top,
                this.GetRect().Width - margin.Right,
                this.GetRect().Height - margin.Bottom
                , this);
        }


        public Rectangle GetRect(Vector2 origin)
        {
            return new Rectangle(
                this.GetRect().X + (int)origin.X,
                this.GetRect().Y + (int)origin.Y,
                this.GetRect().Width,
                this.GetRect().Height
                );
        }
    }
}
