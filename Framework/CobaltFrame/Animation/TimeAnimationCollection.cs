using CobaltFrame.Animation;
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
	/// 時間経過Progressのコレクション
	/// </summary>
    public class TimeAnimationCollection<T> : GameObject, ICollection<TimeAnimation<T>>, IAnimation<T>
    {
        
        List<TimeAnimation<T>> _animationCollection;

		/// <summary>
		/// コレクション
		/// </summary>
		/// <value>The progress collection.</value>
        public List<TimeAnimation<T>> AnimationCollection
        {
            get { return _animationCollection; }
            set { _animationCollection = value; }
        }

        protected int _currentAnimationNum;

		/// <summary>
		/// 現在のProgressの数
		/// </summary>
		/// <value>The current progress number.</value>
        public int CurrentAnimationNum
        {
            get { return _currentAnimationNum; }
            set { _currentAnimationNum = value; }
        }

		/// <summary>
		/// 現在の値
		/// </summary>
		/// <value>The current progress.</value>
        protected TimeAnimation<T> CurrentProgress
        {
            get { return this._animationCollection.ElementAt(this._currentAnimationNum); }
            
        }

        public TimeAnimationCollection()
        {
            this._animationCollection = new List<TimeAnimation<T>>();

            this._beforeState = AnimationState.Stop;
            this._state = AnimationState.Stop;
            
            this.OnCompleted += () => { };
            this.OnStarted += () => { };
        }

        public TimeAnimationCollection(TimeAnimationCollection<T> collection)
        {
            TimeAnimation<T>[] array = collection.ToArray();
            collection.CopyTo(array,0);
            this._animationCollection = new List<TimeAnimation<T>>();
            foreach (var progress in collection)
            {
                this.Add(progress);
            }

            this._beforeState = AnimationState.Stop;
            this._state = AnimationState.Stop;

            this.OnCompleted += () => { };
            this.OnStarted += () => { };
        }

        protected AnimationState _beforeState;

		/// <summary>
		/// 終了時イベント
		/// </summary>
        public event Action OnCompleted;

		/// <summary>
		/// 開始時イベント
		/// </summary>
        public event Action OnStarted;

        protected AnimationState _state;

		/// <summary>
		/// Progressの状態
		/// </summary>
		/// <value>The state.</value>
        public AnimationState State
        {
            get { return this._state; }
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

        protected TimeSpan _elapsedTime;

		/// <summary>
		/// 経過時間
		/// </summary>
		/// <value>The elapsed time.</value>
        public TimeSpan ElapsedTime
        {
            get { return this._elapsedTime; }
        }

        protected TimeSpan _beginTime;

		/// <summary>
		/// 開始時間
		/// </summary>
		/// <value>The begin time.</value>
        public TimeSpan BeginTime
        {
            get { return this._beginTime; }
        }

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

		/// <summary>
		/// 開始
		/// </summary>
        public void Start()
        {
            if (this._animationCollection.Count > 0)
            {
                this._state = AnimationState.Active;
                this._currentAnimationNum = 0;
                var next = this._animationCollection.ElementAt(this._currentAnimationNum);
                next.OnCompleted += this.OnOneProgressCompleted;
                next.Start();
            }
        }

        private void OnOneProgressCompleted()
        {
            
            if ((this._currentAnimationNum+1) >= this._animationCollection.Count)
            {
                this.OnCompleted();
                this._state = AnimationState.Stop;
            }
            else
            {

                this._currentAnimationNum++;
                var next = this._animationCollection.ElementAt(this._currentAnimationNum);
                next.OnCompleted += this.OnOneProgressCompleted;
                next.Start();
            }
        }

		/// <summary>
		/// 一時停止
		/// </summary>
        public void Pause()
        {
            this._state = AnimationState.Pause;
            this.CurrentProgress.Pause();
        }

		/// <summary>
		/// 再開
		/// </summary>
        public void Resume()
        {
            this._state = AnimationState.Active;
            this.CurrentProgress.Resume();
        }

		/// <summary>
		/// 終了
		/// </summary>
        public void Stop()
        {
            this._state = AnimationState.Stop;
            this.CurrentProgress.Stop();

        }

		/// <summary>
		/// 更新関数
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="frameContext">Frame context.</param>
        public override void Update(FrameContext frameContext)
        {
            base.Update(frameContext);
            if (this.State == AnimationState.Active)
            {
                if (this._beforeState == AnimationState.Stop && this.State == AnimationState.Active)
                {
                    this._beginValue = this._animationCollection.ElementAt(0).BeginValue;
                    
                    this._beginTime = frameContext.TotalGameTime;
                    this.OnStarted();
                }

                if (this._beforeState == AnimationState.Pause && this.State == AnimationState.Active)
                {
                    //Pause状態から再開した場合は現在の時間から経過した時間を戻す事によって初期時間を復元
                    this._beginTime = frameContext.TotalGameTime - ElapsedTime;
                }

                this._elapsedTime = frameContext.TotalGameTime - this.BeginTime;
                this._currentValue = this.CurrentProgress.CurrentValue;
                
            }
            this._beforeState = State;
        }

		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Progress追加
		/// </summary>
		/// <param name="item">Item.</param>
        public void Add(TimeAnimation<T> item)
        {
            this._animationCollection.Add(item);
            this.BeginValue = this._animationCollection.ElementAt(0).BeginValue;
            this.CurrentValue = this._animationCollection.ElementAt(0).CurrentValue;
            this.AddChild(item);
        }

		/// <summary>
		/// Progressクリア
		/// </summary>
        public void Clear()
        {
            this._animationCollection.Clear();
            this.Children.Clear();
        }

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Progressが含まれてるかどうか
		/// </summary>
		/// <param name="item">Item.</param>
        public bool Contains(TimeAnimation<T> item)
        {
            return this._animationCollection.Contains(item);
        }

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="arrayIndex">Array index.</param>
        public void CopyTo(TimeAnimation<T>[] array, int arrayIndex)
        {
            this._animationCollection.CopyTo(array, arrayIndex);
        }

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
        public int Count
        {
            get { return this._animationCollection.Count; }
        }

		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get { return false; }
        }

		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
        public bool Remove(TimeAnimation<T> item)
        {
            this.RemoveChild(item);
            return this._animationCollection.Remove(item);

        }

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
        public IEnumerator<TimeAnimation<T>> GetEnumerator()
        {
            return this._animationCollection.GetEnumerator();
        }

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._animationCollection.GetEnumerator();
        }


        
    }
}
