using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayContent : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform DayContent1;
	[SerializeField] RectTransform DayContent2;
	[SerializeField] RectTransform DayContent3;
	[SerializeField] RectTransform ActiveDayContent;
	[SerializeField] RectTransform DayContent5;
	[SerializeField] RectTransform DayContent6;
	[SerializeField] RectTransform DayContent7;

	[SerializeField] Text Day1Name;
	[SerializeField] Text Day2Name;
	[SerializeField] Text Day3Name;
	[SerializeField] Text ActiveDayName;
	[SerializeField] Text Day5Name;
	[SerializeField] Text Day6Name;
	[SerializeField] Text Day7Name;

#pragma warning restore 0649

	public List<UpgradeItem> FillContent(DateTime date)
	{
		List<UpgradeItem> createdItems = new List<UpgradeItem>();

		// Filling 3 days befor, 1 current day and 3 days after with content
		// and changing their names
		createdItems.AddRange(FilLDay(date.AddDays(-3), DayContent1, Day1Name));
		createdItems.AddRange(FilLDay(date.AddDays(-2), DayContent2, Day2Name));
		createdItems.AddRange(FilLDay(date.AddDays(-1), DayContent3, Day3Name));
		createdItems.AddRange(FilLDay(date, ActiveDayContent, ActiveDayName));
		createdItems.AddRange(FilLDay(date.AddDays(1), DayContent5, Day5Name));
		createdItems.AddRange(FilLDay(date.AddDays(2), DayContent6, Day6Name));
		createdItems.AddRange(FilLDay(date.AddDays(3), DayContent7, Day7Name));

		return createdItems;
	}

	List<UpgradeItem> FilLDay(DateTime date, RectTransform parent, Text dayName)
	{
		dayName.text = date.Day.ToString();
		List<UpgradeItem> createdItems = new List<UpgradeItem>();

		List<Upgrade> checkers = Upgrade.GetAllUpgradesOfAType(UpgradeType.Checker);
		List<Upgrade> multiCheckers = Upgrade.GetAllUpgradesOfAType(UpgradeType.MultiChecker);
		List<Upgrade> timers = Upgrade.GetAllUpgradesOfAType(UpgradeType.Timer);

		createdItems.AddRange(CreateItems(checkers, date, parent));
		createdItems.AddRange(CreateItems(multiCheckers, date, parent));
		createdItems.AddRange(CreateItems(timers, date, parent));

		return createdItems;
	}

	List<UpgradeItem> CreateItems(List<Upgrade> upgrades, DateTime date, RectTransform parent)
	{
		List<UpgradeItem> createdItems = new List<UpgradeItem>();

		foreach (Upgrade upgrade in upgrades)
		{
			int value = upgrade.Calendar.GetValue(date);

			if (value > 0)
			{
				UpgradeItem item = ContentPrefabs.Instance.GetUpgrade(upgrade, date, parent);
				createdItems.Add(item);
			}
		}

		return createdItems;
	}

	void ClearContent()
	{
		foreach (Transform obj in DayContent1.transform)
		{
			Destroy(obj.gameObject);
		}

		foreach (Transform obj in DayContent2.transform)
		{
			Destroy(obj.gameObject);
		}

		foreach (Transform obj in DayContent3.transform)
		{
			Destroy(obj.gameObject);
		}

		foreach (Transform obj in ActiveDayContent)
		{
			Destroy(obj.gameObject);
		}

		foreach (Transform obj in DayContent5.transform)
		{
			Destroy(obj.gameObject);
		}

		foreach (Transform obj in DayContent6.transform)
		{
			Destroy(obj.gameObject);
		}

		foreach (Transform obj in DayContent7.transform)
		{
			Destroy(obj.gameObject);
		}
	}
}