using CobaltFrame.Core.Context;
using CobaltFrame.Core.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Animation
{
    public abstract class TimeAnimationBase<T>:CobaltFrame.Core.Progress.TimeProgress<T>
    {
        public TimeAnimationBase(IGameContext context,TimeSpan duration,T begin,T end)
            : base(context,duration,begin,end)
        {

        }

    }
}
