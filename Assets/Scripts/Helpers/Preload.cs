using UnityEngine;

public class Preload : Singleton<Preload>
{
	protected void Start()
	{
		// Load data
		JsonSerializer.LoadData();

		// Create empty Upgrade if there is no upgrades
		if (Upgrade.AllUpgrades.Count == 0)
		{
			Upgrade.CreateUpgrade("Autocreated", UpgradeType.Timer);
		}

		// Upgrade dropdown display
		UpgradesList.Instance.Init();
		UpgradesList.Instance.UpdateDisplay();

		// Load month tab
		FindObjectOfType<UICalendar>().OpenMonthTab(System.DateTime.Today);
	}
}