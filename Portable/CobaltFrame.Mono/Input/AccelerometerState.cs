using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Input
{
    public class AccelerometerState
    {
        public AccelerometerState(Vector3 accel)
        {
            this.Accel = accel;
        }
        public Vector3 Accel { get; set; }
    }
}
