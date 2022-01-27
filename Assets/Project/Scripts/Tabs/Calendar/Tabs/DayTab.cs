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

	[SerializeField] DayLabel dayLabel1;
	[SerializeField] DayLabel dayLabel2;
	[SerializeField] DayLabel dayLabel3;
	[SerializeField] DayLabel activeDayLabel;
	[SerializeField] DayLabel dayLabel5;
	[SerializeField] DayLabel dayLabel6;
	[SerializeField] DayLabel dayLabel7;

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
		createdItems.AddRange(FilLDay(date.AddDays(-3), dayContent1, dayLabel1));
		createdItems.AddRange(FilLDay(date.AddDays(-2), dayContent2, dayLabel2));
		createdItems.AddRange(FilLDay(date.AddDays(-1), dayContent3, dayLabel3));
		createdItems.AddRange(FilLDay(date, activeDayContent, activeDayLabel));
		createdItems.AddRange(FilLDay(date.AddDays(1), dayContent5, dayLabel5));
		createdItems.AddRange(FilLDay(date.AddDays(2), dayContent6, dayLabel6));
		createdItems.AddRange(FilLDay(date.AddDays(3), dayContent7, dayLabel7));

		return createdItems;
	}

	List<UICalendarUpgradeItem> FilLDay(DateTime date, RectTransform parent, DayLabel dayLabel)
	{
		dayLabel.Init(date);
		List<UICalendarUpgradeItem> createdItems = new List<UICalendarUpgradeItem>();

		foreach (Upgrade upgrade in UpgradeList.Instance.GetActive())
		{
			int value = upgrade.Progress.GetValue(date);
			int minValueToDisplay = upgrade.Type == UpgradeType.Timer ? 60 : 0;

			if (value > minValueToDisplay)
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
		UpgradeList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;

		OnEnabled?.Invoke();
	}

	private void OnDisable()
	{
		UpgradeList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;

		OnDisabled?.Invoke();
	}
}