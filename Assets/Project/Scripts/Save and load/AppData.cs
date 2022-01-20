using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

public class AppData
{
	public DateTime dateCreated;
	public int totalSeconds;
	public List<Upgrade> Upgrades;

	AppData() { }

	public static string GetDataAsJson()
	{
		return JsonConvert.SerializeObject(AppData.GetData(), Formatting.Indented);
	}

	public static AppData GetData()
	{
		int totalS = 0;
		List<Upgrade> allTimers = Upgrade.AllUpgrades.Where(obj => obj.Type == UpgradeType.Timer).ToList();
		allTimers.ForEach(obj => totalS += obj.Progress.GetTotalValue());

		return new AppData()
		{
			dateCreated = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day),
			totalSeconds = totalS,
			Upgrades = Upgrade.AllUpgrades.ToList()
		};
	}
}