using CobaltFrame.Core.Context;
using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public abstract class TimeProgress<T>:UpdatableObject,ITimeProgress<T>
    {
        public event Action OnCompleted;

        public event Action OnStarted;

        private ProgressState _state;
        public ProgressState State
        {
            get { return _state; }
        }
        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get { return _duration; }
        }
        private TimeSpan _elapsedTime;
        public TimeSpan ElapsedTime
        {
            get { return this._elapsedTime; }
        }
        private TimeSpan _beginTime;
        public TimeSpan BeginTime
        {
            get { return _beginTime; }
        }
        private float _currentProgress;
        public float CurrentProgress
        {
            get { return this._currentProgress; }
            set { this._currentProgress = value; }
        }
        private bool _isLoop;
        public bool IsLoop
        {
            get 
            { 
                return this._isLoop; 
            }
            set
            {
                this._isLoop = value;
            }
        }
        private bool _isChain;
        public bool IsChain
        {
            get { return this._isChain; }
        }

        private ProgressState _beforeState;

        private ITimeProgress<T> _nextProgress;

        public ITimeProgress<T> NextProgress
        {
            get { return this._nextProgress; }
            set { this._nextProgress = value; }
        }

        protected T _beginValue;


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

        protected T _endValue;
        public T EndValue
        {
            get
            {
                return this._endValue;
            }
            set
            {
                this._endValue = value;
            }
        }

        protected T _currentValue;
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
        
        public TimeProgress(IGameContext context, TimeSpan duration,T begin,T end)
            :base(context)
        {
            
            this._beforeState = ProgressState.Stop;
            this._state = ProgressState.Stop;
            this._isLoop = false;
            this._duration = duration;
            this._isChain = false;
            this.OnCompleted += () => { };
            this.OnStarted += () => { };
            this._beginValue = begin;
            this._endValue = end;
            this._currentValue = begin;
        }

        public override void Update(IFrameContext frameContext)
        {
            base.Update(frameContext);
            if (this.State == ProgressState.Active)
            {
                if (this._beforeState == ProgressState.Stop && this.State == ProgressState.Active)
                {
                    this._beginTime = frameContext.TotalGameTime;
                    this.OnStarted();
                }

                if (this._beforeState == ProgressState.Pause && this.State == ProgressState.Active)
                {
                    //Pause状態から再開した場合は現在の時間から経過した時間を戻す事によって初期時間を復元
                    this._beginTime = frameContext.TotalGameTime - ElapsedTime;
                }

                this._elapsedTime = frameContext.TotalGameTime - this.BeginTime;
                this._currentProgress = (float)(this.ElapsedTime.TotalSeconds / this.Duration.TotalSeconds);
                this._currentValue = this.UpdateExpression(this._beginValue,this._endValue,this._currentProgress);
                if (this._currentProgress >= 1.0f)
                {
                    if (this.IsLoop)
                    {
                        this._currentProgress = 0.0f;
                        this._beginTime = frameContext.TotalGameTime;
                    }
                    else
                    {
                        this._currentProgress = 1.0f;
                        this._state = ProgressState.Stop;
                        this.OnCompleted();
                    }
                }

                 
            }

            

            if (IsChain)
            {
                this._currentValue = this._nextProgress.CurrentValue;
            }

            this._beforeState = State;
        }

        public virtual void Start()
        {
            this._currentProgress = 0.0f;
            this._isChain = false;
            this._state = ProgressState.Active;
            
        }

        public virtual void Pause()
        {
            this._state = ProgressState.Pause;
        }

        public virtual void Resume()
        {
            this._state = ProgressState.Active;
        }

        public virtual void Stop()
        {
            this._state = ProgressState.Stop;
        }

        public ITimeProgress<T> Chain(ITimeProgress<T> nextProgress, Action<ITimeProgress<T>> onCompleted)
        {
            this.OnCompleted += () =>
            {
                if (onCompleted != null)
                {
                    nextProgress.OnCompleted += () =>
                    {
                        onCompleted(nextProgress);
                    };
                }
                
                this._isChain = true;
                
                this._nextProgress.Start();
                
            };
            this._nextProgress = nextProgress;
            this.AddObject(this._nextProgress);
            return nextProgress;
        }

        public void Chain(Action onCompleted)
        {
            this.OnCompleted += () =>
            {
                if (onCompleted != null)
                {
                    onCompleted();
                    
                }
                
            };
        }


        protected abstract T UpdateExpression(T begin, T end, float currentProgress);




    }
}
