using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public interface IProgress<T>:IUpdatableObject
    {
        event Action OnCompleted;
        event Action OnStarted;
        ProgressState State { get; }
        T CurrentValue { get; set; }
        TimeSpan ElapsedTime { get; }
        TimeSpan BeginTime { get; }

        T BeginValue { get; set; }

        void Start();
        void Pause();
        void Resume();
        void Stop();
    }
}
