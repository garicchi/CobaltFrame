using CobaltFrame.Core.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Object;
using CobaltFrame.Screen;

namespace CobaltFrame.Transition
{
    public abstract class ScreenTransition:GameObject2D,IScreenTransition
    {
        
        public ScreenTransition()
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
