using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Position
{
    public class RelativePosition2D:Position2D
    {
        private Position2D _sourcePosition;
        
        protected Position2D SourcePosition
        {
            get { return _sourcePosition; }
        }

        public RelativePosition2D(Rectangle position,Position2D sourcePosition)
            :base(position)
        {
            this._sourcePosition = sourcePosition;

        }

        public override void SetPosition(Rectangle newRect)
        {
            var absoluteRect = new Rectangle(
                newRect.X+ this._sourcePosition.GetPosition().X,
                newRect.Y + this._sourcePosition.GetPosition().Y,
                newRect.Width,
                newRect.Height);
            base.SetPosition(absoluteRect);
            
        }
        public override Rectangle GetPosition()
        {
            return base.GetPosition();
        }

        public override Vector2 GetLocation()
        {
            return base.GetLocation();
        }

        public void UpdatePosition(Position2D newSourceposition)
        {
            this._sourcePosition = newSourceposition;
            this.SetPosition(this.GetRelativePosition());
        }
        
        public Rectangle GetRelativePosition()
        {
            var absoluteRect = base.GetPosition();
            return new Rectangle(
                absoluteRect.X - this._sourcePosition.GetPosition().X,
                absoluteRect.Y - this._sourcePosition.GetPosition().Y,
                absoluteRect.Width,
                absoluteRect.Height
                );
        }
    }
}
