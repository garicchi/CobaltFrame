using CobaltFrame.Screen;
using CobaltFrame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame
{
    //最初のゲーム画面
    public class TitleScreen:GameScreen
    {
        public TitleScreen()
        {
            //var obj = new TextButtonObject("press!", "System/Font/ipagothic", "System/Texture/button_on", "System/Texture/button_off");
            //this.AddChild(obj);

            var slidepad = new SlidePadObject("System/Texture/slidepad_pad","System/Texture/slidepad_back");
            this.AddChild(slidepad);
        }

        /// <summary>
        /// コンテンツ初期化
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// リソースのロード
        /// </summary>
        public override void Load()
        {
            base.Load();
        }

        /// <summary>
        /// リソースの解放
        /// </summary>
        public override void Unload()
        {
            base.Unload();
        }

        /// <summary>
        /// コンテンツ更新(60fps)
        /// </summary>
        /// <param name="context"></param>
        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
        }

        /// <summary>
        /// コンテンツ描画(60fps)
        /// </summary>
        /// <param name="context"></param>
        public override void Draw(Context.FrameContext context)
        {
            base.Draw(context);
        }

        /// <summary>
        /// 別の画面から遷移したとき
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="transition"></param>
        public override void NavigateTo(object parameter, IScreenTransition transition = null)
        {
            base.NavigateTo(parameter, transition);
        }

    }
}
