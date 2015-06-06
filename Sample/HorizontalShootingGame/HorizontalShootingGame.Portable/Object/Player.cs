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
using CobaltFrame.Common;
using CobaltFrame.Animation;
using CobaltFrame.Context;
using CobaltFrame.UI;

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

        public List<Bullet> BulletList
        {
            get { return _bulletList; }
            set { _bulletList = value; }
        }

        public BindableProperty<int> Energy { get; set; }

        private TimerAnimation _damageTimer;
        public Player(string texturePath)
            : base(texturePath)
        {
            this._bulletList = new List<Bullet>();
            for (int i = 0; i < 10;i++ )
            {
                Bullet bullet = new Bullet(
                    "Texture/bullet");
                bullet.SetRect(new Rectangle(
                    this.GetRect().X,
                    this.GetRect().Y + this.GetRect().Height / 2,
                    80,
                    10));
                this.AddChild(bullet);
                this._bulletList.Add(bullet);
            }

            this.Energy = new BindableProperty<int>();
            this.Energy.Value = 100;

            this._damageTimer = new TimerAnimation(TimeSpan.FromSeconds(2));
            this.AddChild(this._damageTimer);
            
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

        public override void Update(FrameContext context)
        {
            base.Update(context);

            
        }

        public override void Draw(FrameContext context)
        {
            base.Draw(context);
        }

        public void Shot()
        {
            if (this._bulletList.Any(q => q.IsVisible == false))
            {

                var bullet = this._bulletList.Where(q => q.IsVisible == false).First();
                var init = new Point(this.GetSize().X, this.GetSize().Y / 2);
                bullet.SetPosition(init);
                bullet.Shot(init);

            }
        }

        public void Damage()
        {
            if (this._damageTimer.State == AnimationState.Stop)
            {
                this.Energy.Value -= 2;
                this._damageTimer.Start();
                
            }
        }
    }
}
