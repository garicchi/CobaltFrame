using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Object;
using CobaltFrame.Mono.Position;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.UI
{
    public abstract class UIObject:DrawableGameObject
    {
        public UIObject(IBox2 position)
            : base(position)
        {

        }

        
    }
}
