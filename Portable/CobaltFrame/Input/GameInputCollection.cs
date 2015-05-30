using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Input
{
    public class GameInputCollection
    {
        //入力概念のリスト
        private List<InputCondition> _inputConditions;

        public GameInputCollection()
        {
            _inputConditions = new List<InputCondition>();
        }
        public void RegisterInput(
            string stateName,
            Func<TouchInputCollection, TouchInputCollection, bool> touchCondition = null,
            Func<MouseState,MouseState,bool> mouseCondition = null,
            Func<GamePadState[],GamePadState[],bool> gamePadCondition = null,
            Func<KeyboardState,KeyboardState,bool> keyboardCondition = null,
            Func<AccelerometerState,AccelerometerState,bool> accelCondition = null
            )
        {
            if (!_inputConditions.Any(q => q.StateName == stateName))
            {
                _inputConditions.Add(new InputCondition(
                 stateName,
                 touchCondition,
                 mouseCondition,
                 gamePadCondition,
                 keyboardCondition,
                 accelCondition
                 )
                 );
            }


        }

        public void UnregisterInput(string stateName)
        {
            if (_inputConditions.Any(q => q.StateName == stateName))
            {
                var condition = _inputConditions.Where(q => q.StateName == stateName).First();
                _inputConditions.Remove(condition);
            }
        }

        public void UnregisterAllInput()
        {
            _inputConditions.Clear();
        }

        public bool IsInput(string stateName)
        {
            if (!_inputConditions.Any(q => q.StateName == stateName))
            {
                return false;
            }
            return _inputConditions.Where(q => q.StateName == stateName).First().IsInput;
        }

        public void Update()
        {
            foreach (var condition in _inputConditions)
            {
                var inputTouch = false;
                var inputKeyboard = false;
                var inputMouse = false;
                var inputPad = false;
                var inputAccel = false;
                if (TouchPanel.GetCapabilities().IsConnected && condition.TouchCondition != null)
                {
                    inputTouch = condition.TouchCondition(InputContext.TouchCollection,InputContext.TouchCollectionPrev);
                }
                if (condition.KeyboardCondition != null)
                {
                    inputKeyboard = condition.KeyboardCondition(InputContext.KeyboardState, InputContext.KeyboardStatePrev);
                }
                if (condition.MouseCondition != null)
                {
                    inputMouse = condition.MouseCondition(InputContext.MouseState, InputContext.MouseStatePrev);
                }
                if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected && condition.GamePadCondition != null)
                {
                    inputPad = condition.GamePadCondition(InputContext.GamePadState,InputContext.GamePadStatePrev);
                }
                if (InputContext.IsAccelEnable && condition.AccelCondition != null)
                {
                    inputAccel = condition.AccelCondition(InputContext.AccelState,InputContext.AccelStatePrev);
                }

                condition.IsInput = inputTouch || inputKeyboard || inputMouse || inputPad || inputAccel;
            }

        }
    }
}
