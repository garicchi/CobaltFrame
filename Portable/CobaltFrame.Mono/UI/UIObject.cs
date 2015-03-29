using CobaltFrame.Context;
using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.UI
{
    public abstract class UIObject:DrawableGameObject
    {
        public UIObject(GameContext context)
            : base(context)
        {

        }

        
    }
}
