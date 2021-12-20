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

	[SerializeField] DayItem day1;
	[SerializeField] DayItem day2;
	[SerializeField] DayItem day3;
	[SerializeField] DayItem activeDay;
	[SerializeField] DayItem day5;
	[SerializeField] DayItem day6;
	[SerializeField] DayItem day7;

#pragma warning restore 0649

	DateTime currentDate;

	public static event Action OnEnabled;
	public static event Action OnDisabled;

	public List<UICalendarUpgradeItem> FillContent(DateTime date)
	{
		ClearContent();
		currentDate = date;
		List<UICalendarUpgradeItem> createdItems = new List<UICalendarUpgradeItem>();

		// Filling 3 days befor, 1 current day and 3 days after with content
		// and changing their names
		createdItems.AddRange(FilLDay(date.AddDays(-3), dayContent1, day1));
		createdItems.AddRange(FilLDay(date.AddDays(-2), dayContent2, day2));
		createdItems.AddRange(FilLDay(date.AddDays(-1), dayContent3, day3));
		createdItems.AddRange(FilLDay(date, activeDayContent, activeDay));
		createdItems.AddRange(FilLDay(date.AddDays(1), dayContent5, day5));
		createdItems.AddRange(FilLDay(date.AddDays(2), dayContent6, day6));
		createdItems.AddRange(FilLDay(date.AddDays(3), dayContent7, day7));

		return createdItems;
	}

	List<UICalendarUpgradeItem> FilLDay(DateTime date, RectTransform parent, DayItem dayItem)
	{
		dayItem.Init(date);
		List<UICalendarUpgradeItem> createdItems = new List<UICalendarUpgradeItem>();

		foreach (Upgrade upgrade in UpgradesList.Instance.GetActive())
		{
			int value = upgrade.Progress.GetValue(date);

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
	void ActiveUpgradesChanged(IReadOnlyList<Upgrade> upgrades)
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

		OnEnabled?.Invoke();
	}

	private void OnDisable()
	{
		UpgradesList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;

		OnDisabled?.Invoke();
	}
}