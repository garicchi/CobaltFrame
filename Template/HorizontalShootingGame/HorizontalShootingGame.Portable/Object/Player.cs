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
using CobaltFrame.Mono.UI;

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
                    this.Box.GetRect().X,
                    this.Box.GetRect().Y + this.Box.GetRect().Height / 2,
                    100,
                    10)
                    ,"Texture/bullet");
                
                this.AddDrawableObject(bullet);
                this._bulletList.Add(bullet);
            }
        }
        public override void Init()
        {
            base.Init();
            this._speed = 2;
        }

        public override void Load()
        {
            base.Load();

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
                () => GameInput.KeyboardState.IsKeyDown(Keys.Space)&&!GameInput.KeyboardStatePrev.IsKeyDown(Keys.Space),
                null
            );
        }

        public override void Unload()
        {
            base.Unload();
            
        }

        public override void Update(IFrameContext context)
        {
            base.Update(context);

            if (this.Inputs.IsInput("PlayerUp"))
            {
                this.MovePosition(this._speed);
            }
            if (this.Inputs.IsInput("PlayerDown"))
            {
                this.MovePosition(0,this._speed);
            }
            if (this.Inputs.IsInput("PlayerLeft"))
            {
                this.MovePosition(0,0,0,this._speed);
            }
            if (this.Inputs.IsInput("PlayerRight"))
            {
                this.MovePosition(0,0,this._speed);
            }
            if (this.Inputs.IsInput("PlayerShot"))
            {
                if (this._bulletList.Any(q => q.IsVisible == false))
                {

                    Bullet bullet = this._bulletList.Where(q => q.IsVisible == false).First();
                    bullet.Box.SetLocation(new Vector2(this.Box.GetLocation().X,this.Box.GetLocation().Y+this.Box.GetRect().Height/2));
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
