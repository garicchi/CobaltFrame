using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame
{
    public class SampleComponent:GameComponent
    {
        public SampleComponent(Game game)
            :base(game)
        {

        }
        public override void Initialize()
        {
            for (int i = 0; i < 20;i++ )
                Debug.WriteLine("hoge");
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
}
