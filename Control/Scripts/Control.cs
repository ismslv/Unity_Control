/* CONTROL SYSTEM
 * MOUSE, KEYBOARD, GAMEPAD
 * V0.3
 * FMLHT, 04.2019
 */

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMLHT.Controls {

public class Control : MonoBehaviour
{
    public static Control a;

    public ControlData data;

    void Awake()
    {
        if (Control.a == null)
            a = this;
    }

    public static bool IsControl(string name, State state)
    {
        return (IsControl(a.data.Control(name), state));
    }

    public static Vector2 GetAxis(string name)
    {
        Vector2 axis;
        foreach (var a in a.data.Axis(name).axisItems) {
            axis = GetAxis(a.axis);
            if (axis != Vector2.zero) {
                axis *= a.coeff;
                return axis;
            }
        }
        return Vector2.zero;
    }

    public static bool IsControl(ControlButton control, State state)
    {
        if (control.keysKeyboard != null)
        {
            foreach (var k in control.keysKeyboard)
            {
                switch (state)
                {
                    case State.On:
                        if (Input.GetKey(k)) return true;
                        break;
                    case State.Pressed:
                        if (Input.GetKeyDown(k)) return true;
                        break;
                    case State.Released:
                        if (Input.GetKeyUp(k)) return true;
                        break;
                }
            }
        }

        if (control.keysMouse != null)
        {
            foreach (var k in control.keysMouse)
            {
                switch (state)
                {
                    case State.On:
                        if (Input.GetMouseButton((int)k)) return true;
                        break;
                    case State.Pressed:
                        if (Input.GetMouseButtonDown((int)k)) return true;
                        break;
                    case State.Released:
                        if (Input.GetMouseButtonUp((int)k)) return true;
                        break;
                }
            }
        }

        if (control.keysJoystick != null)
        {
            foreach (var k in control.keysJoystick)
            {
                switch (state)
                {
                    case State.On:
                        if (Input.GetButton(JoystickKey(k))) return true;
                        break;
                    case State.Pressed:
                        if (Input.GetButtonDown(JoystickKey(k))) return true;
                        break;
                    case State.Released:
                        if (Input.GetButtonUp(JoystickKey(k))) return true;
                        break;
                }
            }
        }

        if (control.keysAxis != null)
        {
            foreach (var k in control.keysAxis)
            {
                if (GetKeyAxis(k)) return true;
            }
        }

        return false;
    }

    static bool GetKeyAxis(AxisButtons axis)
    {
        switch (axis)
        {
            case AxisButtons.MouseXMinus:
                return Input.GetAxis("Mouse X") < 0f;
            case AxisButtons.MouseXPlus:
                return Input.GetAxis("Mouse X") > 0f;
            case AxisButtons.MouseYMinus:
                return Input.GetAxis("Mouse Y") < 0f;
            case AxisButtons.MouseYPlus:
                return Input.GetAxis("Mouse Y") > 0f;
            case AxisButtons.ScrollXMinus:
                return Input.mouseScrollDelta.x < 0f;
            case AxisButtons.ScrollXPlus:
                return Input.mouseScrollDelta.x > 0f;
            case AxisButtons.ScrollYMinus:
                return Input.mouseScrollDelta.y < 0f;
            case AxisButtons.ScrollYPlus:
                return Input.mouseScrollDelta.y > 0f;
            case AxisButtons.JoystickLeftXMinus:
                return Input.GetAxis("Joystick Left X") < 0f;
            case AxisButtons.JoystickLeftXPlus:
                return Input.GetAxis("Joystick Left X") > 0f;
            case AxisButtons.JoystickLeftYMinus:
                return Input.GetAxis("Joystick Left Y") < 0f;
            case AxisButtons.JoystickLeftYPlus:
                return Input.GetAxis("Joystick Left Y") > 0f;
            case AxisButtons.JoystickDpadXMinus:
                return Input.GetAxis("Joystick Dpad X") < 0f;
            case AxisButtons.JoystickDpadXPlus:
                return Input.GetAxis("Joystick Dpad X") > 0f;
            case AxisButtons.JoystickDpadYMinus:
                return Input.GetAxis("Joystick Dpad Y") < 0f;
            case AxisButtons.JoystickDpadYPlus:
                return Input.GetAxis("Joystick Dpad Y") > 0f;
            case AxisButtons.JoystickRightXMinus:
                return Input.GetAxis("Joystick Right X") < 0f;
            case AxisButtons.JoystickRightXPlus:
                return Input.GetAxis("Joystick Right X") > 0f;
            case AxisButtons.JoystickRightYMinus:
                return Input.GetAxis("Joystick Right Y") < 0f;
            case AxisButtons.JoystickRightYPlus:
                return Input.GetAxis("Joystick Right Y") > 0f;
            case AxisButtons.JoystickTriggerL:
                return Input.GetAxis("Joystick Trigger L") > 0f;
            case AxisButtons.JoystickTriggerR:
                return Input.GetAxis("Joystick Trigger R") > 0f;
            default:
                return false;
        }
    }

    public Dir GetDir()
    {
        Dir _dir = Dir.Z;
        _dir = GetDirKeyboard();
        if (_dir != Dir.Z) return _dir;
        _dir = GetDirJoystick();
        return _dir;
    }

    public static Vector2 GetAxis(Axis axis)
    {
        switch (axis)
        {
            case Axis.Mouse:
                return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            case Axis.Scroll:
                return Input.mouseScrollDelta;
            case Axis.JoystickLeft:
                return new Vector2(Input.GetAxis("Joystick Left X"), Input.GetAxis("Joystick Left Y"));
            case Axis.JoystickDpad:
                return new Vector2(Input.GetAxis("Joystick Dpad X"), Input.GetAxis("Joystick Dpad Y"));
            case Axis.JoystickRight:
                return new Vector2(Input.GetAxis("Joystick Right X"), Input.GetAxis("Joystick Right Y"));
            case Axis.KeysWASD:
                return new Vector2(
                    (Input.GetKey(KeyCode.A) ? -1f : 0) + (Input.GetKey(KeyCode.D) ? 1f : 0),
                    (Input.GetKey(KeyCode.W) ? 1f : 0) + (Input.GetKey(KeyCode.S) ? -1f : 0)
                    );
            case Axis.KeysArrows:
                return new Vector2(
                    (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0) + (Input.GetKey(KeyCode.RightArrow) ? 1f : 0),
                    (Input.GetKey(KeyCode.UpArrow) ? 1f : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1f : 0)
                    );
            default:
                return Vector2.zero;
        }
    }

    /* bool GetAxisButtonDown(string name) {
        
    }

    bool GetAxisButtonUp(string name) {
        
    } */

    Dir GetDirKeyboard()
    {
        bool N = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool E = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool S = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool W = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        return BoolsToDir(N, E, S, W);
    }

    Dir GetDirJoystick()
    {
        Vector2 _dir = GetJoystickDir("Left");
        if (_dir == Vector2.zero)
        {
            _dir = GetJoystickDir("Dpad");
        }

        bool N = _dir.y > 0f;
        bool E = _dir.x > 0f;
        bool S = _dir.y < 0f;
        bool W = _dir.x < 0f;
        return BoolsToDir(N, E, S, W);
    }

    Vector2 GetJoystickDir(string axis)
    {
        Vector2 _dir = new Vector2(Input.GetAxis("Joystick " + axis + " X"), Input.GetAxis("Joystick " + axis + " Y"));

        if (_dir.x < 0f)
        {
            if (_dir.x > -0.4f) _dir.x = 0f;
        }
        else if (_dir.x > 0f)
        {
            if (_dir.x < 0.4f) _dir.x = 0f;
        }
        if (_dir.y < 0f)
        {
            if (_dir.y > -0.4f) _dir.y = 0f;
        }
        else if (_dir.y > 0f)
        {
            if (_dir.y < 0.4f) _dir.y = 0f;
        }
        return _dir;
    }

    Dir BoolsToDir(bool N, bool E, bool S, bool W)
    {
        if (N || E || S || W)
        {
            if (N && !W && !E) return Dir.N;
            if (N && W && !E) return Dir.NW;
            if (N && !W && E) return Dir.NE;
            if (S && !W && !E) return Dir.S;
            if (S && W && !E) return Dir.SW;
            if (S && !W && E) return Dir.SE;
            if (W && !N && !S) return Dir.W;
            if (E && !N && !S) return Dir.E;
        }

        return Dir.Z;
    }

    static string JoystickKey(Joystick key)
    {
        switch (key)
        {
            case Joystick.ButtonA:
                return "Joystick A";
            case Joystick.ButtonB:
                return "Joystick B";
            case Joystick.ButtonX:
                return "Joystick X";
            case Joystick.ButtonY:
                return "Joystick Y";
            case Joystick.Start:
                return "Joystick Start";
            case Joystick.Back:
                return "Joystick Back";
            case Joystick.BumperL:
                return "Joystick Bumper L";
            case Joystick.BumperR:
                return "Joystick Bumper R";
            case Joystick.TriggerL:
                return "Joystick Trigger L";
            case Joystick.TriggerR:
                return "Joystick Trigger R";
            case Joystick.UpD:
                return "Joystick Dpad Up";
            case Joystick.RightD:
                return "Joystick Dpad Right";
            case Joystick.DownD:
                return "Joystick Dpad Down";
            case Joystick.LeftD:
                return "Joystick Dpad Left";
        }
        return "";
    }

    public Vector2Int VectorDir(Dir dir)
    {
        switch (dir)
        {
            case Dir.N:
                return new Vector2Int(0, 1);
            case Dir.NE:
                return new Vector2Int(1, 1);
            case Dir.E:
                return new Vector2Int(1, 0);
            case Dir.SE:
                return new Vector2Int(1, -1);
            case Dir.S:
                return new Vector2Int(0, -1);
            case Dir.SW:
                return new Vector2Int(-1, -1);
            case Dir.W:
                return new Vector2Int(-1, 0);
            case Dir.NW:
                return new Vector2Int(-1, 1);
        }
        return Vector2Int.zero;
    }
}

}