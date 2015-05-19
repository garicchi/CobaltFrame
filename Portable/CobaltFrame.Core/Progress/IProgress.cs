using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
	/// <summary>
	/// Progressインターフェース
	/// </summary>
    public interface IProgress<T>:IUpdatable
    {
		/// <summary>
		/// 終了時イベント
		/// </summary>
        event Action OnCompleted;

		/// <summary>
		/// 開始時イベント
		/// </summary>
        event Action OnStarted;

		/// <summary>
		/// Progressの状態
		/// </summary>
		/// <value>The state.</value>
        ProgressState State { get; }

		/// <summary>
		/// Progressの現在の値
		/// </summary>
		/// <value>The current value.</value>
        T CurrentValue { get; set; }

		/// <summary>
		/// 経過時間
		/// </summary>
		/// <value>The elapsed time.</value>
        TimeSpan ElapsedTime { get; }

		/// <summary>
		/// 開始時間
		/// </summary>
		/// <value>The begin time.</value>
        TimeSpan BeginTime { get; }

		/// <summary>
		/// 初期値
		/// </summary>
		/// <value>The begin value.</value>
        T BeginValue { get; set; }

		/// <summary>
		/// 開始
		/// </summary>
        void Start();

		/// <summary>
		/// 一時停止
		/// </summary>
        void Pause();

		/// <summary>
		/// 再開
		/// </summary>
        void Resume();

		/// <summary>
		/// 終了
		/// </summary>
        void Stop();
    }
}
