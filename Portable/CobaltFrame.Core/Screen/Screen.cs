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
        public event Action<IScreen, object> OnNavigate;
        public event Action<int, object> OnNavigatePrevious;

        public Screen(IGameContext context)
            :base(context)
        {
            this.OnNavigate += (sc,obj) => { };
            this.OnNavigatePrevious += (num,obj) => { };
        }

        public void Navigate(IScreen screen, object parameter)
        {
            throw new NotImplementedException();
        }


        public virtual void NavigateTo(object parameter)
        {
            
        }

    }
}
