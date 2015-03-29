using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
    interface IScreenManager:IDrawableGameObject
    {
        int ScreenChacheSize { get; set; }
        Queue<IScreen> PreviousScreenQueue { get; }
        IScreen FirstScreen { get; }
        IScreen CurrentScreen { get; }
    }
}
