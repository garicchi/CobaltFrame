using CobaltFrame.Context;
using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
	/// <summary>
	/// 条件を指定して進むAnimation
	/// </summary>
    public abstract class ConditionAnimation<T>:GameObject,IConditionAnimation<T>
    {
        public ConditionAnimation(T beginValue)
            : base()
        {
            this.OnCompleted += () => { };
            this.OnStarted += () => { };
            this._state = AnimationState.Stop;
            this._beforeState = AnimationState.Stop;
            this._beginValue = beginValue;
            this._currentValue = beginValue;
            this._beginTriggers = new List<Func<T, bool>>();
            this._stopTriggers = new List<Func<T, bool>>();
        }

        #region Event

        public event Action OnCompleted;
        public event Action OnStarted;

        #endregion

        #region Field

        private AnimationState _beforeState;
        private AnimationState _state;
        private T _currentValue;
        private TimeSpan _elapsedTime;
        private TimeSpan _beginTime;
        private T _beginValue;
        private IList<Func<T, bool>> _beginTriggers;
        private IList<Func<T, bool>> _stopTriggers;

        #endregion

        #region Property

        public AnimationState State
        {
            get { return  this._state; }
        }

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

        public TimeSpan ElapsedTime
        {
            get { return this._elapsedTime; }
        }

        public TimeSpan BeginTime
        {
            get { return this._beginTime; }
        }

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
            this._state = AnimationState.Active;
        }

        public void Pause()
        {
            this._state = AnimationState.Pause;
        }

        public void Resume()
        {
            this._state = AnimationState.Active;
        }

        public void Stop()
        {
            this._state = AnimationState.Stop;
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);

            if (this.State == AnimationState.Active)
            {
                //前回がStopで今回がActiveなら初回
                if (this._beforeState == AnimationState.Stop && this.State == AnimationState.Active)
                {
                    //アニメーション開始時間を記録
                    this._beginTime = context.TotalGameTime;
                    //イベント発火
                    this.OnStarted();
                }

                if (this._beforeState == AnimationState.Pause && this.State == AnimationState.Active)
                {
                    //Pause状態から再開した場合は現在の時間から経過した時間を戻す事によって初期時間を復元
                    this._beginTime = context.TotalGameTime - ElapsedTime;
                }

                //経過時間を計算1
                this._elapsedTime = context.TotalGameTime - this.BeginTime;

                //更新式によって新しい値を得る
                this._currentValue = this.UpdateExpression(this._currentValue, this._elapsedTime);

                //StopTriggerに引っかかってるならアニメーション終了
                foreach (var trigger in this._stopTriggers)
                {
                    if (trigger(this._currentValue))
                    {
                        this._state = AnimationState.Stop;
                        this.OnCompleted();
                        break;
                    }
                }
            }
            else
            {
                //ActiveでないならBeginTriggerに引っかかってるかどうか調べる
                foreach (var trigger in this._beginTriggers)
                {
                    if (trigger(this._currentValue))
                    {
                        this._currentValue = this._beginValue;
                        this._state = AnimationState.Active;
                        break;
                    }
                }
            }

            this._beforeState = State;
        }


        protected abstract T UpdateExpression(T currentValue, TimeSpan elapsedTime);


        #endregion
        
     
    }
}
