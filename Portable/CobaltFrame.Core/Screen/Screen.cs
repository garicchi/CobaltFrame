using CobaltFrame.Core.Context;
using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
    public abstract class Screen:DrawableObject,IScreen
    {
        public event Action<IScreen, object,IScreenTransition> OnNavigate;
        public event Action<int, object,IScreenTransition> OnNavigatePrevious;
        

        public Screen()
            :base()
        {
            this.OnNavigate += (sc,obj,trans) => { };
            this.OnNavigatePrevious += (num,obj,trans) => { };
        }

        public virtual void Navigate(IScreen screen, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null)
        {
            this.OnNavigate(screen,parameter,toTrans);
        }

        public virtual void NavigatePrevious(int oldNum, object parameter, IScreenTransition fromTrans = null, IScreenTransition toTrans = null)
        {
            this.OnNavigatePrevious(oldNum,parameter,toTrans);
        }


        public virtual void NavigateTo(object parameter,IScreenTransition transition = null)
        {
            
        }

    }
}
