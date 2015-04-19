using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public class TimeProgressCollection<T> : UpdatableObject, ICollection<TimeProgress<T>>, IProgress<T>
    {
        
        List<TimeProgress<T>> _progressCollection;

        public List<TimeProgress<T>> ProgressCollection
        {
            get { return _progressCollection; }
            set { _progressCollection = value; }
        }

        protected int _currentProgressNum;

        public int CurrentProgressNum
        {
            get { return _currentProgressNum; }
            set { _currentProgressNum = value; }
        }

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

        public event Action OnCompleted;

        public event Action OnStarted;

        protected ProgressState _state;
        public ProgressState State
        {
            get { return this._state; }
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
        protected TimeSpan _elapsedTime;
        public TimeSpan ElapsedTime
        {
            get { return this._elapsedTime; }
        }
        protected TimeSpan _beginTime;
        public TimeSpan BeginTime
        {
            get { return this._beginTime; }
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

        public void Pause()
        {
            this._state = ProgressState.Pause;
            this.CurrentProgress.Pause();
        }

        public void Resume()
        {
            this._state = ProgressState.Active;
            this.CurrentProgress.Resume();
        }

        public void Stop()
        {
            this._state = ProgressState.Stop;
            this.CurrentProgress.Stop();

        }


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

        public void Add(TimeProgress<T> item)
        {
            this._progressCollection.Add(item);
            this.AddObject(item);
            this.BeginValue = this._progressCollection.ElementAt(0).BeginValue;
            this.CurrentValue = this._progressCollection.ElementAt(0).CurrentValue;
        }

        public void Clear()
        {
            this._progressCollection.Clear();
            this.GameObjects.Clear();
        }

        public bool Contains(TimeProgress<T> item)
        {
            return this._progressCollection.Contains(item);
        }

        public void CopyTo(TimeProgress<T>[] array, int arrayIndex)
        {
            this._progressCollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._progressCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TimeProgress<T> item)
        {
            this.RemoveObject(item);
            return this._progressCollection.Remove(item);

        }

        public IEnumerator<TimeProgress<T>> GetEnumerator()
        {
            return this._progressCollection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._progressCollection.GetEnumerator();
        }


        
    }
}
