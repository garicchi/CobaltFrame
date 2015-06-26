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
    public abstract class FarseerPhysicsScreen:GameScreen
    {
        private DebugViewXNA _debugView;

        protected DebugViewXNA DebugView
        {
            get { return _debugView; }
            set { _debugView = value; }
        }


        protected World _world;
        public World World
        {
            get { return _world; }
            set { _world = value; }
        }

        protected Matrix _simProjection;

        public bool IsDebugViewEnabled 
        {
            get
            {
                return this.DebugView.Enabled;
            }
            set
            {
                this.DebugView.Enabled = value;
            }
        }
        
        public FarseerPhysicsScreen(Microsoft.Xna.Framework.Vector2 gravity)
        {
            this._world = new World(gravity);
            this._debugView = new DebugViewXNA(this._world);
            this._debugView.DefaultShapeColor = Color.White;
            this._debugView.SleepingShapeColor = Color.LightGray;
            
        }

        public override void Init()
        {
            base.Init();
            this._simProjection = Matrix.CreateOrthographicOffCenter(0f, ConvertUnits.ToSimUnits(_game.GraphicsDevice.Viewport.Width), ConvertUnits.ToSimUnits(_game.GraphicsDevice.Viewport.Height), 0f, 0f, 1f);
            
        }

        public override void Load()
        {
            base.Load();
            
            this._debugView.LoadContent(this._game.GraphicsDevice,this._game.Content);
            
            
        }

        public override void Update(Context.FrameContext context)
        {
            base.Update(context);
            
            this._world.Step(Math.Min((float)context.ElapsedGameTime.TotalSeconds, (1f / 30f)));
             
        }

        public override void Draw(Context.FrameContext context)
        {
            base.Draw(context);
            Matrix simView = Matrix.CreateTranslation(new Vector3(ConvertUnits.ToSimUnits(-this.Camera2D.Position.X), ConvertUnits.ToSimUnits(-this.Camera2D.Position.Y), 0)) *
                                         Matrix.CreateRotationZ(this._camera2D.Rotation) *
                                         Matrix.CreateScale(new Vector3(this._camera2D.Zoom, this._camera2D.Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(ConvertUnits.ToSimUnits(this._game.GraphicsDevice.Viewport.Width * 0.5f),ConvertUnits.ToSimUnits( this._game.GraphicsDevice.Viewport.Height * 0.5f), 0));
            this._debugView.RenderDebugData(ref this._simProjection, ref simView);
        }
    }
}
