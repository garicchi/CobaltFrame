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
        void ScreenResolutionChanged();

        /// <summary>
        /// ゲームの計算を行うデフォルト解像度
        /// この解像度でゲームの計算を行い、各デバイスの解像度へ拡大縮小する
        /// </summary>
        Point DefaultResolution { get; set; }

        /// <summary>
        /// デバイス解像度へ拡大縮小するモード
        /// </summary>
        ScaleMode ScreenScaleMode { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        Color BackgroundColor { get; set; }
    }
}
