using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public interface IProgress:IUpdatableObject
    {
        event Action OnCompleted;
        ProgressState State { get; }
        TimeSpan Duration{ get; }
        TimeSpan ElapsedTime { get; }
        TimeSpan BeginTime { get; }
        float CurrentProgress { get; }
        bool IsLoop { get; set; }
        bool IsChain { get; }

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
        IProgress Chain(IProgress nextProgress,Action<IProgress> onCompleted);
        /// <summary>
        /// 連続Progressの最後はこれを実行する
        /// </summary>
        /// <param name="onCompleted">このProgressが完了したとき(いらないならnull)</param>
        void Chain(Action onCompleted);
    }
}
