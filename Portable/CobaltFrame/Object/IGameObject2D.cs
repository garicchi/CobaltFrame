using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public interface IGameObject2D:IGameObject
    {
        /// <summary>
        /// Positionが変更されたとき
        /// </summary>
        event Action<Point> OnPositionChanged;

        /// <summary>
        /// Sizeが変更されたとき
        /// </summary>
        event Action<Point> OnSizeChanged;

        /// <summary>
        /// 座標設定
        /// </summary>
        /// <param name="pos"></param>
        void SetPosition(Point pos);

        /// <summary>
        /// WidthとHeight指定
        /// </summary>
        /// <param name="size"></param>
        void SetSize(Point size);

        /// <summary>
        /// 描画矩形を指定
        /// </summary>
        /// <param name="rect"></param>
        void SetRect(Rectangle rect);

        /// <summary>
        /// 絶対座標で座標設定
        /// </summary>
        /// <param name="pos"></param>
        void SetAbsolutePosition(Point pos);

        /// <summary>
        /// 絶対座標で矩形指定
        /// </summary>
        /// <param name="rect"></param>
        void SetAbsoluteRect(Rectangle rect);

        /// <summary>
        /// 描画矩形を取得(親要素がある場合は親要素を含めた絶対座標となる)
        /// </summary>
        /// <returns></returns>
        Rectangle GetRect();

        /// <summary>
        /// ぜったい座標を取得
        /// </summary>
        /// <returns></returns>
        Point GetPosition();

        /// <summary>
        /// サイズを取得
        /// </summary>
        /// <returns></returns>
        Point GetSize();

        /// <summary>
        /// 相対座標なRectを取得
        /// </summary>
        /// <returns></returns>
        Rectangle GetRelativeRect();

        /// <summary>
        /// 相対座標を取得
        /// </summary>
        /// <returns></returns>
        Point GetRelativePosition();

        /// <summary>
        /// Rectの中央を取得
        /// </summary>
        /// <returns></returns>
        Point GetCenter();

        /// <summary>
        /// 各4方向にRectを移動させる
        /// </summary>
        /// <param name="up"></param>
        /// <param name="left"></param>
        /// <param name="down"></param>
        /// <param name="right"></param>
        void MoveRect(int up = 0, int left = 0, int down = 0, int right = 0);

        /// <summary>
        /// 各4方向にRectを移動してみた結果を返す(実際には移動しない)
        /// </summary>
        /// <param name="up"></param>
        /// <param name="left"></param>
        /// <param name="down"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        Rectangle TryMoveRect(int up = 0, int left = 0, int down = 0, int right = 0);

    }
}
