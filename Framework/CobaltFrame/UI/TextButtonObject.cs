using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.UI
{
    //テキストつきボタン
    public class TextButtonObject:ButtonObject
    {

        protected BitmapTextObject _textObject;

        public TextButtonObject(string text,string fontPath,string pressedTexturePath,string releasedTexturePath)
            :base(pressedTexturePath,releasedTexturePath)
        {
            this._textObject = new BitmapTextObject(fontPath,text,2,Color.White);
            this._textObject.SetRect(new Rectangle(10,10,200,80));
            this._textObject.LayerDepth = 0.4f;
            this.AddChild(this._textObject);
        }

        public BitmapTextObject TextObject
        {
            get { return this._textObject; }
            set { this._textObject = value; }
        }

        public override void SetSize(Point size)
        {

            base.SetSize(size);
        }
    }
}
