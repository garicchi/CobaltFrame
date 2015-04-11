using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Input
{
    public class InputCondition
    {
        public InputCondition(
            string stateName,
            Func<bool> touchCondition,
            Func<bool> mouseCondition,
            Func<bool> gamePadCondition,
            Func<bool> keyboardCondition,
            Func<bool> accelCondition
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
        public Func<bool> TouchCondition { get; set; }
        public Func<bool> MouseCondition { get; set; }
        public Func<bool> GamePadCondition { get; set; }
        public Func<bool> KeyboardCondition { get; set; }
        public Func<bool> AccelCondition { get; set; }
       public bool IsInput { get; set; }
    }
}
