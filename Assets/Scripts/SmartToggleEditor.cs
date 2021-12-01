using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SmartToggle))]
[CanEditMultipleObjects]
public class SmartToggleEditor : Editor
{
    public override void OnInspectorGUI()
    {
       base.OnInspectorGUI();
    }
}