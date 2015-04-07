using CobaltFrame.Context;
using CobaltFrame.Core.Context;
using CobaltFrame.Input;
using CobaltFrame.Object;
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

namespace HorizontalShootingGame.Portable.Object
{
    public class Player : Texture2DObject
    {

        public Player(GameContext context, Position2D position, string texturePath)
            : base(context, position, texturePath)
        {

            //プレイヤーを上に動かすという入力概念を登録
            GameInput.RegisterInputState("PlayerMove",
                //タッチ入力条件
                () => GameInput.TouchCollection.Where(q => q.State == TouchLocationState.Moved).Count() != 0,
                //ゲームパッド入力は必要ないのでnull
                null,
                //マウス入力も必要ないのでnull
                null,
                //キーボード入力条件
                () => GameInput.KeyboardState.IsKeyDown(Keys.Up),
                //加速度センサー入力条件
                () => GameInput.AccelState.Accel.X > 0
            );

        }

        public override void Update(IFrameContext context)
        {
            base.Update(context);

            //プレイヤーを上に動かす入力条件のどれかがOKなら
            if (GameInput.IsInput("PlayerMove"))
            {
                //プレイヤーを上に動かす
                this.Position.SetLocation(new Vector2(this.Position.GetLocation().X, this.Position.GetLocation().Y - 1));
            }

        }
    }
}
