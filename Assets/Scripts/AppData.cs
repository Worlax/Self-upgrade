using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class AppData
{
	[JsonProperty] List<Upgrade> Upgrades;

	private AppData() { }

	public static AppData GetData()
	{
		AppData data = new AppData();
		data.Upgrades = Upgrade.AllUpgrades;

		return data;
	}

	public static void LoadData(AppData data)
	{
		Upgrade.LoadUpgrades(data.Upgrades);
	}
}