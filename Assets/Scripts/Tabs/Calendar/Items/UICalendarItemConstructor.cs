using System;
using UnityEngine;

public class UICalendarItemConstructor : Singleton<UICalendarItemConstructor>
{
#pragma warning disable 0649

	[field: SerializeField] public UICalendarItem YearItem { get; private set; }
	[field: SerializeField] public UICalendarItem MonthItem { get; private set; }
	[field: SerializeField] public UICalendarItem DayItem { get; private set; }

	[SerializeField] UICalendarUpgradeItem upgradeItem;
	[SerializeField] UICalendarUpgradeItem upgradeItemWithValue;

#pragma warning restore 0649

	public UICalendarItem CreateItem(UICalendarItemType type, DateTime date, Transform parent)
	{
		UICalendarItem item = null;

		switch (type)
		{
			case UICalendarItemType.Year:
				item = Instantiate(YearItem);
				break;

			case UICalendarItemType.Month:
				item = Instantiate(MonthItem);
				break;

			case UICalendarItemType.Day:
				item = Instantiate(DayItem);
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
				item = Instantiate(upgradeItemWithValue);
				break;

			case UpgradeType.Checker:
				item = Instantiate(upgradeItem);
				break;
		}

		item.Init(upgrade, date);
		item.transform.SetParent(parent, false);

		return item;
	}
}