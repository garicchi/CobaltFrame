using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Position
{
    public class GameRerativePosition:GamePosition
    {
        private GamePosition _sourcePosition;
        
        protected GamePosition SourcePosition
        {
            get { return _sourcePosition; }
        }

        public GameRerativePosition(Rectangle position,GamePosition sourcePosition)
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
            var absoluteRect = base.GetPosition();
            return new Rectangle(
                absoluteRect.X - this._sourcePosition.GetPosition().X,
                absoluteRect.Y - this._sourcePosition.GetPosition().Y,
                absoluteRect.Width,
                absoluteRect.Height
                );
        }

        public Rectangle GetAbsolutePosition()
        {
            return base.GetPosition();
        }
    }
}
