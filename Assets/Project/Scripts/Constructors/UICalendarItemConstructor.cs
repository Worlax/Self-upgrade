using System;
using UnityEngine;

public class UICalendarItemConstructor : Singleton<UICalendarItemConstructor>
{
#pragma warning disable 0649

	[field: SerializeField] public UICalendarItem YearItemPrefab { get; private set; }
	[field: SerializeField] public UICalendarItem MonthItemPrefab { get; private set; }
	[field: SerializeField] public UICalendarItem DayItemPrefab { get; private set; }

	[SerializeField] UICalendarUpgradeItem upgradeItemPrefab;
	[SerializeField] UICalendarUpgradeItem upgradeItemWithValuePrefab;

#pragma warning restore 0649

	public UICalendarItem CreateCalendarItem(UICalendarItemType type, DateTime date, Transform parent)
	{
		UICalendarItem item = null;

		switch (type)
		{
			case UICalendarItemType.Year:
				item = Instantiate(YearItemPrefab);
				break;

			case UICalendarItemType.Month:
				item = Instantiate(MonthItemPrefab);
				break;

			case UICalendarItemType.Day:
				item = Instantiate(DayItemPrefab);
				break;
		}

		item.transform.SetParent(parent, false);
		item.Init(type, date);

		return item;
	}

	public UICalendarUpgradeItem CreateUpgradeItem(Upgrade upgrade, DateTime date, Transform parent)
	{
		UICalendarUpgradeItem item = null;

		switch (upgrade.Type)
		{
			case UpgradeType.Timer:
			case UpgradeType.MultiChecker:
				item = Instantiate(upgradeItemWithValuePrefab);
				break;

			case UpgradeType.Checker:
				item = Instantiate(upgradeItemPrefab);
				break;
		}

		item.Init(upgrade, date);
		item.transform.SetParent(parent, false);

		return item;
	}
}