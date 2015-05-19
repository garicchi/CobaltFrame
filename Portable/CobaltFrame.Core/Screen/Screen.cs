using CobaltFrame.Core.Context;
using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
	/// <summary>
	/// ゲーム画面
	/// </summary>
    public abstract class Screen:DrawableObject,IScreen
    {
		/// <summary>
		/// ナビゲート開始したとき
		/// </summary>
        public event Action<IScreen, object,IScreenTransition> OnNavigate;

		/// <summary>
		/// 前のスクリーンにナビゲート開始したとき
		/// </summary>
        public event Action<int, object,IScreenTransition> OnNavigatePrevious;
        

        public Screen()
            :base()
        {
            this.OnNavigate += (sc,obj,trans) => { };
            this.OnNavigatePrevious += (num,obj,trans) => { };
        }

		/// <summary>
		/// 自身のスクリーンから別のスクリーンへ遷移したいときに自身が呼び出す
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="parameter"></param>
		/// <param name="fromTrans">From trans.</param>
		/// <param name="toTrans">To trans.</param>
        public virtual void Navigate(IScreen screen, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null)
        {
            this.OnNavigate(screen,parameter,toTrans);
        }

		/// <summary>
		/// 自身のスクリーンから過去のスクリーンへ遷移したいときに自身が呼び出す
		/// </summary>
		/// <param name="oldNum">Old number.</param>
		/// <param name="parameter">Parameter.</param>
		/// <param name="fromTrans">From trans.</param>
		/// <param name="toTrans">To trans.</param>
        public virtual void NavigatePrevious(int oldNum, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null)
        {
            this.OnNavigatePrevious(oldNum,parameter,toTrans);
        }

		/// <summary>
		/// 別のスクリーンから自身のスクリーンに来た時に呼び出される
		/// </summary>
		/// <param name="parameter"></param>
		/// <param name="transition">Transition.</param>
        public virtual void NavigateTo(object parameter,IScreenTransition transition = null)
        {
            
        }

    }
}
