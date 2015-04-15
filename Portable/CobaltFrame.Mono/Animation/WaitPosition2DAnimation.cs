using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Core.Progress;

namespace CobaltFrame.Mono.Animation
{
    public class WaitBox2Animation:TimeProgress<Box2>
    {
        public WaitBox2Animation(GameContext context, TimeSpan duration, Box2 begin)
            : base(context, duration, begin, begin)
        {

        }

        protected override Box2 UpdateExpression(Box2 begin, Box2 end, float currentProgress)
        {
            return begin;
        }
    }
}
