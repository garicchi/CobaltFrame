using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public interface ITimeProgress<T>:IProgress<T>
    {
        
        TimeSpan Duration{ get; }
        
        float CurrentProgress { get; set; }
        bool IsLoop { get; set; }
        bool IsChain { get; }

        
        T EndValue { get; set; }
        
        ITimeProgress<T> NextProgress { get; set; }

        /// <summary>
        /// IProgressをつなぎ合わせて連続Progressを作る
        /// </summary>
        /// <param name="nextProgress">次のProgress</param>
        /// <param name="onCompleted">このProgressが完了したとき(いらないならnull)</param>
        /// <returns></returns>
        ITimeProgress<T> Chain(ITimeProgress<T> nextProgress,Action<ITimeProgress<T>> onCompleted);
        /// <summary>
        /// 連続Progressの最後はこれを実行する
        /// </summary>
        /// <param name="onCompleted">このProgressが完了したとき(いらないならnull)</param>
        void Chain(Action onCompleted);

    }
}
