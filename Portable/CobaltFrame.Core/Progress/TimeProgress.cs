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
	/// <summary>
	/// 時間更新式で進むProgress
	/// </summary>
    public abstract class TimeProgress<T>:UpdatableObject,ITimeProgress<T>
    {
		/// <summary>
		/// 終了時イベント
		/// </summary>
        public event Action OnCompleted;

		/// <summary>
		/// 開始時イベント
		/// </summary>
        public event Action OnStarted;

        private ProgressState _state;

		/// <summary>
		/// Progressの状態
		/// </summary>
		/// <value>The state.</value>
        public ProgressState State
        {
            get { return _state; }
        }
        private TimeSpan _duration;

		/// <summary>
		/// 長さ
		/// </summary>
		/// <value>The duration.</value>
        public TimeSpan Duration
        {
            get { return _duration; }
        }
        private TimeSpan _elapsedTime;

		/// <summary>
		/// 経過時間
		/// </summary>
		/// <value>The elapsed time.</value>
        public TimeSpan ElapsedTime
        {
            get { return this._elapsedTime; }
        }
        private TimeSpan _beginTime;

		/// <summary>
		/// 開始時間
		/// </summary>
		/// <value>The begin time.</value>
        public TimeSpan BeginTime
        {
            get { return _beginTime; }
        }
        private float _currentProgress;

		/// <summary>
		/// 現在の進捗(0.0〜1.0)
		/// </summary>
		/// <value>The current progress.</value>
        public float CurrentProgress
        {
            get { return this._currentProgress; }
            set { this._currentProgress = value; }
        }
        private bool _isLoop;

		/// <summary>
		/// ループするかどうか
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
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

        private ProgressState _beforeState;

        protected T _beginValue;

		/// <summary>
		/// 初期値
		/// </summary>
		/// <value>The begin value.</value>
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

		/// <summary>
		/// 終了値
		/// </summary>
		/// <value>The end value.</value>
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

		/// <summary>
		/// Progressの現在の値
		/// </summary>
		/// <value>The current value.</value>
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
        
        public TimeProgress(TimeSpan duration,T begin,T end)
            :base()
        {
            
            this._beforeState = ProgressState.Stop;
            this._state = ProgressState.Stop;
            this._isLoop = false;
            this._duration = duration;
            this.OnCompleted += () => { };
            this.OnStarted += () => { };
            this._beginValue = begin;
            this._endValue = end;
            this._currentValue = begin;
        }

		/// <summary>
		/// 更新関数
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="frameContext">Frame context.</param>
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


            this._beforeState = State;
        }

		/// <summary>
		/// 開始
		/// </summary>
        public virtual void Start()
        {
            this._currentProgress = 0.0f;
            this._state = ProgressState.Active;
            
        }

		/// <summary>
		/// 一時停止
		/// </summary>
        public virtual void Pause()
        {
            this._state = ProgressState.Pause;
        }

		/// <summary>
		/// 再開
		/// </summary>
        public virtual void Resume()
        {
            this._state = ProgressState.Active;
        }

		/// <summary>
		/// 終了
		/// </summary>
        public virtual void Stop()
        {
            this._state = ProgressState.Stop;
        }

		/// <summary>
		/// 更新式
		/// </summary>
		/// <returns>更新後の値</returns>
		/// <param name="begin">開始値</param>
		/// <param name="end">終了値</param>
		/// <param name="currentProgress">現在の進捗</param>
        protected abstract T UpdateExpression(T begin, T end, float currentProgress);




    }
}
