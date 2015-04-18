using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Position
{
    public class RelativeBox2:Box2
    {
        private Box2 _sourceBox;
        
        protected Box2 SourcePosition
        {
            get { return _sourceBox; }
        }

        public RelativeBox2(Rectangle position,Box2 sourcePosition)
            :base(position)
        {
            this._sourceBox = sourcePosition;

        }

        public override void SetRect(Rectangle newRect)
        {
            base.SetRect(newRect);
            
        }
        public override Rectangle GetRect()
        {
            return new Rectangle(
                this._drawRect.X + this._sourceBox.GetRect().X,
                this._drawRect.Y + this._sourceBox.GetRect().Y,
                this._drawRect.Width,
                this._drawRect.Height
                );
        }

        
        public override Vector2 GetLocation()
        {
            var relativeRect = base.GetLocation();
            return new Vector2(
                relativeRect.X + this._sourceBox.GetRect().X,
                relativeRect.Y + this._sourceBox.GetRect().Y
                );
        }

        public void SetAbsoluteLocation(Vector2 newPos)
        {
            base.SetLocation(new Vector2((int)newPos.X - this._sourceBox.GetRect().X,
                (int)newPos.Y - this._sourceBox.GetRect().Y));
            
        }

        public Vector2 GetRelativeLocation()
        {
            return new Vector2(this._drawRect.X, this._drawRect.Y);
        }
        

        public override bool Contains(int x, int y)
        {
            return this.GetRect().Contains((float)x, (float)y);
        }

        public override bool Contains(Box2 position)
        {
            return this.GetRect().Contains(position.GetRect());
        }

        public override bool Contains(Vector2 vec)
        {
            return this.GetRect().Contains(vec);
        }

        public override bool Intersects(Box2 position)
        {
            return this.GetRect().Intersects(position.GetRect());
        }

        public override CobaltFrame.Position.Box2 TryMoveRect(int up = 0, int down = 0, int right = 0, int left = 0)
        {
            var box = base.TryMoveRect(up,down,right,left);
            return new RelativeBox2(box.GetRect(),_sourceBox);
        }
        
        
    }
}
