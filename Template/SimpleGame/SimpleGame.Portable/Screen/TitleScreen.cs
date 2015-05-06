using CobaltFrame.Mono.Position;
using CobaltFrame.Mono.Screen;
using CobaltFrame.Mono.UI;
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
        
        public TitleScreen()
        {
            this._player = new Texture2DObject(new Box2(100, 100, 150, 150), "System/Texture/player");
            this.AddDrawableObject(this._player);
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
