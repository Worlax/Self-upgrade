using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDropdown : Singleton<UpgradeDropdown>
{
	public class Options
	{
		public static void SetToggles(bool timers, bool checkers, bool multiCheckers)
		{
			Instance.showTimers.interactable = timers;
			Instance.showCheckers.interactable = checkers;
			Instance.showMultiChechers.interactable = multiCheckers;
		}

		public static void SetDropdown(bool timersAll, bool checkersAll, bool multicheckersAll)
		{

		}
	}

#pragma warning disable 0649

	[SerializeField] Dropdown dropdown;
	[SerializeField] SmartToggle showTimers;
	[SerializeField] SmartToggle showCheckers;
	[SerializeField] SmartToggle showMultiChechers;

#pragma warning restore 0649

	public event Action<List<Upgrade>> OnActiveUpgradesChanged;

	public List<Upgrade> GetActive()
	{
		string upgradeName = dropdown.options[dropdown.value].text;
		List<Upgrade> upgrades = new List<Upgrade>();
		upgrades.Add(Upgrade.AllUpgrades.Find(obj => obj.Name == upgradeName));
		return upgrades;
	}

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
		OnActiveUpgradesChanged?.Invoke(GetActive());
	}

	void ShowTimersValueChanged(bool value)
	{
		
	}

	void ShowCheckersValueChanged(bool value)
	{
		
	}

	void ShowMultiCheckersValueChanged(bool value)
	{
		
	}

	// Unity
	private void OnEnable()
	{
		JsonSerializer.OnProgressLoaded += ProgressLoaded;
		Upgrade.OnNewUpgradeCreated += NewUpdateCreated;
		dropdown.onValueChanged.AddListener(ActiveUpgradeChanged);

		showTimers.onValueChanged.AddListener(ShowTimersValueChanged);
		showCheckers.onValueChanged.AddListener(ShowCheckersValueChanged);
		showMultiChechers.onValueChanged.AddListener(ShowMultiCheckersValueChanged);
	}

	private void OnDisable()
	{
		JsonSerializer.OnProgressLoaded -= ProgressLoaded;
		Upgrade.OnNewUpgradeCreated -= NewUpdateCreated;
		dropdown.onValueChanged.RemoveListener(ActiveUpgradeChanged);

		showTimers.onValueChanged.RemoveListener(ShowTimersValueChanged);
		showCheckers.onValueChanged.RemoveListener(ShowCheckersValueChanged);
		showMultiChechers.onValueChanged.RemoveListener(ShowMultiCheckersValueChanged);
	}
}