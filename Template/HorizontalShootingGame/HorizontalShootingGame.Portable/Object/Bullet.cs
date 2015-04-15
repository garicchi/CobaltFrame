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
        protected Box2ConditionAnimation Animation { get; set; }


        public Bullet(GameContext context,Box2 pos,string texturePath)
            :base(context,pos,texturePath)
        {
            this.Animation = new Box2ConditionAnimation(context, pos, (current, time) =>
            {
                var newPos=new Box2(current);
                newPos.SetLocation(new Vector2((float)(current.GetLocation().X + 40), current.GetLocation().Y));
                return newPos;
            });
            this.Animation.StopTriggers.Add((current) =>
            {
                int width = this._game.GraphicsDevice.Viewport.Width;
                return (current.GetLocation().X > width);
                
            });
            this.Animation.OnCompleted += () =>
            {
                this.IsVisible = false;
            };
            this.AddObject(Animation);
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

        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Update(context);
            this.Position.SetLocation(this.Animation.CurrentValue.GetLocation());
        }
    }
}
