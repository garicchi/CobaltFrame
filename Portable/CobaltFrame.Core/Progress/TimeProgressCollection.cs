using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
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

        public TimeProgressCollection()
        {
            this._progressCollection = new List<TimeProgress<T>>();
        }
        public TimeProgressCollection(TimeProgressCollection<T> collection)
        {
            this._progressCollection = new List<TimeProgress<T>>();
            foreach (var progress in collection)
            {
                this._progressCollection.Add(progress);
            }
        }
        public void Add(TimeProgress<T> item)
        {
            this._progressCollection.Add(item);
        }

        public void Clear()
        {
            this._progressCollection.Clear();
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
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
