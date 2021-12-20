using System;
using System.Collections.Generic;
using UnityEngine;

public class Preload : Singleton<Preload>
{
	protected void Start()
	{
		// Load data
		GeneralFileSystem.LoadData();

		// Create empty Upgrade if there is no upgrades
		if (Upgrade.AllUpgrades.Count == 0)
		{
			Upgrade.CreateUpgrade("Autocreated", UpgradeType.Timer);
		}

		// Update dropdown display
		UpgradesList.Instance.UpdateDisplay();
	}
}