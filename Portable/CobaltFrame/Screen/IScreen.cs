using CobaltFrame.Common;
using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public interface IScreen
    {
        /// <summary>
        /// 自身のスクリーンから別のスクリーンへ遷移したいときに自身が呼び出す
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="parameter"></param>
        void Navigate(ScreenBase screen, object parameter);

        /// <summary>
        /// 別のスクリーンから自身のスクリーンに来た時に呼び出される
        /// </summary>
        /// <param name="parameter"></param>
        void NavigateTo(object parameter);
    }
}
