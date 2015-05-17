using CobaltFrame.Core.Data;
using CobaltFrame.Mono;
using CobaltFrame.Mono.Animation;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Position;
using CobaltFrame.Mono.Screen;
using CobaltFrame.Mono.Sound;
using CobaltFrame.Mono.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SimpleGame.Portable.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame.Portable.Screen
{
    public class TitleScreen:GameScreen
    {
        Texture2DObject _player;
        Box2TimeAnimation _animation;
        BitmapTextObject _text;
        SoundEffectObject _sound;
        
        public TitleScreen()
        {
            this._player = new Texture2DObject(new Box2(100, 100, 150, 150), "System/Texture/player");
            this.AddDrawableObject(this._player);
            
            //100,100から300,400まで4秒間アニメーションさせる
            this._animation = new Box2TimeAnimation(TimeSpan.FromSeconds(4),new Box2(100,100,150,150),new Box2(300,400,150,150));
            this.AddObject(this._animation);

            this._sound = new SoundEffectObject("se");
            this.AddObject(this._sound);
            
            _animation.OnCompleted += () =>
            {
                this._sound.Play();
            };

            this._animation.Start();

            //座標、フォントパス、描画テキスト、大きさ、描画色
            this._text = new BitmapTextObject(new Box2(300,300,400,100),"System/Font/meiryo","Hello,World",2,Color.White);
            this.AddDrawableObject(this._text);
        }

        public override void Init()
        {
            base.Init();

        }

        public override void Load()
        {
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }
        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Update(context);

            this._player.Box = this._animation.CurrentValue;

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
