using System;
using UnityEngine;

public class ContentPrefabs : Singleton<ContentPrefabs>
{
#pragma warning disable 0649

	[field: SerializeField] public CalendarItem YearItem { get; private set; }
	[field: SerializeField] public CalendarItem MonthItem { get; private set; }
	[field: SerializeField] public CalendarItem DayItem { get; private set; }

	[SerializeField] UpgradeItem upgradePrefab;
	[SerializeField] UpgradeItem upgradeWithValue;

#pragma warning restore 0649

	public UpgradeItem GetUpgrade(Upgrade upgrade, DateTime date, Transform parent)
	{
		UpgradeItem item = null;

		switch (upgrade.Type)
		{
			case UpgradeType.Timer:
			case UpgradeType.MultiChecker:
				item = Instantiate(upgradeWithValue);
				break;

			case UpgradeType.Checker:
				item = Instantiate(upgradePrefab);
				break;
		}

		item.Init(upgrade, date);
		item.transform.SetParent(parent, false);

		return item;
	}

	public CalendarItem GetItem(CalendarItemType type, DateTime date, Transform parent)
	{
		CalendarItem item = null;

		switch (type)
		{
			case CalendarItemType.Year:
				item = Instantiate(YearItem);
				break;

			case CalendarItemType.Month:
				item = Instantiate(MonthItem);
				break;

			case CalendarItemType.Day:
				item = Instantiate(DayItem);
				break;
		}

		item.transform.SetParent(parent, false);
		item.Init(type, date);

		return item;
	}
}