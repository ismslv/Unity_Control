using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMLHT.Controls {

[CreateAssetMenu(fileName = "ControlData", menuName = "FMLHT/ControlData", order = 1)]
public class ControlData : ScriptableObject
{
    public ControlButton[] controls;
    public ControlAxis[] controlsAxis;

    public ControlButton Control(string name) {
        return controls.First(c => {return c.name == name;});
    }

    public ControlAxis Axis(string name) {
        return controlsAxis.First(c => {return c.name == name;});
    }
}

}