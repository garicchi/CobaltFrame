using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
	/// <summary>
	/// スクリーンマネージャのインターフェース
	/// </summary>
    interface IScreenManager:IDrawable
    {
		/// <summary>
		/// 過去画面のキャッシュサイズ
		/// </summary>
		/// <value>The size of the screen chache.</value>
        int ScreenChacheSize { get; set; }

		/// <summary>
		/// 過去スクリーンのキュー
		/// </summary>
		/// <value>The previous screen queue.</value>
        Queue<IScreen> PreviousScreenQueue { get; }

		/// <summary>
		/// 最初のスクリーン
		/// </summary>
		/// <value>The first screen.</value>
        IScreen FirstScreen { get; }

		/// <summary>
		/// 現在のスクリーン
		/// </summary>
		/// <value>The current screen.</value>
        IScreen CurrentScreen { get; }

		/// <summary>
		/// スクリーンの解像度が変更されたとき
		/// </summary>
        void ScreenResolutionChanged();
    }
}
