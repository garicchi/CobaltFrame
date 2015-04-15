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
        private int _moveSpeed;

        public int MoveSpeed
        {
            get { return _moveSpeed; }
            set { _moveSpeed = value; }
        }
        private List<Bullet> _bulletList;

        public Player(Box2 position, string texturePath)
            : base(position, texturePath)
        {
            this._bulletList = new List<Bullet>();
            for (int i = 0; i < 10;i++ )
            {
                Bullet bullet = new Bullet(new Box2(
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
            this._moveSpeed = 4;
        }

        public override void Load()
        {
            base.Load();

            
        }

        public override void Unload()
        {
            base.Unload();
            
        }

        public override void Update(IFrameContext context)
        {
            base.Update(context);

            
        }

        public override void Draw(IFrameContext context)
        {
            base.Draw(context);
        }

        public void Shot()
        {
            if (this._bulletList.Any(q => q.IsVisible == false))
            {

                Bullet bullet = this._bulletList.Where(q => q.IsVisible == false).First();
                bullet.Box.SetLocation(new Vector2(this.Box.GetLocation().X, this.Box.GetLocation().Y + this.Box.GetRect().Height / 2));
                bullet.Shot();

            }
        }
    }
}
