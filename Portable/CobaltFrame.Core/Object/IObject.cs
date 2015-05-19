using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
	/// <summary>
	/// ゲームオブジェクトのインターフェース
	/// </summary>
    public interface IObject
    {
		/// <summary>
		/// どこまでロードされたか
		/// </summary>
		/// <value>The state of the load.</value>
        ObjectLoadState LoadState { get; }

		/// <summary>
		/// 初期化関数
		/// </summary>
        void Init();

		/// <summary>
		/// リソース確保関数
		/// </summary>
        void Load();

		/// <summary>
		/// リソース解放関数
		/// </summary>
        void Unload();
    }
}
