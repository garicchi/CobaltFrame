using CobaltFrame.Core.Animation;
using CobaltFrame.Mono.Context;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Animation
{
    public class Position2DConditionAnimation:ConditionAnimationBase<Position2D>
    {
        protected Func<Position2D, TimeSpan, Position2D> Expression { get; private set; }
        public Position2DConditionAnimation(GameContext context,Position2D beginValue,Func<Position2D,TimeSpan,Position2D> expression)
            : base(context,beginValue)
        {
            this.Expression = expression;
        }
        
        protected override Position2D UpdateExpression(Position2D currentValue, TimeSpan elapsedTime)
        {
            return this.Expression(currentValue, elapsedTime);
        }
    }
}
