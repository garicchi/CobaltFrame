using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
	/// <summary>
	/// スクリーントランジションのインターフェース
	/// </summary>
    public interface IScreenTransition:IGameObject2D
    {
		/// <summary>
		/// トランジション完了時のイベント
		/// </summary>
        event Action OnCompleted;

		/// <summary>
		/// トランジション開始
		/// </summary>
        void Start();
    }
}
