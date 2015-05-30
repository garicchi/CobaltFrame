using CobaltFrame.Animation;
using CobaltFrame.Context;
using CobaltFrame.Mono.Sound;
using CobaltFrame.Screen;
using CobaltFrame.UI;
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
        BitmapTextObject _text;
        SoundEffectObject _sound;
        
        public TitleScreen()
        {
            this._player = new Texture2DObject( "System/Texture/player");
            this.SetRect(new Rectangle(100, 100, 150, 150));
            this.AddChild(this._player);
            
            

            this._sound = new SoundEffectObject("se");
            this.AddChild(this._sound);
            
            

            //座標、フォントパス、描画テキスト、大きさ、描画色
            this._text = new BitmapTextObject("System/Font/meiryo","Hello,World",2,Color.White);
            this.SetRect(new Rectangle(300,300,400,100));
            this.AddChild(this._text);
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
        public override void Update(FrameContext context)
        {
            base.Update(context);

            
        }

        public override void Draw(FrameContext context)
        {
            base.Draw(context);
        }

        public override void NavigateTo(object parameter,IScreenTransition transition = null)
        {
            base.NavigateTo(parameter, transition);
        }
    }
}
