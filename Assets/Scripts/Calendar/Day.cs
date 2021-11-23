using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Day
{
	[JsonProperty] public DateTime Date { get; private set; }
	[JsonProperty] public List<Upgrade> Upgrades { get; private set; } = new List<Upgrade>();

	[JsonConstructor]
	public Day(DateTime date)
	{
		Date = date;
	}

	public Day(DateTime date, IEnumerable<Upgrade> upgrades)
	{
		Date = date;
		Upgrades = upgrades.ToList();
	}

	public void SetTodayUpgrades()
	{
		//Upgrades = UpgradeManager.Upgrades;
	}
}