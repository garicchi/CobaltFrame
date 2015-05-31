using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Input
{
    public class InputCondition
    {
        public InputCondition(
            string stateName,
            Func<TouchInputCollection, TouchInputCollection, bool> touchCondition = null,
            Func<MouseState, MouseState, bool> mouseCondition = null,
            Func<GamePadState[], GamePadState[], bool> gamePadCondition = null,
            Func<KeyboardState, KeyboardState, bool> keyboardCondition = null,
            Func<AccelerometerState, AccelerometerState, bool> accelCondition = null
            )
        {
            this.TouchCondition = touchCondition;
            this.MouseCondition = mouseCondition;
            this.GamePadCondition = gamePadCondition;
            this.KeyboardCondition = keyboardCondition;
            this.AccelCondition = accelCondition;
            this.IsInput = false;
            this.StateName = stateName;
        }
        public string StateName{get;set;}
        public Func<TouchInputCollection, TouchInputCollection, bool> TouchCondition { get; set; }
        public Func<MouseState, MouseState, bool> MouseCondition { get; set; }
        public Func<GamePadState[], GamePadState[], bool> GamePadCondition { get; set; }
        public Func<KeyboardState, KeyboardState, bool> KeyboardCondition { get; set; }
        public Func<AccelerometerState, AccelerometerState, bool> AccelCondition { get; set; }
       public bool IsInput { get; set; }
    }
}
