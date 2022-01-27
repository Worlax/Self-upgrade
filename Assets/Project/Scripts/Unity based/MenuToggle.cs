using UnityEngine;
using UnityEngine.UI;

public class MenuToggle : Toggle
{
#pragma warning disable 0649

	[SerializeField] RectTransform menuTab;

#pragma warning restore 0649

	void ValueChanged(bool value)
	{
		menuTab.gameObject.SetActive(value);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		onValueChanged.AddListener(ValueChanged);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		onValueChanged.RemoveListener(ValueChanged);
	}
}