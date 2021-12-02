using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesList : Singleton<UpgradesList>
{
#pragma warning disable 0649

	[SerializeField] Dropdown dropdown;

#pragma warning restore 0649

	const string ALL_GLOBAL = "<b><color=navy>All</color></b>";
	const string ALL_TIMERS = "<b><color=navy>All Timers</color></b>";
	const string ALL_CHECKERS = "<b><color=navy>All Checkers</color></b>";
	const string ALL_MULTI_CHECKERS = "<b><color=navy>All MCheckers</color></b>";

	public event Action<List<Upgrade>> OnActiveUpgradesChanged;

	HashSet<UpgradeType> avaibleTypes = new HashSet<UpgradeType>();
	HashSet<UpgradeType> avaibleTypesAsAll = new HashSet<UpgradeType>();
	bool allGlobal = true;

	public void Init()
	{
		foreach(UpgradeType type in Enum.GetValues(typeof(UpgradeType)))
		{
			avaibleTypes.Add(type);
			avaibleTypesAsAll.Add(type);
		}

		UpdateDisplay();
	}

	public List<Upgrade> GetActive()
	{
		string activeOptionName = dropdown.options[dropdown.value].text;
		List<Upgrade> activeUpgrades = new List<Upgrade>();

		// Getting all upgrades of the type if name matches the const
		// else return single upgrade that mutches the selected option name
		switch (activeOptionName)
		{
			case ALL_GLOBAL:
				activeUpgrades.AddRange(Upgrade.AllUpgrades);
				break;

			case ALL_TIMERS:
				List<Upgrade> allTimers = Upgrade.AllUpgrades.Where(obj => obj.Type == UpgradeType.Timer).ToList();
				activeUpgrades.AddRange(allTimers);
				break;

			case ALL_CHECKERS:
				List<Upgrade> allCheckers = Upgrade.AllUpgrades.Where(obj => obj.Type == UpgradeType.Checker).ToList();
				activeUpgrades.AddRange(allCheckers);
				break;

			case ALL_MULTI_CHECKERS:
				List<Upgrade> allMultiCheckers = Upgrade.AllUpgrades.Where(obj => obj.Type == UpgradeType.MultiChecker).ToList();
				activeUpgrades.AddRange(allMultiCheckers);
				break;

			default:
				Upgrade singleUpgrade = Upgrade.AllUpgrades.Find(obj => obj.Name == activeOptionName);
				activeUpgrades.Add(singleUpgrade);
				break;
		}

		return activeUpgrades;
	}

	public void SetAvaibleTypes(HashSet<UpgradeType> avaibleTypes, HashSet<UpgradeType> avaibleTypesAsAll, bool allGlobal)
	{
		this.avaibleTypes = avaibleTypes;
		this.avaibleTypesAsAll = avaibleTypesAsAll;
		this.allGlobal = allGlobal;

		UpdateDisplay();
	}

	public void UpdateDisplay()
	{
		// Dropdown update
		List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();
		HashSet<UpgradeType> notYetShowedTypes = new HashSet<UpgradeType>(avaibleTypes);
		string activeOptionBeforUpdate = dropdown.options[dropdown.value].text;

		if (allGlobal)
		{
			newOptions.Add(new Dropdown.OptionData(ALL_GLOBAL));
		}

		foreach (UpgradeType type in avaibleTypesAsAll)
		{
			if (avaibleTypes.Contains(type))
			{
				// Add option that represents all upgrades of the type
				switch (type)
				{
					case UpgradeType.Timer:
						newOptions.Add(new Dropdown.OptionData(ALL_TIMERS));
						break;

					case UpgradeType.Checker:
						newOptions.Add(new Dropdown.OptionData(ALL_CHECKERS));
						break;

					case UpgradeType.MultiChecker:
						newOptions.Add(new Dropdown.OptionData(ALL_MULTI_CHECKERS));
						break;
				}

				// Add options for all upgrades of the type
				List<Upgrade> upgradesOfTheType = Upgrade.AllUpgrades.Where(obj => obj.Type == type).ToList();
				upgradesOfTheType.ForEach(obj => newOptions.Add(new Dropdown.OptionData(obj.Name)));

				notYetShowedTypes.Remove(type);
			}
		}

		// Showing types that dosent have "all" option
		foreach (UpgradeType type in notYetShowedTypes)
		{
			List<Upgrade> upgradesOfTheType = Upgrade.AllUpgrades.Where(obj => obj.Type == type).ToList();
			upgradesOfTheType.ForEach(obj => newOptions.Add(new Dropdown.OptionData(obj.Name)));
		}

		dropdown.options = newOptions;

		// Reset active option if old one is no longer avaible;
		int activeOptionIndexNow = dropdown.options.FindIndex(obj => obj.text == activeOptionBeforUpdate);

		if (activeOptionIndexNow != -1)
		{
			dropdown.value = activeOptionIndexNow;
		}
		else
		{
			dropdown.value = 0;
		}
	}

	// Events
	void NewUpdateCreated(Upgrade upgrade)
	{
		UpdateDisplay();
	}

	void DropdownActiveItemChanged(int value)
	{
		OnActiveUpgradesChanged?.Invoke(GetActive());
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();
		Upgrade.OnNewUpgradeCreated += NewUpdateCreated;
		dropdown.onValueChanged.AddListener(DropdownActiveItemChanged);
	}

	private void OnDisable()
	{
		Upgrade.OnNewUpgradeCreated -= NewUpdateCreated;
		dropdown.onValueChanged.RemoveListener(DropdownActiveItemChanged);
	}
}