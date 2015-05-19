using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
	/// <summary>
	/// 時間更新式で進むProgressのインターフェース
	/// </summary>
    public interface ITimeProgress<T>:IProgress<T>
    {
        /// <summary>
        /// 長さ
        /// </summary>
        /// <value>The duration.</value>
        TimeSpan Duration{ get; }
        
		/// <summary>
		/// 現在の進捗(0.0〜1.0)
		/// </summary>
		/// <value>The current progress.</value>
        float CurrentProgress { get; set; }

		/// <summary>
		/// ループするかどうか
		/// </summary>
		/// <value><c>true</c> if this instance is loop; otherwise, <c>false</c>.</value>
        bool IsLoop { get; set; }

		/// <summary>
		/// 終了値
		/// </summary>
		/// <value>The end value.</value>
        T EndValue { get; set; }
        

    }
}
