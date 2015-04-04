using CobaltFrame.Core.Context;
using CobaltFrame.Core.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Animation
{
    public abstract class ConditionAnimationBase<T>:ConditionProgress<T>
    {
        public ConditionAnimationBase(IGameContext context,T beginValue)
            : base(context,beginValue)
        {

        }
    }
}
