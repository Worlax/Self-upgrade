using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeDropdown : Singleton<UpgradeDropdown>
{
	[SerializeField] Dropdown dropdown;

	public Action<Upgrade> OnActiveUpgradeChanged;

	void UpdateVisual()
	{
		List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();

		foreach (Upgrade upgrade in Upgrade.AllUpgrades)
		{
			Dropdown.OptionData option = new Dropdown.OptionData(upgrade.Name);
			newOptions.Add(option);
		}

		dropdown.options = newOptions;
	}

	public Upgrade GetActive()
	{
		string upgradeName = dropdown.options[dropdown.value].text;
		return Upgrade.AllUpgrades.Find(obj => obj.Name == upgradeName);
	}

	void ActiveUpgradeChanged(int value)
	{
		OnActiveUpgradeChanged?.Invoke(GetActive());
	}

	private void OnEnable()
	{
		JsonSerializer.OnProgressLoaded += UpdateVisual;
		dropdown.onValueChanged.AddListener(ActiveUpgradeChanged);
	}

	private void OnDisable()
	{
		JsonSerializer.OnProgressLoaded -= UpdateVisual;
		dropdown.onValueChanged.RemoveListener(ActiveUpgradeChanged);
	}
}