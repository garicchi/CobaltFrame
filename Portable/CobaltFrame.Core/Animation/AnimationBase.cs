using CobaltFrame.Core.Context;
using CobaltFrame.Core.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Animation
{
    public abstract class AnimationBase:CobaltFrame.Core.Progress.Progress
    {
        public AnimationBase(IGameContext context,TimeSpan duration)
            : base(context,duration)
        {

        }
    }
}
