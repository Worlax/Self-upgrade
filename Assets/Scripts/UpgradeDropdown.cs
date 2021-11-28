using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeDropdown : Singleton<UpgradeDropdown>
{
#pragma warning disable 0649

	[SerializeField] Dropdown dropdown;

#pragma warning restore 0649

	public event Action<Upgrade> OnActiveUpgradeChanged;

	public void UpdateDisplay()
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

	// Events
	void ProgressLoaded()
	{
		UpdateDisplay();
	}

	void NewUpdateCreated(Upgrade upgrade)
	{
		UpdateDisplay();
	}

	void ActiveUpgradeChanged(int value)
	{
		OnActiveUpgradeChanged?.Invoke(GetActive());
	}
	//

	// Unity
	private void OnEnable()
	{
		JsonSerializer.OnProgressLoaded += ProgressLoaded;
		Upgrade.OnNewUpgradeCreated += NewUpdateCreated;
		dropdown.onValueChanged.AddListener(ActiveUpgradeChanged);
	}

	private void OnDisable()
	{
		JsonSerializer.OnProgressLoaded -= ProgressLoaded;
		Upgrade.OnNewUpgradeCreated -= NewUpdateCreated;
		dropdown.onValueChanged.RemoveListener(ActiveUpgradeChanged);
	}
	//
}