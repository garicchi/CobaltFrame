using CobaltFrame.Mono.Context;
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
using CobaltFrame.Mono.Position;
using CobaltFrame.Mono.Animation;
using CobaltFrame.Core.Animation;
using CobaltFrame.Core.Progress;
using CobaltFrame.Core.Common;

namespace HorizontalShootingGame.Portable.Screen
{
    public class Stage1Screen:GameScreen
    {
        Player _player;
        SlidePadObject _slidePad;
        ProgressBarObject _playerEnergyBar;
		ButtonObject _shotButton;
		List<EnemyBase> _enemyList;


        public Stage1Screen()
            : base()
        {
            
        }

        public override void Init()
        {
            base.Init();

            this._enemyList = new List<EnemyBase>();

            _player = new Player(new Box2(0, 200, 100, 100), "Texture/Player");
            _player.Energy.Bind("playerDamage",q => this._playerEnergyBar.CurrentProgress = (float)q / 100.0f);
            this.AddDrawableObject(_player);

            var background = new Texture2DObject(this.Box, "Texture/frame", false);
            background.LayerDepth = 1.0f;
            this.AddDrawableObject(background);

			this._playerEnergyBar = new ProgressBarObject(new Box2(30, 10, 461 ,93),new Margin(10,10,10,10), "Texture/progress_frame", "Texture/progress_inner");
            this.AddDrawableObject(this._playerEnergyBar);

            this._slidePad = new SlidePadObject(new Box2(50, this.Box.GetRect().Height - 150, 100, 100), "Texture/slidepad_pad", "Texture/slidepad_back");
            this.AddDrawableObject(_slidePad);

			this._shotButton = new ButtonObject (new Box2(Box.GetRect().Width-100,Box.GetRect().Height-100,100,100),"Texture/shotbutton_on","Texture/shotbutton_off");
			this._shotButton.OnClick += (sender,pos) => 
			{
				this._player.Shot();
			};
				
			this.AddDrawableObject (_shotButton);


            for (int i = 0; i < 5; i++)
            {
                var collection = new TimeProgressCollection<Box2>
                {
                    new Box2TimeAnimation(TimeSpan.FromSeconds(2),new Box2(Box.GetRect().Right-200,0,100,100),new Box2(Box.GetRect().Right-300,400,100,100)),
                    new WaitBox2Animation(TimeSpan.FromSeconds(3), new Box2(Box.GetRect().Right-300, 400, 100, 100)),
                    new Box2TimeAnimation(TimeSpan.FromSeconds(5), new Box2(Box.GetRect().Right-300, 400, 100, 100),new Box2(200,600,100,100)),

                };

                _enemyList.Add(new Enemy1(collection, TimeSpan.FromSeconds(i)));
            }

            for (int i = 0; i < 5; i++)
            {
                var collection = new TimeProgressCollection<Box2>
                {
                    new Box2TimeAnimation(TimeSpan.FromSeconds(3),new Box2(Box.GetRect().Right-100,800,100,100),new Box2(Box.GetRect().Right-200,300,100,100)),
                    new WaitBox2Animation(TimeSpan.FromSeconds(1), new Box2(Box.GetRect().Right-200, 300, 100, 100)),
                    new Box2TimeAnimation(TimeSpan.FromSeconds(2), new Box2(Box.GetRect().Right-200, 300, 100, 100),new Box2(500,60,100,100)),

                };

                _enemyList.Add(new Enemy1(collection, TimeSpan.FromSeconds(i + 2)));
            }
            foreach (var e in _enemyList)
            {
                this.AddDrawableObject(e);
            }
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
                () => GameInput.KeyboardState.IsKeyDown(Keys.Space) && !GameInput.KeyboardStatePrev.IsKeyDown(Keys.Space),
                null
            );


        }

        

        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            
            base.Update(context);

            if (this.Inputs.IsInput("PlayerUp")
                && this.Box.Contains(this._player.Box.TryMoveRect(this._player.MoveSpeed)))
            {

                this._player.Box.MoveRect(this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerDown")
                && this.Box.Contains(this._player.Box.TryMoveRect(0, 0, this._player.MoveSpeed)))
            {
                this._player.Box.MoveRect(0, 0, this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerLeft")
                && this.Box.Contains(this._player.Box.TryMoveRect(0, this._player.MoveSpeed)))
            {
                this._player.Box.MoveRect(0, this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerRight")
                && this.Box.Contains(this._player.Box.TryMoveRect(0, 0, 0, this._player.MoveSpeed)))
            {
                this._player.Box.MoveRect(0, 0, 0, this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerShot"))
            {
                this._player.Shot();
            }
            if (this.Box.Contains(this._player.Box.TryMoveRect(0, 0, (int)_slidePad.CurrentValue.Y, 0)))
            {
                this._player.Box.MoveRect(0, 0, (int)_slidePad.CurrentValue.Y / 3, 0);
                
            }
            if (this.Box.Contains(this._player.Box.TryMoveRect(0, 0, 0, (int)_slidePad.CurrentValue.X)))
            {
                this._player.Box.MoveRect(0, 0, 0, (int)_slidePad.CurrentValue.X / 3);

            }

			if (_enemyList.Where(q => q.Box.Intersects(this._player.Box)&&q.IsVisible).Any())
            {
                this._player.Damage();
            }

			foreach (var bullet in _player.BulletList.Where(q=>q.IsVisible).ToList()) 
			{
				foreach (var enemy in _enemyList.Where(q=>q.IsVisible).ToList()) 
				{
					if (bullet.Box.Intersects (enemy.Box)) 
					{
						bullet.Hit ();
						enemy.Die ();
					}
				}
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
