using CobaltFrame.Screen;
using CobaltFrame.UI;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CobaltFrame.Universal.Test
{
    public class Physics2DScreen:FarseerPhysicsScreen
    {
        Texture2DPhysicsObject _physicsObject;
        public Physics2DScreen()
            :base(new Vector2(0,1.0f))
        {
            this._physicsObject = new Texture2DPhysicsObject("System/Texture/sample_player",BodyFactory.CreateRectangle(this.World,1f,1f,1f));
            this._physicsObject.Body.IsStatic = false;
            this.AddChild(this._physicsObject);
            this.DebugView.Enabled = true;

            Body ground = BodyFactory.CreateRectangle(this.World,3f,1f,1f,new Vector2(1.8f,2f));
        }

        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
            
        }
    }
}
