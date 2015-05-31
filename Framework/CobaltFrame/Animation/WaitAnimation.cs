using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
    public class WaitAnimation<T>:TimeAnimation<T>
    {
        public WaitAnimation(TimeSpan duration, T begin)
            : base(duration, begin, begin)
        {

        }

        protected override T UpdateExpression(T begin, T end, float currentProgress)
        {
            return begin;
        }

    }

    
}
