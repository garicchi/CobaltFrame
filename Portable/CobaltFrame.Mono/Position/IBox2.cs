using CobaltFrame.Core.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Position
{
    public interface IBox2
    {
        void SetRect(Rectangle rect);
        Rectangle GetRect();
        Rectangle GetRect(Vector2 origin);
        void SetLocation(Vector2 vec);
        Vector2 GetLocation();
        Vector2 GetCenter();
        void SetCenter(Vector2 vec);
        void MoveRect(int up=0,int left=0,int down=0,int right=0);
        IBox2 TryMoveRect(int up = 0, int left = 0, int down = 0, int right = 0);
        bool Contains(int x,int y);
        bool Contains(Vector2 vec);
        bool Contains(IBox2 box);
        bool Intersects(IBox2 box);

        RelativeBox2 GetRelativeBox(Margin margin);
    }
}
