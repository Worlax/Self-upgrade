using UnityEngine;
using UnityEngine.UI;

public class Preload : Singleton<Preload>
{
#pragma warning disable 0649



#pragma warning restore 0649

	protected void Start()
	{
		Log.Instance.Print(typeof(Preload), "Start.");
		// Load data
		GeneralFileSystem.LoadData();

		// Create empty Upgrade if there is no upgrades
		if (Upgrade.AllUpgrades.Count == 0)
		{
			Log.Instance.Print(typeof(Preload), "There is no upgrades, creating new.");
			Upgrade.CreateUpgrade("English", UpgradeType.Timer);
			Upgrade.CreateUpgrade("Code", UpgradeType.Timer);
		}
		else
		{
			Log.Instance.Print(typeof(Preload), "There is: " + Upgrade.AllUpgrades.Count + " upgrades.");
		}

		// Update dropdown display
		UpgradeList.Instance.UpdateDisplay();
	}
}