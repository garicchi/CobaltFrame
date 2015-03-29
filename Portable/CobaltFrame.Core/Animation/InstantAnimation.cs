using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Animation
{
    public class InstantAnimation<T>:AnimationBase
    {
        private T _begin;
        private T _end;
        private T _value;
        private Func<T, T,float, T> _expression;

        public T Value
        {
            get { return _value; }
        }

        public InstantAnimation(IGameContext context,T begin,T end,TimeSpan duration,Func<T,T,float,T> expression)
            : base(context, duration)
        {
            this._begin = begin;
            this._end = end;
            this._expression = expression;
        }

        public override void Update(Core.Context.IFrameContext frameContext)
        {
            base.Update(frameContext);
            this._value = this._expression(this._begin,this._end,this.CurrentProgress);
        }


    }
}
