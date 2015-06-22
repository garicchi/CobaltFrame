using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using CobaltFrame.Context;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;
using System.Diagnostics;
using CobaltFrame.Physics;


namespace CobaltFrame.Screen
{
    public class FarseerPhysicsScreen:GameScreen
    {
        private DebugViewXNA _debugView;

        protected DebugViewXNA DebugView
        {
            get { return _debugView; }
            set { _debugView = value; }
        }
        private PhysicsCamera2D _camera;

        protected PhysicsCamera2D Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        protected World _world;
        public World World
        {
            get { return _world; }
            set { _world = value; }
        }
        public FarseerPhysicsScreen(Microsoft.Xna.Framework.Vector2 gravity)
        {
            this._world = new World(gravity);
            this._debugView = new DebugViewXNA(this._world);
            this._debugView.DefaultShapeColor = Color.White;
            this._debugView.SleepingShapeColor = Color.LightGray;
        }

        public override void Load()
        {
            base.Load();
            
            this._debugView.LoadContent(this._game.GraphicsDevice,this._game.Content);
            
            this._camera = new PhysicsCamera2D(this._game.GraphicsDevice);
        }

        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
            
            this._world.Step(Math.Min((float)context.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            this._camera.Update(context.TotalGameTime);
            
        }

        public override void Draw(Context.FrameContext context)
        {
            base.Draw(context);
            this._debugView.RenderDebugData(ref this._camera.SimProjection, ref this._camera.SimView);
        }
    }
}
