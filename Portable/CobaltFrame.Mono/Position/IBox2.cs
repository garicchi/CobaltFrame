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
        public void SetRect(Rectangle rect);
        public Rectangle GetRect();
        public void SetLocation(Vector2 vec);
        public Vector2 GetLocation();
        public Vector2 GetCenter();
        public void SetCenter(Vector2 vec);
        public void MoveRect(int up=0,int left=0,int down=0,int right=0);
        public IBox2 TryMoveRect(int up = 0, int left = 0, int down = 0, int right = 0);
        public bool Contains(int x,int y);
        public bool Contains(Vector2 vec);
        public bool Contains(IBox2 box);
        public bool Intersects(IBox2 box);
    }
}
