using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
	/// <summary>
	/// 条件を指定して進むProgressのインターフェース
	/// </summary>
    public interface IConditionProgress<T>:IProgress<T>
    {
		/// <summary>
		/// 開始トリガー
		/// </summary>
		/// <value>The begin triggers.</value>
        IList<Func<T, bool>> BeginTriggers { get; set; }

		/// <summary>
		/// 終了トリガー
		/// </summary>
		/// <value>The stop triggers.</value>
        IList<Func<T, bool>> StopTriggers { get; set; }
        
    }
}
