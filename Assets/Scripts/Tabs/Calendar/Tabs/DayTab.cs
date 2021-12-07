using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTab : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform dayContent1;
	[SerializeField] RectTransform dayContent2;
	[SerializeField] RectTransform dayContent3;
	[SerializeField] RectTransform activeDayContent;
	[SerializeField] RectTransform dayContent5;
	[SerializeField] RectTransform dayContent6;
	[SerializeField] RectTransform dayContent7;

	[SerializeField] Text day1Name;
	[SerializeField] Text day2Name;
	[SerializeField] Text day3Name;
	[SerializeField] Text activeDayName;
	[SerializeField] Text day5Name;
	[SerializeField] Text day6Name;
	[SerializeField] Text day7Name;

#pragma warning restore 0649

	DateTime currentDate;

	public List<UICalendarUpgradeItem> FillContent(DateTime date)
	{
		ClearContent();
		currentDate = date;
		List<UICalendarUpgradeItem> createdItems = new List<UICalendarUpgradeItem>();

		// Filling 3 days befor, 1 current day and 3 days after with content
		// and changing their names
		createdItems.AddRange(FilLDay(date.AddDays(-3), dayContent1, day1Name));
		createdItems.AddRange(FilLDay(date.AddDays(-2), dayContent2, day2Name));
		createdItems.AddRange(FilLDay(date.AddDays(-1), dayContent3, day3Name));
		createdItems.AddRange(FilLDay(date, activeDayContent, activeDayName));
		createdItems.AddRange(FilLDay(date.AddDays(1), dayContent5, day5Name));
		createdItems.AddRange(FilLDay(date.AddDays(2), dayContent6, day6Name));
		createdItems.AddRange(FilLDay(date.AddDays(3), dayContent7, day7Name));

		return createdItems;
	}

	List<UICalendarUpgradeItem> FilLDay(DateTime date, RectTransform parent, Text dayName)
	{
		dayName.text = date.Day.ToString();
		List<UICalendarUpgradeItem> createdItems = new List<UICalendarUpgradeItem>();

		foreach (Upgrade upgrade in UpgradesList.Instance.GetActive())
		{
			int value = upgrade.Calendar.GetValue(date);

			if (value > 0)
			{
				UICalendarUpgradeItem item = UICalendarItemConstructor.Instance.CreateUpgradeItem(upgrade, date, parent);
				createdItems.Add(item);
			}
		}

		return createdItems;
	}

	void ClearContent()
	{
		foreach (Transform transform in dayContent1) Destroy(transform.gameObject);
		foreach (Transform transform in dayContent2) Destroy(transform.gameObject);
		foreach (Transform transform in dayContent3) Destroy(transform.gameObject);
		foreach (Transform transform in activeDayContent) Destroy(transform.gameObject);
		foreach (Transform transform in dayContent5) Destroy(transform.gameObject);
		foreach (Transform transform in dayContent6) Destroy(transform.gameObject);
		foreach (Transform transform in dayContent7) Destroy(transform.gameObject);
	}

	// Events
	void ActiveUpgradesChanged(List<Upgrade> upgrades)
	{
		if (currentDate != null)
		{
			FillContent(currentDate);
		}
	}

	// Unity
	private void OnEnable()
	{
		UpgradesList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}

	private void OnDisable()
	{
		UpgradesList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}
}