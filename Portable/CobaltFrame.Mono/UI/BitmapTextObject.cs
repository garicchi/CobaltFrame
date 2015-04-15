using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Object;
using CobaltFrame.Mono.Font;
using CobaltFrame.Mono.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.UI
{
    public class BitmapTextObject:DrawableGameObject
    {
        protected string _fontPath;

        public string FontPath
        {
            get { return _fontPath; }
        }
        private string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private int _fontTextureSize;

        protected FontFile _fontFile;

        protected List<Texture2D> _fontTextures;

        protected Dictionary<char, FontChar> _charactorDic;

        protected float _fontScale;

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

        public BitmapTextObject(GameContext context,Box2 pos,string fontPath,string text,float fontScale,Color color)
            : base(context,pos)
        {
            this._fontPath = fontPath;
            this._text = text;

            this._fontTextureSize = 1;
            this._fontTextures = new List<Texture2D>();
            this._origin = Vector2.Zero;
            this._charactorDic = new Dictionary<char, FontChar>();
            this._drawColor = color;
            this._fontScale = fontScale;
            this._rowOffset = 5.0f;
        }

        public override void Init()
        {
            
            base.Init();
            
        }

        public override void Load()
        {
            base.Load();
            this._fontTextures.Clear();
            
            var fontFilePath = Path.Combine(this._game.Content.RootDirectory, this._fontPath+".fnt");
            using (var stream = TitleContainer.OpenStream(fontFilePath))
            {
                this._fontFile = FontLoader.Load(stream);
                for(int i=0;i<_fontTextureSize;i++)
                {
                    var format = "{0:D1}";
                    if (this._fontTextureSize < 10)
                    {
                        format = "{0:D1}";
                    }
                    else
                    {
                        format = "{0:D2}";
                    }
                    var texture=this._game.Content.Load<Texture2D>(this._fontPath+"_"+string.Format(format,i));
                    this._fontTextures.Add(texture);
                }
            }
            this._charactorDic.Clear();
            foreach (var c in this._fontFile.Chars)
            {
                FontChar fc = null;

                if (!this._charactorDic.TryGetValue((char)c.ID,out fc))
                {
                    this._charactorDic.Add((char)c.ID, c);
                }
                
            }
            
        }

        public override void Unload()
        {
            base.Unload();
            
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            base.Draw(context);

            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, (context as FrameContext).ScreenTrans);

            var charPos = this._position.GetLocation();
            foreach (var c in this._text)
            {
                FontChar fc = null;
                if (this._charactorDic.TryGetValue(c, out fc))
                {
                    var texture = this._fontTextures.ElementAt(fc.Page);
                    this._spriteBatch.Draw(texture,
                        new Rectangle((int)(charPos.X + fc.XOffset * this._fontScale), (int)(charPos.Y + fc.YOffset * this._fontScale), (int)(fc.Width * this._fontScale), (int)(fc.Height * this._fontScale)),
                        new Rectangle(fc.X,fc.Y,fc.Width,fc.Height), this._drawColor);
                    charPos.X += fc.XAdvance * this._fontScale;
                    if (charPos.X > this.Position.GetRect().Width)
                    {
                        charPos.Y+=fc.Height * this._fontScale+_rowOffset;
                        charPos.X = this.Position.GetRect().X;
                    }
                }
            }
            this._spriteBatch.End();
        }
    }
}
