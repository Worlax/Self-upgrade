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
	[JsonProperty] public Calendar Calendar { get; private set; } = new Calendar();

	public static List<Upgrade> AllUpgrades { get; private set; } = new List<Upgrade>();

	private Upgrade() { }

	public static event Action<Upgrade> OnNewUpgradeCreated;
	public static event Action<string> OnUpgradeDeleted;

	static Upgrade()
	{
		GeneralFileSystem.OnLoadingBegins += LoadBegins;
	}

	public static Upgrade CreateUpgrade(string name, UpgradeType type)
	{
		string formatName = FormatName(name);

		if (!UpgradeAlreadyExists(formatName))
		{
			Upgrade upgrade = new Upgrade()
			{
				Name = formatName,
				Type = type
			};

			AllUpgrades.Add(upgrade);
			AllUpgrades.Sort();
			OnNewUpgradeCreated?.Invoke(upgrade);

			return upgrade;
		}
		else
		{
			return null;
		}
	}

	public static void DeleteUpgrade(string name)
	{
		string formatedName = FormatName(name);
		Upgrade upgradeToDelete = AllUpgrades.Find(obj => obj.Name == formatedName);

		if (upgradeToDelete != null)
		{
			AllUpgrades.Remove(upgradeToDelete);
			OnUpgradeDeleted?.Invoke(formatedName);
		}
	}

	static bool UpgradeAlreadyExists(string name)
	{
		return AllUpgrades.Any(x => x.Name == name);
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
		AllUpgrades = data.Upgrades;
		AllUpgrades.Sort();
	}
}