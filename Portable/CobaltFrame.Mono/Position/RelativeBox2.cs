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
            var relativeRect = base.GetRect();
            return new Rectangle(
                relativeRect.X + this._sourceBox.GetRect().X,
                relativeRect.Y + this._sourceBox.GetRect().Y,
                relativeRect.Width,
                relativeRect.Height
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

        public override bool Contains(int x, int y)
        {
            return this.GetRect().Contains((float)x, (float)y);
        }

        public override bool Contains(Box2 position)
        {
            return this.GetRect().Contains(position.GetRect());
        }

        public override bool Intersects(Box2 position)
        {
            return this.GetRect().Intersects(position.GetRect());
        }
        
    }
}
