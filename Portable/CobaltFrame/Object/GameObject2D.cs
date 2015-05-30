using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public class GameObject2D:GameObject,IGameObject2D,IComparable
    {
        public GameObject2D()
        {
            
            this._rect = new Rectangle(0, 0, 100, 100);

            this.OnPositionChanged += (pos) => { };
            this.OnSizeChanged += (size) => { };
        }

        #region Event

        public event Action<Point> OnPositionChanged;

        public event Action<Point> OnSizeChanged;
        #endregion

        #region Field

        
        protected Rectangle _rect;
        
        
        #endregion

        #region Property

        #endregion

        #region Method
        public override void Draw(Context.FrameContext context)
        {
            //もし子要素に一つでもレイヤー変更があったなら
            if (this._children.Any(q => q.IsObjectLayerChanged))
            {
                //子要素をソート
                this._children.Sort();

                //ソートが完了したら子要素のフラグを戻す
                for (int i = 0; i < this._children.Count; i++)
                {
                    this._children.ElementAt(i).IsObjectLayerChanged = false;
                }
            }
            base.Draw(context);
        }

        /// <summary>
        /// レイヤーソートをするための比較関数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            //LayerDepthの数が大きくなるほど後に描画されるようにする
            var gObj = (IGameObject)obj;
            if (gObj.LayerDepth < this.LayerDepth) { return -1; }
            if (gObj.LayerDepth > this.LayerDepth) { return 1; }
            if (gObj.LayerDepth == this.LayerDepth) { return 0; }

            return 0;
        }

        public virtual void SetPosition(Point pos)
        {
            this._rect.X = pos.X;
            this._rect.Y = pos.Y;

            this.OnPositionChanged(pos);
        }

        public virtual void SetSize(Point size)
        {
            this._rect.Width = size.X;
            this._rect.Height = size.Y;

            this.OnSizeChanged(size);
        }

        public virtual void SetRect(Rectangle rect)
        {
            this.SetPosition(rect.GetPosition());
            this.SetSize(rect.Size);
        }

        public virtual Rectangle GetRect()
        {
            if ((this._parent != null) && (this._parent is GameObject2D))
            {
                //親要素があって、親要素がGameObject2Dなら絶対座標を返す
                return new Rectangle(
                    (this._parent as IGameObject2D).GetRect().X + this._rect.X,
                    (this._parent as IGameObject2D).GetRect().Y + this._rect.Y,
                    this._rect.Width,
                    this._rect.Height
                    );

            }
            else
            {
                //親要素がないかGameObject2Dじゃないならそのまま座標を返す
                return this._rect;
            }
        }

        public virtual Rectangle GetRelativeRect()
        {
            //親要素がないかGameObject2Dじゃないならそのまま座標を返す
            return this._rect;
            
        }
        
        /// <summary>
        /// 各4方向に移動する
        /// </summary>
        /// <param name="up"></param>
        /// <param name="left"></param>
        /// <param name="down"></param>
        /// <param name="right"></param>
        public virtual void MoveRect(int up = 0, int left = 0, int down = 0, int right = 0)
        {
            var rect = new Point(this.GetRect().X, this.GetRect().Y);
            rect.X -= left;
            rect.X += right;
            rect.Y -= up;
            rect.Y += down;
            this.SetPosition(rect);
        }

        /// <summary>
        /// 各4方向に移動した結果を返す(実際には移動しない)
        /// </summary>
        /// <param name="up"></param>
        /// <param name="left"></param>
        /// <param name="down"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public virtual Rectangle TryMoveRect(int up = 0, int left = 0, int down = 0, int right = 0)
        {
            var rect = this.GetRect();
            rect.X -= left;
            rect.X += right;
            rect.Y -= up;
            rect.Y += down;
            return rect;
        }

        public virtual Point GetPosition()
        {
            var rect = this.GetRect();
            return new Point(rect.X, rect.Y);
        }

        public virtual Point GetSize()
        {
            var rect = this.GetRect();
            return new Point(rect.Width, rect.Height);
        }

        public virtual Point GetRelativePosition()
        {
            var rect = this.GetRelativeRect();
            return new Point(rect.X, rect.Y);
        }

        public virtual void SetAbsolutePosition(Point pos)
        {
            this.SetAbsoluteRect(new Rectangle(pos.X,pos.Y,this._rect.Width,this._rect.Height));
        }

        public virtual void SetAbsoluteRect(Rectangle rect)
        {
            if ((this._parent != null) && (this._parent is GameObject2D))
            {
                //親要素があって、親要素がGameObject2Dなら絶対座標を返す
                this.SetRect(new Rectangle(
                    rect.X - (this._parent as IGameObject2D).GetRect().X,
                    rect.Y - (this._parent as IGameObject2D).GetRect().Y,
                    rect.Width,
                    rect.Height
                    ));

            }
            else
            {
                //親要素がないかGameObject2Dじゃないならそのまま座標を返す
                this.SetRect (rect);
            }
        }


        public virtual Point GetCenter()
        {
            var rect = this.GetRect();
            return new Point(rect.X+rect.Width/2, rect.Y+rect.Height/2);
        }
        #endregion


    }
}
