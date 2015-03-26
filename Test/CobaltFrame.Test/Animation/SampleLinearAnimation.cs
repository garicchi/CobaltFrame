using CobaltFrame.Animation;
using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Test.Animation
{
    public class SampleLinearAnimation:AnimationBase
    {
        public Vector2 StartPosition { get; private set; }
        public Vector2 EndPosition { get; private set; }

        public Vector2 CurrentPosition { get; private set; }
        public SampleLinearAnimation(ObjectContext context,Vector2 start,Vector2 end)
            :base(context)
        {
            this.StartPosition = start;
            this.EndPosition = end;
            this.CurrentPosition = this.StartPosition;
        }

        public override void Update(ObjectFrameContext frameContext)
        {
            base.Update(frameContext);
            CurrentPosition = (EndPosition - StartPosition) * this.Progress;
        }

        
    }
}
