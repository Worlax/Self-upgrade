using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(MenuToggle))]
[CanEditMultipleObjects]
public class MenuToggleEditor : ToggleEditor
{
#pragma warning disable 0649

	SerializedProperty menuTab;

#pragma warning restore 0649

	protected override void OnEnable()
	{
		base.OnEnable();
		menuTab = serializedObject.FindProperty("menuTab");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.PropertyField(menuTab);
		serializedObject.ApplyModifiedProperties();

		base.OnInspectorGUI();
	}
}