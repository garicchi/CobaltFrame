using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.UI;
using CobaltFrame.Object;
using CobaltFrame.Context;
using CobaltFrame.Common;

namespace CobaltFrame.UI
{
    /// <summary>
    /// ビットマップフォントでテキストを描画するオブジェクト
    /// </summary>
    public class BitmapTextObject:GameObject2D
    {
        public BitmapTextObject(string fontPath, string text, float fontScale, Color color)
        {
            this._fontPath = fontPath;
            this._text = text;
            this._drawColor = color;
            this._fontScale = fontScale;

            this._fontTextures = new List<Texture2D>();
            this._charactorDic = new Dictionary<char, FontChar>();

        }

        #region Field
        //フォントファイル(.fnt)へのパス
        protected string _fontPath;
        //描画テキスト
        private string _text;
        //フォントテクスチャの数
        private int _fontTextureSize;
        //フォントファイル
        protected FontFile _fontFile;
        //フォントテクスチャのコレクション
        protected List<Texture2D> _fontTextures;
        //描画可能文字の辞書
        protected Dictionary<char, FontChar> _charactorDic;
        //フォントのスケール
        protected float _fontScale;
        //SpriteBatch
        protected SpriteBatch _spriteBatch;
        //描画色
        protected Color _drawColor;
        #endregion

        #region Property

        public string FontPath
        {
            get { return _fontPath; }
        }


        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        public float FontScale
        {
            get { return _fontScale; }
            set { _fontScale = value; }
        }

        private float _rowOffset;

        public float RowOffset
        {
            get { return _rowOffset; }
            set { _rowOffset = value; }
        }

        public Color DrawColor
        {
            get { return this._drawColor; }
            set { this._drawColor = value; }
        }


        #endregion

        #region Method

        public override void Init()
        {
            base.Init();

            this._fontTextureSize = 1;
            this._rowOffset = 5.0f;
        }

        public override void Load()
        {
            base.Load();
            this._fontTextures.Clear();
            //フォントファイルをロード
            this._fontFile = ResourceContext.LoadWithoutManager<FontFile>(this._fontPath, () => FontLoader.Load(this._fontPath));
            //フォントテクスチャの数だけテクスチャファイルをロードする
            for (int i = 0; i < _fontTextureSize; i++)
            {
                //フォントテクスチャのファイル名に対応させている
                var format = "{0:D1}";
                if (this._fontTextureSize < 10)
                {
                    format = "{0:D1}";
                }
                else
                {
                    format = "{0:D2}";
                }
                //フォントテクスチャをロードする
                var texture = ResourceContext.Load<Texture2D>(this._fontPath + "_" + string.Format(format, i));
                
                //ロードしたテクスチャを追加
                this._fontTextures.Add(texture);
            }

            //描画可能文字の辞書を作る
            this._charactorDic.Clear();
            foreach (var c in this._fontFile.Chars)
            {
                FontChar fc = null;

                if (!this._charactorDic.TryGetValue((char)c.ID, out fc))
                {
                    this._charactorDic.Add((char)c.ID, c);
                }

            }

            this._spriteBatch = new SpriteBatch(this._game.GraphicsDevice);
        }

        public override void Unload()
        {
            base.Unload();

        }

        public override void Draw(FrameContext context)
        {
            base.Draw(context);

            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, context.ScreenTrans);
            //初期座標を保持
            var charPos = this.GetRect().GetPosition();
            //描画テキストを1文字ずつ取り出して
            foreach (var c in this._text)
            {
                FontChar fc = null;
                //描画可能な文字なら
                if (this._charactorDic.TryGetValue(c, out fc))
                {
                    //テクスチャを取得
                    var texture = this._fontTextures.ElementAt(fc.Page);
                    //描画
                    this._spriteBatch.Draw(texture,
                        new Rectangle((int)(charPos.X + fc.XOffset * this._fontScale), (int)(charPos.Y + fc.YOffset * this._fontScale), (int)(fc.Width * this._fontScale), (int)(fc.Height * this._fontScale)),
                        new Rectangle(fc.X, fc.Y, fc.Width, fc.Height), this._drawColor);
                    //次の文字を描画するために座標を移動
                    charPos.X += (int)(fc.XAdvance * this._fontScale);
                    if (charPos.X > (this._rect.X + this._rect.Width))
                    {
                        charPos.Y += (int)(fc.Height * this._fontScale + _rowOffset);
                        charPos.X = this._rect.X;
                    }
                }
            }
            this._spriteBatch.End();
        }

        #endregion


    }
}
