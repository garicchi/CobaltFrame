using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Object;
using CobaltFrame.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.UI
{
    public abstract class UIObject:DrawableGameObject
    {
        public UIObject(GameContext context,Box2 position)
            : base(context,position)
        {

        }

        
    }
}
