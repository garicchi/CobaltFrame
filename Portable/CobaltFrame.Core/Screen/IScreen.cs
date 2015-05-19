using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
	/// <summary>
	/// ゲーム画面のインターフェース
	/// </summary>
    public interface IScreen:IDrawable
    {
		/// <summary>
		/// ナビゲート開始したとき
		/// </summary>
        event Action<IScreen, object, IScreenTransition> OnNavigate;

		/// <summary>
		/// 前のスクリーンにナビゲート開始したとき
		/// </summary>
        event Action<int, object, IScreenTransition> OnNavigatePrevious;

        /// <summary>
        /// 自身のスクリーンから別のスクリーンへ遷移したいときに自身が呼び出す
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        void Navigate(IScreen screen, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null);

		/// <summary>
		/// 自身のスクリーンから過去のスクリーンへ遷移したいときに自身が呼び出す
		/// </summary>
		/// <param name="oldNum">Old number.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="fromTrans">From trans.</param>
		/// <param name="toTrans">To trans.</param>
        void NavigatePrevious(int oldNum, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null);

        /// <summary>
        /// 別のスクリーンから自身のスクリーンに来た時に呼び出される
        /// </summary>
        /// <param name="parameter"></param>
        void NavigateTo(object parameter, IScreenTransition transition = null);
    }
}
