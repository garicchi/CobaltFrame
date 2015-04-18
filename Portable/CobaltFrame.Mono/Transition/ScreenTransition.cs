using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Core.Object;
using CobaltFrame.Core.Screen;
using CobaltFrame.Mono.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Mono.Position;

namespace CobaltFrame.Mono.Transition
{
    public abstract class ScreenTransition:DrawableGameObject,IScreenTransition
    {
        
        public ScreenTransition(IBox2 pos)
            :base(pos)
        {
            this.OnCompleted += () => { };
        }

        protected void Translate()
        {
            this.OnCompleted();
        }

        public event Action OnCompleted;

        public abstract void Start();

    }
}
