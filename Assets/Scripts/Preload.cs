using UnityEngine;

public class Preload : Singleton<Preload>
{
	protected void Start()
	{
		// Load data
		JsonSerializer.LoadProgress();

		// Create empty Upgrade if there is no upgrades
		if (Upgrade.AllUpgrades.Count == 0)
		{
			Upgrade.CreateUpgrade("Autocreated", UpgradeType.Timer);
		}

		// Upgrade dropdown display
		UpgradeDropdown.Instance.UpdateDisplay();

		// Load month tab
		FindObjectOfType<VisualCalendar>().OpenMonthTab(System.DateTime.Today);
	}
}