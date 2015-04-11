using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Object;
using CobaltFrame.Position;
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
        public TitleScreen(GameContext context)
            : base(context)
        {
            player = new Player(context,new Position2D(50,300,200,200),"Texture/Player");
            this.AddDrawableObject(player);
            var player2 = new Player(context, new Position2D(65, 315, 200, 200), "Texture/Player");
            player2.LayerDepth = 0.0f;
            this.AddDrawableObject(player2);
            
            GameInput.RegisterInputState("input1", () =>
            {
                if(GameInput.TouchCollection.Count>0)
                {
                    return GameInput.TouchCollection.ElementAt(0).State==TouchLocationState.Released;
                }
                else
                {
                    return false;
                }
                
            });
        }

        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Update(context);
            
            
            if (GameInput.IsInput("input1"))
            {
                var gContext = this._gameContext as GameContext;
                this.Navigate(new Stage1Screen(gContext),null,
                    new FadeColorTransition(gContext,Color.Black,0,255,TimeSpan.FromSeconds(0.8)),
                    new FadeColorTransition(gContext,Color.Black,255,0,TimeSpan.FromSeconds(0.8)));
            }
            
        }

        public override void Draw(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Draw(context);
        }
    }
}
