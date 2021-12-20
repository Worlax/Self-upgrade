using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleItemConstructor : Singleton<ScheduleItemConstructor>
{
#pragma warning disable 0649

	[SerializeField] ScheduleItem upgradePrefab;
	[SerializeField] ScheduleItem upgradeWithValuePrefab;
	[SerializeField] ScheduleItem upgradeWithValueAndBreakPrefab;

#pragma warning restore 0649

	// Creating items in the right order: Checkers, Multichekers, Timers(sorted)
	public List<ScheduleItem> CreateItemsForDayOfTheWeek(DayOfWeek dayOfWeek, bool ShowBreakTime, Transform parent)
	{
		List<Upgrade> nonTimers = Upgrade.AllUpgrades.Where(obj => obj.Type != UpgradeType.Timer).ToList();
		List<Upgrade> timers = Upgrade.AllUpgrades.Where(obj => obj.Type == UpgradeType.Timer).ToList();
		List<ScheduleItem> createdItems = new List<ScheduleItem>();

		// Non timers go first
		createdItems.AddRange(CreateItemsForUpgrades(nonTimers, dayOfWeek, ShowBreakTime));

		// Timers go last
		List<ScheduleItem> timerItems = CreateItemsForUpgrades(timers, dayOfWeek, ShowBreakTime);
		timerItems.Sort();
		createdItems.AddRange(timerItems);

		createdItems.ForEach(obj => obj.transform.SetParent(parent, false));

		return createdItems;
	}

	List<ScheduleItem> CreateItemsForUpgrades(List<Upgrade> upgrades, DayOfWeek dayOfWeek, bool ShowBreakTime)
	{
		List<ScheduleItem> createdItems = new List<ScheduleItem>();

		foreach (Upgrade upgrade in upgrades)
		{
			foreach (Mission mission in upgrade.Progress.MissionCalendar.ScheduledMissions)
			{
				if (mission.DayOfWeek == dayOfWeek)
				{
					createdItems.Add(CreateItem(upgrade, mission, ShowBreakTime));
				}
			}
		}

		return createdItems;
	}

	ScheduleItem CreateItem(Upgrade upgrade, Mission mission, bool ShowBreakTime)
	{
		ScheduleItem item = null;

		switch (upgrade.Type)
		{
			case UpgradeType.Checker:
				item = Instantiate(upgradePrefab);
				break;

			case UpgradeType.MultiChecker:
				item = Instantiate(upgradeWithValuePrefab);
				break;

			case UpgradeType.Timer:
				if (ShowBreakTime)
				{
					item = Instantiate(upgradeWithValueAndBreakPrefab);
				}
				else
				{
					item = Instantiate(upgradeWithValuePrefab);
				}
				break;
		}

		item.Init(upgrade, mission);

		return item;
	}
}