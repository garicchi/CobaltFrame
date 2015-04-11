using CobaltFrame.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Core.Object;
using CobaltFrame.Core.Screen;
using CobaltFrame.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Transition
{
    public abstract class ScreenTransition:DrawableGameObject,IScreenTransition
    {
        
        public ScreenTransition(GameContext context,Position2D pos)
            :base(context,pos)
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
