using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsItem : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text label;
	[SerializeField] Text hours;
	[SerializeField] Toggle selfToggle;

#pragma warning restore 0649

	public Upgrade Upgrade { get; private set; }

	public event Action<SettingsItem, bool> OnToggleChanged;

	public void Init(Upgrade upgrade)
	{
		Upgrade = upgrade;
		label.text = upgrade.Name;
		hours.text = TimeConverter.TimeString(upgrade.Progress.GetTotalValue(), false);
	}

	// Events
	void ToggleValueChanged(bool value)
	{
		OnToggleChanged?.Invoke(this, value);
	}

	// Unity
	private void OnEnable()
	{
		selfToggle.onValueChanged.AddListener(ToggleValueChanged);
	}

	private void OnDisable()
	{
		selfToggle.onValueChanged.RemoveListener(ToggleValueChanged);
	}
}