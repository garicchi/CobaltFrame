using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.UI
{
    public class RepeatableTexure2DObject:Texture2DObject
    {
        public RepeatableTexure2DObject(string texturePath)
            :base(texturePath)
        {

        }

        public override void Draw(Context.FrameContext context)
        {
            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, context.GetScreenTrans());

            int texW = (int)this._texture.Bounds.Width;
            int texH = (int)this._texture.Bounds.Height;

            for (int y = this.GetRect().Y; y < this.GetRect().Height + texH; y += texH)
            {
                for (int x = this.GetRect().X; x < this.GetRect().Width + texW; x += texW)
                {
                    this._spriteBatch.Draw(this._texture, new Vector2(x, y), null, this._texture.Bounds, this._origin, this._rotation, null, this._drawColor, SpriteEffects.None, 0.0f);
                }
            }

            this._spriteBatch.End();
        }
    }
}
