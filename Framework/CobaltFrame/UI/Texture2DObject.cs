using CobaltFrame.Context;
using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Common;
using CobaltFrame.UI;
using System.Diagnostics;

namespace CobaltFrame.UI
{
    /// <summary>
    /// Texture2Dを描画するゲームオブジェクト
    /// </summary>
    public class Texture2DObject : GameObject2D
    {
        public Texture2DObject(string texturePath)
        {
            this._texturePath = texturePath;
            this._origin = Vector2.Zero;
            this._textureScale = new Vector2(1.0f, 1.0f);
            this._rotation = 0.0f;
            this._drawColor = Color.White;
            this._sourceRect = new Rectangle(0, 0, 100, 100);
            this._isAbsolutePosition = false;
        }

        #region Field
        /// <summary>
        /// テクスチャへのパス
        /// </summary>
        protected string _texturePath;
        protected Vector2 _origin;
        protected SpriteBatch _spriteBatch;
        private Vector2 _textureScale;
        protected Color _drawColor;
        protected float _rotation;
        protected Rectangle _sourceRect;
        /// <summary>
        /// テクスチャ
        /// </summary>
        protected Texture2D _texture;
        /// <summary>
        /// 絶対座標で描画するかどうか(相対なら親の座標に依存する)
        /// </summary>
        protected bool _isAbsolutePosition;
        #endregion

        #region Property

        protected string TexturePath
        {
            get { return _texturePath; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Vector2 TextureScale
        {
            get { return this._textureScale; }
        }

        public Vector2 Origin
        {
            get
            {
                return this._origin;
            }
            set
            {
                this._origin = value;
            }
        }

        public SpriteBatch SpriteBatch
        {
            get { return this._spriteBatch; }
        }
        public Color DrawColor
        {
            get
            {
                return this._drawColor;
            }
            set
            {
                this._drawColor = value;
            }
        }

        public float Rotation
        {
            get
            {
                return this._rotation;
            }
            set
            {
                this._rotation = value;
            }
        }

        public Rectangle SourceRect
        {
            get { return this._sourceRect; }
        }

        /// <summary>
        /// 絶対座標で描画するかどうか(相対なら親の座標に依存する)
        /// </summary>
        public bool IsAbsolutePosition
        {
            get { return this._isAbsolutePosition; }
            set { this._isAbsolutePosition = value; }
        }
        
        #endregion

        #region Method

        public override void Init()
        {
            base.Init();

        }
        public override void Load()
        {
            base.Load();
            //ResourceContextでテクスチャをロード
            this._texture = ResourceContext.Load<Texture2D>(this._texturePath);
            //描画座標原点は最初テクスチャサイズの半分の位置にあるので0,0にする
            this._origin = new Vector2(this._texture.Width / 2.0f, this._texture.Height / 2.0f);
            
            this._sourceRect = this._texture.Bounds;
            this._spriteBatch = new SpriteBatch(this._game.GraphicsDevice);

            UpdateSize();
            
        }


        public override void Unload()
        {
            base.Unload();
            this._spriteBatch.Dispose();
        }

        public override void Update(FrameContext context)
        {

            base.Update(context);
        }

        public override void Draw(FrameContext context)
        {
            base.Draw(context);

            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, context.ScreenTrans);
            //描画

            //絶対座標で描画するかどうか
            if (this._isAbsolutePosition)
            {
                this._spriteBatch.Draw(this._texture, null, this.GetRelativeRect().ShiftRect(this._origin * this._textureScale), this._sourceRect, this._origin, this._rotation, null, this._drawColor, SpriteEffects.None, 0.0f);
            }
            else
            {
                this._spriteBatch.Draw(this._texture, null, this.GetRect().ShiftRect(this._origin * this._textureScale), this._sourceRect, this._origin, this._rotation, null, this._drawColor, SpriteEffects.None, 0.0f);
            }
            
            this._spriteBatch.End();
            
        }

        public override void SetSize(Point size)
        {
            base.SetSize(size);

            UpdateSize();
        }

        /// <summary>
        /// オブジェクトのサイズが変更されたときに再計算
        /// </summary>
        protected virtual void UpdateSize()
        {
            if (Texture != null)
            {
                //現在のサイズに対してどれだけ引き延ばせばいいか計算
                this._textureScale = new Vector2((float)this.GetRect().Width / (float)this._texture.Width, (float)this.GetRect().Height / (float)this._texture.Height);
            }
        }

        #endregion



    }
}
