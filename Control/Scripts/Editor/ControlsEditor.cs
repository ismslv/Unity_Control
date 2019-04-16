using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FMLHT.Controls {

public class ControlsEditor : MonoBehaviour
{
    [MenuItem("FMLHT/Controls/Add to scene")]
    public static void AddPrefab() {
        if (Editor.FindObjectOfType<Control>() == null) {
            UnityEngine.Object prefab = Resources.Load("Controls");
            var newObj = PrefabUtility.InstantiatePrefab(prefab);
            GameObject obj = (GameObject)newObj;
            obj.name = "Controls";
            var core = GameObject.Find("Core");
            if (core == null) {
                core = new GameObject();
                core.name = "Core";
            }
            obj.transform.SetParent(core.transform);
        } else {
            Debug.Log("There is already one Controls Manager in this scene!");
        }
    }
}

}