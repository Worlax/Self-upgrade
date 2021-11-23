//using System;
//using System.Linq;
//using System.Collections.Generic;
//using UnityEngine;

//public static class UpgradeManager
//{
//	[SerializeField] public static List<Upgrade> Upgrades = new List<Upgrade>();

//	public static void CreateTimer(string name)
//	{
//		Upgrades.Add(new Timer(name));
//	}

//	public static void CreateChecker(string name)
//	{
//		Upgrades.Add(new Checker(name));
//	}

//	public static void CreateMultiChecker(string name)
//	{
//		Upgrades.Add(new MultiChecker(name));
//	}

//	public static IEnumerable<Upgrade> GetAllUpgrades()
//	{
//		return Upgrades;
//	}

//	public static IEnumerable<Timer> GetAllTimers()
//	{
//		return Upgrades.Where(x => x is Timer) as IEnumerable<Timer>;
//	}

//	public static IEnumerable<Checker> GetAllCheckers()
//	{
//		return Upgrades.Where(x => x is Checker) as IEnumerable<Checker>;
//	}

//	public static IEnumerable<MultiChecker> GetAllMultiÑheckers()
//	{
//		return Upgrades.Where(x => x is MultiChecker) as IEnumerable<MultiChecker>;
//	}

//	public static void SetUpgrades(List<Upgrade> upgrades)
//	{
//		Upgrades = upgrades;
//	}

//	//public void CreateUpgrade(string name)
//	//{
//	//	string formattedName = name.ToLower();

//	//	if (UpgradeAlreadyExists(formattedName))
//	//	{
//	//		throw new Exception("Upgrade with this name already exists.");
//	//	}
//	//	else
//	//	{
//	//		Name = formattedName;
//	//		upgrades.Add(this);
//	//	}
//	//}

//	//static bool UpgradeAlreadyExists(string name)
//	//{
//	//	IEnumerable<Upgrade> matches = upgrades.Where(x => x.Name == name);

//	//	return matches.Count() == 0;
//	//}
//}