using HorizontalShootingGame.Portable.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using CobaltFrame.Mono.Sound;
using CobaltFrame.Screen;
using CobaltFrame.Common;
using CobaltFrame.Animation;
using CobaltFrame.Context;
using CobaltFrame.UI;

namespace HorizontalShootingGame.Portable.Screen
{
    public class Stage1Screen:GameScreen
    {
        Player _player;
        SlidePadObject _slidePad;
        ProgressBarObject _playerEnergyBar;
		BitmapTextObject _scoreText;

		ButtonObject _shotButton;
		List<EnemyBase> _enemyList;
		BindableProperty<int> _score;

        
        public Stage1Screen()
            : base()
        {
            
        }

        public override void Init()
        {
            base.Init();

            this._player = new Player("Texture/player");
            this._player.SetRect(new Rectangle(0, 200, 100, 100));
            this._player.Energy.Bind("playerDamage",q => this._playerEnergyBar.CurrentProgress = (float)q / 100.0f);
            this.AddChild(this._player);

            var background = new RepeatableTexure2DObject("Texture/spaceback");
            background.SetRect(this.GetRect());
            background.LayerDepth = 1.0f;
            this.AddChild(background);

            this._playerEnergyBar = new ProgressBarObject(new Margin(10, 10, 10, 10), "System/Texture/progress_frame", "System/Texture/progress_value");
            this._playerEnergyBar.SetRect(new Rectangle(30, 10, 461 ,93));
            this.AddChild(this._playerEnergyBar);

            this._slidePad = new SlidePadObject("System/Texture/slidepad_pad", "System/Texture/slidepad_back");
            this._slidePad.SetRect(new Rectangle(50, this.GetRect().Height - 150, 100, 100));
            this.AddChild(this._slidePad);

            this._shotButton = new ButtonObject("System/Texture/circlebutton_on", "System/Texture/circlebutton_off");
            this._shotButton.SetRect(new Rectangle(this.GetRect().Width-100,this.GetRect().Height-100,100,100));
            this._shotButton.OnClick += (pos) => 
			{
				this._player.Shot();
			};

			this.AddChild(_shotButton);

			this._score = new BindableProperty<int>();
			this._score.Value = 0;

            this._scoreText = new BitmapTextObject("System/Font/ipagothic", this._score.Value.ToString(), 4, Color.White);
            this._scoreText.SetRect(new Rectangle(this.GetRect().Width-500,10,800,80));
            this.AddChild(this._scoreText);

			this._score.Bind ("scoreProp", (val) => 
			{
			    this._scoreText.Text=val.ToString();
			});

			this._enemyList = new List<EnemyBase>();

            
            
            for (int i = 0; i < 10; i++)
            {
                var collection = new TimeAnimationCollection<Point>
                {
                    new PointTimeAnimation(TimeSpan.FromSeconds(2),new Point(this.GetRect().Right-200,0),new Point(this.GetRect().Right-300,400)),
                    new WaitAnimation<Point>(TimeSpan.FromSeconds(3), new Point(this.GetRect().Right-300, 400)),
                    new PointTimeAnimation(TimeSpan.FromSeconds(5), new Point(this.GetRect().Right-300, 400),new Point(200,600)),
                };

				this._enemyList.Add(new Enemy1(collection, TimeSpan.FromSeconds(i)));
            }

            for (int i = 0; i < 10; i++)
            {
                var collection = new TimeAnimationCollection<Point>
                {
                    new PointTimeAnimation(TimeSpan.FromSeconds(3),new Point(this.GetRect().Right-100,800),new Point(this.GetRect().Right-200,300)),
                    new WaitAnimation<Point>(TimeSpan.FromSeconds(1), new Point(this.GetRect().Right-200, 300)),
                    new PointTimeAnimation(TimeSpan.FromSeconds(2), new Point(this.GetRect().Right-200, 300),new Point(500,60)),
                };

                this._enemyList.Add(new Enemy1(collection, TimeSpan.FromSeconds(i + 7)));
            }
            
            foreach (var e in _enemyList)
            {
                this.AddChild(e);
            }
            
        }

        public override void Load()
        {
            base.Load();
            

            this.Inputs.RegisterInput("PlayerUp",
                null,
                null,
                null,
                (current,prev) => current.IsKeyDown(Keys.Up),
                null
            );
            this.Inputs.RegisterInput("PlayerDown",
                null,
                null,
                null,
                (current,prev) => current.IsKeyDown(Keys.Down),
                null
            );
            this.Inputs.RegisterInput("PlayerLeft",
                null,
                null,
                null,
                (current,prev) => current.IsKeyDown(Keys.Left),
                null
            );
            this.Inputs.RegisterInput("PlayerRight",
                null,
                null,
                null,
                (current,prev) => current.IsKeyDown(Keys.Right),
                null
            );
            this.Inputs.RegisterInput("PlayerShot",
				null,
                null,
                null,
                (current,prev) => current.IsKeyDown(Keys.Space) && !prev.IsKeyDown(Keys.Space),
                null
            );

        }

        

        public override void Update(FrameContext context)
        {
            
            base.Update(context);

            if (this.Inputs.IsInput("PlayerUp")
                && this.GetRect().Contains(this._player.TryMoveRect(this._player.MoveSpeed)))
            {

                this._player.MoveRect(this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerDown")
                && this.GetRect().Contains(this._player.TryMoveRect(0, 0, this._player.MoveSpeed)))
            {
                this._player.MoveRect(0, 0, this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerLeft")
                && this.GetRect().Contains(this._player.TryMoveRect(0, this._player.MoveSpeed)))
            {
                this._player.MoveRect(0, this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerRight")
                && this.GetRect().Contains(this._player.TryMoveRect(0, 0, 0, this._player.MoveSpeed)))
            {
                this._player.MoveRect(0, 0, 0, this._player.MoveSpeed);
            }
            if (this.Inputs.IsInput("PlayerShot"))
            {
                this._player.Shot();
                
            }
            if (this.GetRect().Contains(this._player.TryMoveRect(0, 0, (int)_slidePad.CurrentValue.Y, 0)))
            {
                this._player.MoveRect(0, 0, (int)_slidePad.CurrentValue.Y / 3, 0);
                
            }
            if (this.GetRect().Contains(this._player.TryMoveRect(0, 0, 0, (int)_slidePad.CurrentValue.X)))
            {
                this._player.MoveRect(0, 0, 0, (int)_slidePad.CurrentValue.X / 3);

            }

            if (_enemyList.Where(q => q.GetRect().Intersects(this._player.GetRect()) && q.IsVisible).Any())
            {
                this._player.Damage();
            }

			foreach (var bullet in _player.BulletList.Where(q=>q.IsVisible).ToList()) 
			{
				foreach (var enemy in _enemyList.Where(q=>q.IsVisible).ToList()) 
				{
                    if (bullet.GetRect().Intersects(enemy.GetRect())) 
					{
						bullet.Hit ();
						enemy.Die ();
						this._score.Value++;
					}
				}
			}

			if (this._screenElapsedTime > TimeSpan.FromSeconds (20)) 
			{
				this.Navigate (new ResultScreen(),this._score.Value);
			}
        }

        public override void Draw(FrameContext context)
        {
            
            base.Draw(context);
        }

    }
}
