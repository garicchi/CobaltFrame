using CobaltFrame.Core.Context;
using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public abstract class ConditionProgress<T>:UpdatableObject,IConditionProgress<T>
    {
        public event Action OnCompleted;

        public event Action OnStarted;

        private ProgressState _beforeState;

        public ConditionProgress(IGameContext context,T beginValue)
            : base(context)
        {
            this.OnCompleted+=()=>{};
            this.OnStarted += () => { };
            this._state = ProgressState.Stop;
            this._beforeState = ProgressState.Stop;
            this._beginValue = beginValue;
            this._currentValue = beginValue;
            this._beginTriggers = new List<Func<T, bool>>();
            this._stopTriggers = new List<Func<T, bool>>();
        }
        private ProgressState _state;
        public ProgressState State
        {
            get { return  this._state; }
        }
        private T _currentValue;
        public T CurrentValue
        {
            get
            {
                return this._currentValue;
            }
            set
            {
                this._currentValue = value;
            }
        }
        private TimeSpan _elapsedTime;
        public TimeSpan ElapsedTime
        {
            get { return this._elapsedTime; }
        }
        private TimeSpan _beginTime;
        public TimeSpan BeginTime
        {
            get { return this._beginTime; }
        }
        private T _beginValue;
        public T BeginValue
        {
            get
            {
                return this._beginValue;
            }
            set
            {
                this._beginValue = value;
            }
        }
        private IList<Func<T, bool>> _beginTriggers;
        public IList<Func<T, bool>> BeginTriggers
        {
            get
            {
                return this._beginTriggers;
            }
            set
            {
                this._beginTriggers = value;
            }
        }
        private IList<Func<T, bool>> _stopTriggers;
        public IList<Func<T, bool>> StopTriggers
        {
            get
            {
                return this._stopTriggers;
            }
            set
            {
                this._stopTriggers = value;
            }
        }

        public void Start()
        {
            this._currentValue = this._beginValue;
            this._state = ProgressState.Active;
        }

        public void Pause()
        {
            this._state = ProgressState.Pause;
        }

        public void Resume()
        {
            this._state = ProgressState.Active;
        }

        public void Stop()
        {
            this._state = ProgressState.Stop;
        }

        public override void Update(IFrameContext context)
        {
            base.Update(context);

            if (this.State == ProgressState.Active)
            {
                if (this._beforeState == ProgressState.Stop && this.State == ProgressState.Active)
                {
                    this._beginTime = context.TotalGameTime;
                    this.OnStarted();
                }

                if (this._beforeState == ProgressState.Pause && this.State == ProgressState.Active)
                {
                    //Pause状態から再開した場合は現在の時間から経過した時間を戻す事によって初期時間を復元
                    this._beginTime = context.TotalGameTime - ElapsedTime;
                }

                this._elapsedTime = context.TotalGameTime - this.BeginTime;
                
                this._currentValue = this.UpdateExpression(this._currentValue,this._elapsedTime);

                foreach (var trigger in this._stopTriggers)
                {
                    if (trigger(this._currentValue))
                    {
                        this._state = ProgressState.Stop;
                        this.OnCompleted();
                        break;
                    }
                }
            }
            else
            {
                foreach (var trigger in this._beginTriggers)
                {
                    if (trigger(this._currentValue))
                    {
                        this._currentValue = this._beginValue;
                        this._state = ProgressState.Active;
                        break;
                    }
                }
            }

            this._beforeState = State;
        }


        protected abstract T UpdateExpression(T currentValue,TimeSpan elapsedTime);




        
    }
}
