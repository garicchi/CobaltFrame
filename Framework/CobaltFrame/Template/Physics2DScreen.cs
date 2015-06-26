using CobaltFrame.Input;
using CobaltFrame.Screen;
using CobaltFrame.UI;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CobaltFrame.Universal.Test
{
    public class Physics2DScreen:FarseerPhysicsScreen
    {
        Texture2DCirclePhysicsObject _physicsObject;
        public Physics2DScreen()
            :base(new Vector2(0,1.0f))
        {
            this._physicsObject = new Texture2DCirclePhysicsObject("System/Texture/sample_player",this.World,100,1f);
            this._physicsObject.Body.Position = ConvertUnits.ToSimUnits(400,400);
            this._physicsObject.Body.IsStatic = false;
            this.AddChild(this._physicsObject);
            this.DebugView.Enabled = true;
            
            Body ground = BodyFactory.CreateRectangle(this.World,7f,1f,1f,new Vector2(5f,6f));
            ground.Rotation = 0.2f;
        }

        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
            
        }
    }
}
