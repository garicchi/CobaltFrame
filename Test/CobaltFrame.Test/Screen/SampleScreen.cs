using CobaltFrame.Animation;
using CobaltFrame.Context;
using CobaltFrame.Object;
using CobaltFrame.Position;
using CobaltFrame.Screen;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Test.Screen
{
    public class SampleScreen:GameScreen
    {
        protected Texture2DObject _texture2DObj;
        
        public SampleScreen(GameContext context)
            : base(context)
        {
            this._texture2DObj = new Texture2DObject(context,new Position2D(new Rectangle(100,100,100,100)),"face");

            Position2DAnimation animation = new Position2DAnimation(context, TimeSpan.FromSeconds(2), new Position2D(new Rectangle(0, 0, 100, 100)), new Position2D(new Rectangle(200, 200, 100, 100)));
            animation.Chain(new WaitPosition2DAnimation(context, TimeSpan.FromSeconds(1), new Position2D(new Rectangle(200, 200, 100, 100))), (progress) =>
            {
                Debug.WriteLine("Wait");
            })
            .Chain(new Position2DAnimation(context, TimeSpan.FromSeconds(3), new Position2D(new Rectangle(200, 200, 100, 100)), new Position2D(new Rectangle(400, 100, 100, 100))), (progress) =>
            {
            })
            .Chain(() =>
            {
                Debug.WriteLine("Complete");
            });


            this._texture2DObj.AttachAnimation(animation);
            this.AddDrawableObject(this._texture2DObj);
            animation.Start();
        }

        
    }
}
