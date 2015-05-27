using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
	/// <summary>
	/// ゲーム画面のインターフェース
	/// </summary>
    public interface IGameScreen:IGameObject
    {
		/// <summary>
		/// ナビゲート開始したとき
		/// </summary>
        event Action<IGameScreen, object, IScreenTransition> OnNavigate;

        /// <summary>
        /// 自身のスクリーンから別のスクリーンへ遷移したいときに自身が呼び出す
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        void Navigate(IGameScreen screen, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null);

        /// <summary>
        /// 別のスクリーンから自身のスクリーンに来た時に呼び出される
        /// </summary>
        /// <param name="parameter"></param>
        void NavigateTo(object parameter, IScreenTransition transition = null);

        Color BackgroundColor { get; set; }
    }
}
