using CobaltFrame.Core.Context;
using CobaltFrame.Core.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Animation
{
    public abstract class AnimationBase<T>:CobaltFrame.Core.Progress.Progress<T>
    {
        public AnimationBase(IGameContext context,TimeSpan duration,T begin,T end)
            : base(context,duration,begin,end)
        {

        }

    }
}
