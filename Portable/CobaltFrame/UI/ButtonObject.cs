using CobaltFrame.Input;
using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Common;
using CobaltFrame.Context;

namespace CobaltFrame.Mono.UI
{
    /// <summary>
    /// PressとReleaseでテクスチャを変えるボタンゲームオブジェクト
    /// </summary>
    public class ButtonObject:GameObject
    {
        public ButtonObject(string pressedTexturePath, string releasedTexturePath)
        {
            this._pressedObject = new Texture2DObject(pressedTexturePath);
            this._releasedObject = new Texture2DObject(releasedTexturePath);

            this.AddChild(this._pressedObject);
            this.AddChild(this._releasedObject);

            this.OnClick += (pos) => { };
        }
        #region Event

        public event Action<Point> OnClick;

        #endregion

        #region Field
        protected ButtonState _state;
        protected ButtonState _beforeState;

        protected Texture2DObject _pressedObject;
        protected Texture2DObject _releasedObject;

        #endregion

        #region Property
        public ButtonState State
        {
            get { return _state; }
        }
        #endregion

        #region Method

        public override void Init()
        {
            base.Init();

            this._state = ButtonState.Released;
            this._beforeState = ButtonState.Released;
        }

        public override void Load()
        {
            base.Load();
            
            //ボタンが押されたときの条件を登録
            this.Inputs.RegisterInput("_ButtonObjectOnClick",
                () =>
                {
                    if (InputContext.TouchCollection.Where(q => q.State == TouchLocationState.Pressed).Count() != 0
                        && InputContext.TouchCollection.Where(q => this._rect.Contains((int)q.Position.X, (int)q.Position.Y)).Count() != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                },
                () => InputContext.MouseState.LeftButton == ButtonState.Pressed
                    && InputContext.MouseStatePrev.LeftButton == ButtonState.Released
                    && this._rect.Contains(InputContext.MouseState.Position.X, InputContext.MouseState.Position.Y)
            );

            this.Inputs.RegisterInput("_ButtonObjectPressed",
                () =>
                {
                    if (InputContext.TouchCollection.Where(q => q.State == TouchLocationState.Pressed || q.State == TouchLocationState.Moved).Count() != 0
                        && InputContext.TouchCollection.Where(q => this._rect.Contains((int)q.Position.X, (int)q.Position.Y)).Count() != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                },
                () => InputContext.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                    && this._rect.Contains(InputContext.MouseState.Position.X, InputContext.MouseState.Position.Y)
            );
        }

        public override void Unload()
        {
            this.Inputs.UnregisterInput("_ButtonObjectOnClick");
            this.Inputs.UnregisterInput("_ButtonObjectPressed");
            base.Unload();

        }

        public override void Update(FrameContext context)
        {
            base.Update(context);

            if (this.Inputs.IsInput("_ButtonObjectOnClick"))
            {
                OnClick(this._rect.GetPosition());
            }

            if (this.Inputs.IsInput("_ButtonObjectPressed"))
            {
                this._state = ButtonState.Pressed;
            }
            else
            {
                this._state = ButtonState.Released;
            }
        }

        public override void Draw(FrameContext context)
        {
            base.Draw(context);
            
            switch (this._state)
            {
                case ButtonState.Released:
                    this._releasedObject.IsVisible = true;
                    this._pressedObject.IsVisible = false;
                    break;
                case ButtonState.Pressed:
                    this._releasedObject.IsVisible = false;
                    this._pressedObject.IsVisible = true;
                    break;
            }
            
        }

        public override void SetSize(Point size)
        {
            base.SetSize(size);
            this._pressedObject.SetSize(size);
            this._releasedObject.SetSize(size);
        }

        #endregion


    }
}
