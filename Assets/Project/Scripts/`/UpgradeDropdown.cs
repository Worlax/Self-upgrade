using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDropdown : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Dropdown dropdown;

#pragma warning restore 0649

	public Upgrade GetActive()
	{
		string activeName = dropdown.options[dropdown.value].text;
		return Upgrade.AllUpgrades.First(obj => obj.Name == activeName);
	}

	void UpdateDisplay()
	{
		List<string> allUpgradesNames = Upgrade.AllUpgrades.Select(obj => obj.Name).ToList();
		dropdown.ClearOptions();
		dropdown.AddOptions(allUpgradesNames);
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();
	}
}