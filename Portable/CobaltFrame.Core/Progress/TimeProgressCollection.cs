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
	/// 時間経過Progressのコレクション
	/// </summary>
    public class TimeProgressCollection<T> : UpdatableObject, ICollection<TimeProgress<T>>, IProgress<T>
    {
        
        List<TimeProgress<T>> _progressCollection;

		/// <summary>
		/// コレクション
		/// </summary>
		/// <value>The progress collection.</value>
        public List<TimeProgress<T>> ProgressCollection
        {
            get { return _progressCollection; }
            set { _progressCollection = value; }
        }

        protected int _currentProgressNum;

		/// <summary>
		/// 現在のProgressの数
		/// </summary>
		/// <value>The current progress number.</value>
        public int CurrentProgressNum
        {
            get { return _currentProgressNum; }
            set { _currentProgressNum = value; }
        }

		/// <summary>
		/// 現在の値
		/// </summary>
		/// <value>The current progress.</value>
        protected TimeProgress<T> CurrentProgress
        {
            get { return this._progressCollection.ElementAt(this._currentProgressNum); }
            
        }

        public TimeProgressCollection()
        {
            this._progressCollection = new List<TimeProgress<T>>();

            this._beforeState = ProgressState.Stop;
            this._state = ProgressState.Stop;
            
            this.OnCompleted += () => { };
            this.OnStarted += () => { };
        }

        public TimeProgressCollection(TimeProgressCollection<T> collection)
        {
            TimeProgress<T>[] array = collection.ToArray();
            collection.CopyTo(array,0);
            this._progressCollection = new List<TimeProgress<T>>();
            foreach (var progress in collection)
            {
                this.Add(progress);
            }

            this._beforeState = ProgressState.Stop;
            this._state = ProgressState.Stop;

            this.OnCompleted += () => { };
            this.OnStarted += () => { };
        }

        protected ProgressState _beforeState;

		/// <summary>
		/// 終了時イベント
		/// </summary>
        public event Action OnCompleted;

		/// <summary>
		/// 開始時イベント
		/// </summary>
        public event Action OnStarted;

        protected ProgressState _state;

		/// <summary>
		/// Progressの状態
		/// </summary>
		/// <value>The state.</value>
        public ProgressState State
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
            if (this._progressCollection.Count > 0)
            {
                this._state = ProgressState.Active;
                this._currentProgressNum = 0;
                var next = this._progressCollection.ElementAt(this._currentProgressNum);
                next.OnCompleted += this.OnOneProgressCompleted;
                next.Start();
            }
        }

        private void OnOneProgressCompleted()
        {
            
            if ((this._currentProgressNum+1) >= this._progressCollection.Count)
            {
                this.OnCompleted();
                this._state = ProgressState.Stop;
            }
            else
            {

                this._currentProgressNum++;
                var next = this._progressCollection.ElementAt(this._currentProgressNum);
                next.OnCompleted += this.OnOneProgressCompleted;
                next.Start();
            }
        }

		/// <summary>
		/// 一時停止
		/// </summary>
        public void Pause()
        {
            this._state = ProgressState.Pause;
            this.CurrentProgress.Pause();
        }

		/// <summary>
		/// 再開
		/// </summary>
        public void Resume()
        {
            this._state = ProgressState.Active;
            this.CurrentProgress.Resume();
        }

		/// <summary>
		/// 終了
		/// </summary>
        public void Stop()
        {
            this._state = ProgressState.Stop;
            this.CurrentProgress.Stop();

        }

		/// <summary>
		/// 更新関数
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="frameContext">Frame context.</param>
        public override void Update(Context.IFrameContext frameContext)
        {
            base.Update(frameContext);
            if (this.State == ProgressState.Active)
            {
                if (this._beforeState == ProgressState.Stop && this.State == ProgressState.Active)
                {
                    this._beginValue = this._progressCollection.ElementAt(0).BeginValue;
                    
                    this._beginTime = frameContext.TotalGameTime;
                    this.OnStarted();
                }

                if (this._beforeState == ProgressState.Pause && this.State == ProgressState.Active)
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
        public void Add(TimeProgress<T> item)
        {
            this._progressCollection.Add(item);
            this.AddObject(item);
            this.BeginValue = this._progressCollection.ElementAt(0).BeginValue;
            this.CurrentValue = this._progressCollection.ElementAt(0).CurrentValue;
        }

		/// <summary>
		/// Progressクリア
		/// </summary>
        public void Clear()
        {
            this._progressCollection.Clear();
            this.GameObjects.Clear();
        }

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Progressが含まれてるかどうか
		/// </summary>
		/// <param name="item">Item.</param>
        public bool Contains(TimeProgress<T> item)
        {
            return this._progressCollection.Contains(item);
        }

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="arrayIndex">Array index.</param>
        public void CopyTo(TimeProgress<T>[] array, int arrayIndex)
        {
            this._progressCollection.CopyTo(array, arrayIndex);
        }

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
        public int Count
        {
            get { return this._progressCollection.Count; }
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
        public bool Remove(TimeProgress<T> item)
        {
            this.RemoveObject(item);
            return this._progressCollection.Remove(item);

        }

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
        public IEnumerator<TimeProgress<T>> GetEnumerator()
        {
            return this._progressCollection.GetEnumerator();
        }

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._progressCollection.GetEnumerator();
        }


        
    }
}
