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
	/// スクリーンマネージャのインターフェース
	/// </summary>
    interface IGameManager:IGameObject
    {
		
		/// <summary>
		/// 現在のスクリーン
		/// </summary>
		/// <value>The current screen.</value>
        IGameScreen CurrentScreen { get; }

        /// <summary>
        /// 画面遷移
        /// </summary>
        /// <param name="nextScreen"></param>
        /// <param name="parameter"></param>
        /// <param name="transition"></param>
        void ChangeScreen(IGameScreen nextScreen, object parameter, IScreenTransition transition);
		/// <summary>
		/// スクリーンの解像度が変更されたとき
		/// </summary>
        void ScreenResolutionChanged(Point defaultResolution);

        void WindowSizeChanged(Rectangle clientBounds);

        /// <summary>
        /// 背景色
        /// </summary>
        Color BackgroundColor { get; set; }
    }
}
