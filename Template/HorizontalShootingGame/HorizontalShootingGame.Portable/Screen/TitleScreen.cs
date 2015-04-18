using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Object;
using CobaltFrame.Mono.Position;
using CobaltFrame.Mono.Screen;
using CobaltFrame.Mono.Transition;
using HorizontalShootingGame.Portable.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Screen
{
    public class TitleScreen:GameScreen
    {
        Player player;
        public TitleScreen()
            : base()
        {
            player = new Player(new Box2(50,300,200,200),"Texture/Player");
            this.AddDrawableObject(player);
            
        }
        public override void Load()
        {
            base.Load();

            
        }
        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Update(context);
            
        }

        public override void Draw(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Draw(context);
        }
    }
}
