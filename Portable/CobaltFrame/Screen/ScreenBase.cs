using CobaltFrame.Common;
using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public abstract class ScreenBase:DrawableObjectUpdater,IScreen
    {
        protected ScreenContext _screenContext;

        public event Action<ScreenBase,object> OnNavigate;

        protected Game _game;


        public ScreenBase(ScreenContext screenContext)
            :base()
        {
            this._screenContext = screenContext;
            
            this._game = _screenContext.Game;
        }

        

        public virtual void Navigate(ScreenBase screen,object parameter)
        {
            OnNavigate(screen,parameter);
        }


        public virtual void NavigateTo(object parameter)
        {
            
        }



    }
}
