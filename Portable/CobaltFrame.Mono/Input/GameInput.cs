using CobaltFrame.Context;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Input
{
    public static class GameInput
    {
        public static TouchInputCollection TouchCollection = new TouchInputCollection();
        public static MouseState MouseState = new MouseState();
        public static GamePadState[] GamePadState = new GamePadState[4];
        public static KeyboardState KeyboardState = new KeyboardState();
        public static AccelerometerState AccelState = new AccelerometerState(Vector3.Zero);
        public static bool IsAccelEnable = false;
        private static Func<AccelerometerState> _accelFunc;

        public static TouchInputCollection TouchCollectionPrev = new TouchInputCollection();
        public static MouseState MouseStatePrev = new MouseState();
        public static GamePadState[] GamePadStatePrev = new GamePadState[4];
        public static KeyboardState KeyboardStatePrev = new KeyboardState();
        public static AccelerometerState AccelStatePrev = new AccelerometerState(Vector3.Zero);

        private static bool _isFirstUpdate = true;

        //入力概念のリスト
        private static List<InputCondition> _inputConditions = new List<InputCondition>();

        public static void RegisterInputState(
            string stateName,
            Func<bool> touchCondition = null,
            Func<bool> mouseCondition = null,
            Func<bool> gamePadCondition = null,
            Func<bool> keyboardCondition = null,
            Func<bool> accelCondition = null
            )
        {
            if(_inputConditions.Any(q => q.StateName == stateName))
            {
                throw new ArgumentException("StateNameが重複しています");
            }
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

        public static void UnregisterInputState(string stateName)
        {
            if (_inputConditions.Any(q => q.StateName == stateName))
            {
                var condition = _inputConditions.Where(q => q.StateName == stateName).First();
                _inputConditions.Remove(condition);
            }
        }

        public static void UnregisterAllInputState()
        {
            _inputConditions.Clear();
        }

        public static bool IsInput(string stateName)
        {
            if (!_inputConditions.Any(q => q.StateName == stateName))
            {
                return false;
            }
            return _inputConditions.Where(q => q.StateName == stateName).First().IsInput;
        }

        public static void SetupAccelState(Func<AccelerometerState> accelFunc)
        {
            IsAccelEnable = true;
            _accelFunc = accelFunc;
        }

        public static void Update(FrameContext context)
        {
            if (_isFirstUpdate)
            {
                _isFirstUpdate = false;
            }
            else
            {
                GamePadStatePrev = GamePadState;
                MouseStatePrev = MouseState;
                TouchCollectionPrev = TouchCollection;
                KeyboardStatePrev = KeyboardState;
                AccelStatePrev = AccelState;
            }

            if (TouchPanel.GetCapabilities().IsConnected)
            {
                var collection = TouchPanel.GetState();
                TouchCollection.Clear();
                foreach (TouchLocation state in collection)
                {
                    var location = new TouchLocation(
                        state.Id,
                        state.State,
                        Vector2.Transform(state.Position, context.GetScreenTransInvert())
                        );

                    TouchCollection.Add(location);
                }
            }

            var mouseState = Mouse.GetState();
            var mouseVec = new Vector2(mouseState.X, mouseState.Y);
            var mousePosTranslated = Vector2.Transform(mouseVec,context.GetScreenTransInvert());
            MouseState = new MouseState(
                (int)mousePosTranslated.X,
                (int)mousePosTranslated.Y,
                mouseState.ScrollWheelValue,
                mouseState.LeftButton,
                mouseState.MiddleButton,
                mouseState.RightButton,
                mouseState.XButton1,
                mouseState.XButton2
                );

            for (int i = 0; i < 4; i++)
            {
                if(GamePad.GetCapabilities((PlayerIndex)i).IsConnected){
                    GamePadState[i] = GamePad.GetState((PlayerIndex)i);
                }
            }

            KeyboardState = Keyboard.GetState();

            if (IsAccelEnable)
            {
                AccelState = _accelFunc();
            }

            foreach (var condition in _inputConditions)
            {
                var inputTouch = false;
                var inputKeyboard = false;
                var inputMouse = false;
                var inputPad = false;
                var inputAccel =false;
                if (TouchPanel.GetCapabilities().IsConnected&&condition.TouchCondition!=null)
                {
                    inputTouch = condition.TouchCondition();
                }
                if (condition.KeyboardCondition != null)
                {
                    inputKeyboard = condition.KeyboardCondition();
                }
                if (condition.MouseCondition != null)
                {
                    inputMouse = condition.MouseCondition();
                }
                if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected && condition.GamePadCondition != null)
                {
                    inputPad = condition.GamePadCondition();
                }
                if (IsAccelEnable && condition.AccelCondition != null)
                {
                    inputAccel = condition.AccelCondition();
                }

                condition.IsInput = inputTouch || inputKeyboard || inputMouse || inputPad || inputAccel ;
            }
            
        }
    }
}
