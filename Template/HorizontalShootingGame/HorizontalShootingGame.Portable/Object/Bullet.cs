using CobaltFrame.Mono.Animation;
using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Object
{
    public class Bullet:Texture2DObject
    {
        protected Position2DConditionAnimation Animation { get; set; }


        public Bullet(GameContext context,Position2D pos,string texturePath)
            :base(context,pos,texturePath)
        {
            this.Animation = new Position2DConditionAnimation(context, pos, (current, time) =>
            {
                var newPos=new Position2D(current);
                newPos.SetLocation(new Vector2((float)(current.GetLocation().X + current.GetLocation().X * time.TotalSeconds), current.GetLocation().Y));
                return newPos;
            });
            this.Animation.StopTriggers.Add((current) =>
            {
                int width = this._game.GraphicsDevice.Viewport.Width;
                this.IsVisible = false;
                return (current.GetLocation().X > width);
                
            });
            
        }

        public override void Initialize()
        {
            base.Initialize();
            this.IsVisible = false;
        }
        public void Shot()
        {
            this.IsVisible = true;
            this.Animation.Start();
        }
    }
}
