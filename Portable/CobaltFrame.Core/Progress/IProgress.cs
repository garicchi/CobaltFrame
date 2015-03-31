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
        ProgressState State { get; }
        TimeSpan Duration{ get; }
        TimeSpan ElapsedTime { get; }
        TimeSpan BeginTime { get; }
        float CurrentProgress { get; set; }
        bool IsLoop { get; set; }
        bool IsChain { get; }

        T BeginValue { get; set; }
        T EndValue { get; set; }
        T CurrentValue { get; set; }

        IProgress<T> NextProgress { get; set; }

        void Start();
        void Pause();
        void Resume();
        void Stop();
        /// <summary>
        /// IProgressをつなぎ合わせて連続Progressを作る
        /// </summary>
        /// <param name="nextProgress">次のProgress</param>
        /// <param name="onCompleted">このProgressが完了したとき(いらないならnull)</param>
        /// <returns></returns>
        IProgress<T> Chain(IProgress<T> nextProgress,Action<IProgress<T>> onCompleted);
        /// <summary>
        /// 連続Progressの最後はこれを実行する
        /// </summary>
        /// <param name="onCompleted">このProgressが完了したとき(いらないならnull)</param>
        void Chain(Action onCompleted);

    }
}
