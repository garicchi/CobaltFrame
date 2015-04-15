using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Context;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace HorizontalShootingGame.Portable.Object
{
    
    public class Player : Texture2DObject
    {
        private int _speed;
        private List<Bullet> _bulletList;

        public Player(GameContext context, Box2 position, string texturePath)
            : base(context, position, texturePath)
        {
            this._bulletList = new List<Bullet>();
            for (int i = 0; i < 10;i++ )
            {
                Bullet bullet = new Bullet(context,new Box2(
                    this.Position.GetRect().X,
                    this.Position.GetRect().Y + this.Position.GetRect().Height / 2,
                    100,
                    10)
                    ,"Texture/bullet");
                
                this.AddDrawableObject(bullet);
                this._bulletList.Add(bullet);
            }
        }
        public override void Initialize()
        {
            base.Initialize();
            this._speed = 2;
        }

        public override void LoadObject()
        {
            base.LoadObject();

            GameInput.RegisterInputState("PlayerUp",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Up),
                null
            );
            GameInput.RegisterInputState("PlayerDown",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Down),
                null
            );
            GameInput.RegisterInputState("PlayerLeft",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Left),
                null
            );
            GameInput.RegisterInputState("PlayerRight",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Right),
                null
            );
            GameInput.RegisterInputState("PlayerShot",
                null,
                null,
                null,
                () => GameInput.KeyboardState.IsKeyDown(Keys.Space)&&!GameInput.KeyboardStatePrev.IsKeyDown(Keys.Space),
                null
            );
        }

        public override void UnloadObject()
        {
            base.UnloadObject();
            GameInput.UnregisterInputState("PlayerUp");
            GameInput.UnregisterInputState("PlayerDown");
            GameInput.UnregisterInputState("PlayerLeft");
            GameInput.UnregisterInputState("PlayerRight");
            GameInput.UnregisterInputState("PlayerShot");
        }

        public override void Update(IFrameContext context)
        {
            base.Update(context);

            if (GameInput.IsInput("PlayerUp"))
            {
                this.MovePosition(this._speed);
            }
            if (GameInput.IsInput("PlayerDown"))
            {
                this.MovePosition(0,this._speed);
            }
            if (GameInput.IsInput("PlayerLeft"))
            {
                this.MovePosition(0,0,0,this._speed);
            }
            if (GameInput.IsInput("PlayerRight"))
            {
                this.MovePosition(0,0,this._speed);
            }
            if (GameInput.IsInput("PlayerShot"))
            {
                if (this._bulletList.Any(q => q.IsVisible == false))
                {

                    Bullet bullet = this._bulletList.Where(q => q.IsVisible == false).First();
                    bullet.Position.SetLocation(new Vector2(this.Position.GetLocation().X,this.Position.GetLocation().Y+this.Position.GetRect().Height/2));
                    bullet.Shot();

                }
            }
        }

        public override void Draw(IFrameContext context)
        {
            base.Draw(context);
        }
    }
}
