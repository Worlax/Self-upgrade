using UnityEngine;
using UnityEngine.UI;

public class Preload : Singleton<Preload>
{
	protected void Start()
	{
		// Load data
		GeneralFileSystem.LoadData();

		// Create empty Upgrade if there is no upgrades
		if (Upgrade.AllUpgrades.Count == 0)
		{
			Upgrade.CreateUpgrade("English", UpgradeType.Timer);
			Upgrade.CreateUpgrade("Code", UpgradeType.Timer);
		}

		// Update dropdown display
		UpgradeList.Instance.UpdateDisplay();
	}
}