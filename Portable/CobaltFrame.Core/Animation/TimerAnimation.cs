using CobaltFrame.Core.Progress;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Animation
{
    public class TimerAnimation:TimeProgress<float>
    {
        public TimerAnimation(TimeSpan duration)
            : base(duration, 0.0f, 1.0f)
        {

        }

        protected override float UpdateExpression(float begin, float end, float currentProgress)
        {
            return (end - begin) * currentProgress + begin;
        }

        public override void Update(Context.IFrameContext frameContext)
        {
            base.Update(frameContext);
            
        }
    }
}
