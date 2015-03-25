using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public abstract class ScreenBase:IScreen
    {
        protected ScreenContext _screenContext;

        public ScreenBase(ScreenContext screenContext)
        {
            this._screenContext = screenContext;
        }

        public void Initialize(ScreenContext screenContext)
        {
           
        }

        public void LoadScreen()
        {
            
        }

        public void UnloadScreen()
        {
            
        }

        public IScreen Update(ScreenFrameContext frameContext)
        {
            return this;
        }

        public void Draw(ScreenFrameContext frameContext)
        {
           
        }
    }
}
