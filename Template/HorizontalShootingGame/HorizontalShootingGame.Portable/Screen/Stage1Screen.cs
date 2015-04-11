using CobaltFrame.Context;
using CobaltFrame.Position;
using CobaltFrame.Screen;
using HorizontalShootingGame.Portable.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Screen
{
    public class Stage1Screen:GameScreen
    {
        public Stage1Screen(GameContext context)
            : base(context)
        {
            
        }

        public override void LoadObject()
        {
            base.LoadObject();
            var player = new Player(this._gameContext as GameContext, new Position2D(0, 0, 200, 200), "Texture/Player");
            
            this.AddDrawableObject(player);
        }

        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            
            base.Update(context);
        }

        public override void Draw(CobaltFrame.Core.Context.IFrameContext context)
        {
            
            base.Draw(context);
        }

        public override void NavigateTo(object parameter, CobaltFrame.Core.Screen.IScreenTransition transition = null)
        {
            base.NavigateTo(parameter, transition);
            
        }
    }
}
