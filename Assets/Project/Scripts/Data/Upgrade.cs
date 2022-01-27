using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public enum UpgradeType
{
	Checker, MultiChecker, Timer
}

public class Upgrade : IComparable<Upgrade>
{
	[JsonProperty] public string Name { get; private set; }
	[JsonProperty] public UpgradeType Type { get; private set; }
	[JsonProperty] public Progress Progress { get; private set; } = new Progress();

	static List<Upgrade> allUpgrades = new List<Upgrade>();
	public static IReadOnlyList<Upgrade> AllUpgrades { get => allUpgrades; }

	public static event Action<Upgrade> OnNewUpgradeCreated;
	public static event Action<string> OnUpgradeDeleted;

	static Upgrade()
	{
		GeneralFileSystem.OnLoadingBegins += LoadBegins;
	}

	public static void CreateUpgrade(string name, UpgradeType type)
	{
		string formatName = FormatName(name);

		if (!UpgradeAlreadyExists(formatName))
		{
			Upgrade upgrade = new Upgrade()
			{
				Name = formatName,
				Type = type
			};

			allUpgrades.Add(upgrade);
			allUpgrades.Sort();
			OnNewUpgradeCreated?.Invoke(upgrade);
		}
	}

	public static void DeleteUpgrade(string name)
	{
		string formatedName = FormatName(name);
		Upgrade upgradeToDelete = allUpgrades.Find(obj => obj.Name == formatedName);

		if (upgradeToDelete != null)
		{
			allUpgrades.Remove(upgradeToDelete);
			OnUpgradeDeleted?.Invoke(formatedName);
		}
	}

	static bool UpgradeAlreadyExists(string name)
	{
		return allUpgrades.Any(x => x.Name == name);
	}

	static string FormatName(string name)
	{
		if (name.Length == 0)
		{
			return name;
		}
		else if (name.Length == 1)
		{
			return name.ToUpper();
		}
		else
		{
			return char.ToUpper(name[0]) + name.Substring(1).ToLower();
		}
	}

	public int CompareTo(Upgrade other)
	{
		if (other == null) return 1;

		return Type.CompareTo(other.Type);
	}

	// Events
	static void LoadBegins(AppData data)
	{
		allUpgrades = data.Upgrades;
		allUpgrades.Sort();
	}
}