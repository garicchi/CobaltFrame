using CobaltFrame.Mono.Context;
using CobaltFrame.Position;
using CobaltFrame.Mono.Screen;
using HorizontalShootingGame.Portable.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.UI;

namespace HorizontalShootingGame.Portable.Screen
{
    public class Stage1Screen:GameScreen
    {
        Player player;
        SlidePadObject slidePad;
        public Stage1Screen()
            : base()
        {
            
        }

        public override void Load()
        {
            base.Load();
            player = new Player(new Box2(0, 0, 200, 200), "Texture/Player");
            
            this.AddDrawableObject(player);

            var background = new Texture2DObject(this.Box,"Texture/spaceback",true);
            background.LayerDepth = 1.0f;
            this.AddDrawableObject(background);

            var progress = new ProgressBarObject(new Box2(300,100,400,50),"Texture/progress_frame","Texture/progress_inner");
            this.AddDrawableObject(progress);

            slidePad = new SlidePadObject(new Box2(400,400,100,100),"Texture/slidepad_pad","Texture/slidepad_back");
            this.AddDrawableObject(slidePad);

            this.Inputs.RegisterInput("PlayerUp",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Up),
                null
            );
            this.Inputs.RegisterInput("PlayerDown",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Down),
                null
            );
            this.Inputs.RegisterInput("PlayerLeft",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Left),
                null
            );
            this.Inputs.RegisterInput("PlayerRight",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Right),
                null
            );
            this.Inputs.RegisterInput("PlayerShot",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Space) && !GameInput.KeyboardStatePrev.IsKeyDown(Keys.Space),
                null
            );
        }

        

        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            
            base.Update(context);

            if (this.Inputs.IsInput("PlayerUp")
                &&this.Box.Contains(this.player.Box.TryMoveRect(this.player.MoveSpeed)))
            {

                this.player.Box.MoveRect(this.player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerDown")
                && this.Box.Contains(this.player.Box.TryMoveRect(0,this.player.MoveSpeed)))
            {
                this.player.Box.MoveRect(0, this.player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerLeft")
                && this.Box.Contains(this.player.Box.TryMoveRect(0,0,0,this.player.MoveSpeed)))
            {
                this.player.Box.MoveRect(0, 0, 0, this.player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerRight")
                && this.Box.Contains(this.player.Box.TryMoveRect(0,0,this.player.MoveSpeed)))
            {
                this.player.Box.MoveRect(0, 0, this.player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerShot"))
            {
                this.player.Shot();
            }
            if (this.Box.Contains(this.player.Box.TryMoveRect(0,(int)slidePad.CurrentValue.Y,(int)slidePad.CurrentValue.X)))
            {
                this.player.Box.MoveRect(0,(int)slidePad.CurrentValue.Y,(int)slidePad.CurrentValue.X);
                
            }
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
