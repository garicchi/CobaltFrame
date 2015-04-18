using CobaltFrame.Core.Animation;
using CobaltFrame.Core.Progress;
using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Animation
{
    public class Box2ConditionAnimation:ConditionProgress<IBox2>
    {
        protected Func<IBox2, TimeSpan, IBox2> Expression { get; private set; }
        public Box2ConditionAnimation(IBox2 beginValue,Func<IBox2,TimeSpan,IBox2> expression)
            : base(beginValue)
        {
            this.Expression = expression;
        }
        
        protected override IBox2 UpdateExpression(IBox2 currentValue, TimeSpan elapsedTime)
        {
            return this.Expression(currentValue, elapsedTime);
        }

    }
}
