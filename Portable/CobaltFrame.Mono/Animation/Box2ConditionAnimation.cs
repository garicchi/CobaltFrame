using CobaltFrame.Core.Animation;
using CobaltFrame.Core.Progress;
using CobaltFrame.Mono.Context;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Animation
{
    public class Box2ConditionAnimation:ConditionProgress<Box2>
    {
        protected Func<Box2, TimeSpan, Box2> Expression { get; private set; }
        public Box2ConditionAnimation(Box2 beginValue,Func<Box2,TimeSpan,Box2> expression)
            : base(beginValue)
        {
            this.Expression = expression;
        }
        
        protected override Box2 UpdateExpression(Box2 currentValue, TimeSpan elapsedTime)
        {
            return this.Expression(currentValue, elapsedTime);
        }

    }
}
