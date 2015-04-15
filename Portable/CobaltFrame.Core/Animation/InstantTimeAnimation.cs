using CobaltFrame.Core.Context;
using CobaltFrame.Core.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Animation
{
    public class InstantTimeAnimation<T>:TimeProgress<T>
    {

        private Func<T, T,float, T> _expression;


        public InstantTimeAnimation(T begin,T end,TimeSpan duration,Func<T,T,float,T> expression)
            : base(duration,begin,end)
        {

            this._expression = expression;
        }


        protected override T UpdateExpression(T begin, T end, float currentProgress)
        {
            return _expression(begin, end, currentProgress);
        }
    }
}
