using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMLHT.Controls {
    public enum Dir
    {
        Z, N, NE, E, SE, S, SW, W, NW
    }

    public enum Joystick
    {
        ButtonA, ButtonB, ButtonX, ButtonY,
        Start, Back,
        BumperL, BumperR,
        TriggerL, TriggerR,
        UpL, RightL, DownL, LeftL,
        UpR, RightR, DownR, LeftR,
        UpD, RightD, DownD, LeftD
    }

    public enum AxisButtons
    {
        MouseXPlus, MouseXMinus,
        MouseYPlus, MouseYMinus,
        ScrollXPlus, ScrollXMinus,
        ScrollYPlus, ScrollYMinus,
        JoystickLeftXMinus, JoystickLeftXPlus,
        JoystickLeftYMinus, JoystickLeftYPlus,
        JoystickDpadXMinus, JoystickDpadXPlus,
        JoystickDpadYMinus, JoystickDpadYPlus,
        JoystickRightXMinus, JoystickRightXPlus,
        JoystickRightYMinus, JoystickRightYPlus,
        JoystickTriggerL, JoystickTriggerR
    }

    public enum Axis
    {
        Mouse, Scroll,
        KeysWASD, KeysArrows,
        JoystickLeft, JoystickDpad, JoystickRight
    }

    public enum Mouse
    {
        Left, Right, Middle
    }

    [System.Serializable]
    public class ControlButton
    {
        public string name;
        public Mouse[] keysMouse;
        public KeyCode[] keysKeyboard;
        public Joystick[] keysJoystick;
        public AxisButtons[] keysAxis;
    }

    [System.Serializable]
    public class ControlAxis
    {
        public string name;
        public AxisItem[] axisItems;
        [System.Serializable]
        public struct AxisItem {
            public Axis axis;
            public float coeff;
        }
    }

    public class ButtonAxis
    {
        public string axis;
        public float valOn;
        private float _val;
        public float val
        {
            get { return _val; }
            set
            {
                if (_val == 0f && value > 0f)
                {
                    isPressed = true;
                    wasPressed = true;
                }
                else if (value == 0f)
                {
                    isPressed = false;
                    wasPressed = false;
                    wasReleased = true;
                }
                else
                {
                    isPressed = true;
                    wasPressed = false;
                }
                _val = value;
            }
        }
        public bool isPressed;
        public bool wasPressed;
        public bool wasReleased;
    }

    public enum State
    {
        On, Pressed, Released
    }
}