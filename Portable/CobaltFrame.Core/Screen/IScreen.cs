using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
    public interface IScreen:IDrawable
    {
        event Action<IScreen, object, IScreenTransition> OnNavigate;
        event Action<int, object, IScreenTransition> OnNavigatePrevious;
        /// <summary>
        /// 自身のスクリーンから別のスクリーンへ遷移したいときに自身が呼び出す
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        void Navigate(IScreen screen, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null);

        void NavigatePrevious(int oldNum, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null);

        /// <summary>
        /// 別のスクリーンから自身のスクリーンに来た時に呼び出される
        /// </summary>
        /// <param name="parameter"></param>
        void NavigateTo(object parameter, IScreenTransition transition = null);
    }
}
