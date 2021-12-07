using UnityEngine;
using UnityEngine.UI;

public enum ScheduleItemType
{
	upgrade, upgradeWithValue, upgradeWithTimeAndBreak
}

public class ScheduleItemConstructor : Singleton<ScheduleItemConstructor>
{
#pragma warning disable 0649

	[SerializeField] ScheduleItem upgradePrefab;
	[SerializeField] ScheduleItem upgradeWithTimePrefab;
	[SerializeField] ScheduleItem upgradeWithTimeAndBreakPrefab;

#pragma warning restore 0649

	public ScheduleItem CreateItem(Upgrade upgrade, Mission mission, ScheduleItemType type, Transform parent)
	{
		ScheduleItem item = null;

		switch (type)
		{
			case ScheduleItemType.upgrade:
				item = Instantiate(upgradePrefab, parent);
				break;

			case ScheduleItemType.upgradeWithValue:
				item = Instantiate(upgradeWithTimePrefab, parent);
				break;

			case ScheduleItemType.upgradeWithTimeAndBreak:
				item = Instantiate(upgradeWithTimeAndBreakPrefab, parent);
				break;
		}

		item.Init(upgrade, mission);

		return item;
	}
}