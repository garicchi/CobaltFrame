using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Animation
{
    public class Box2TimeAnimation:TimeAnimationBase<Box2>
    {

        
        public Box2TimeAnimation(GameContext context,TimeSpan duration,Box2 begin,Box2 end)
            : base(context, duration,begin,end)
        {

        }


        protected override Box2 UpdateExpression(Box2 begin, Box2 end, float currentProgress)
        {
            return ((end - begin) * currentProgress) + begin;
        }
    }
}
