using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            base.SetPosition(newRect);
            
        }
        public override Rectangle GetPosition()
        {
            var relativeRect = base.GetPosition();
            return new Rectangle(
                relativeRect.X + this._sourcePosition.GetPosition().X,
                relativeRect.Y + this._sourcePosition.GetPosition().Y,
                relativeRect.Width,
                relativeRect.Height
                );
        }

        public override Vector2 GetLocation()
        {
            var relativeRect = base.GetLocation();
            return new Vector2(
                relativeRect.X + this._sourcePosition.GetPosition().X,
                relativeRect.Y + this._sourcePosition.GetPosition().Y
                );
        }

        
    }
}
