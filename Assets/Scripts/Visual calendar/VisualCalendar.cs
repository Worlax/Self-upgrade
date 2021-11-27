using System;
using System.Collections.Generic;
using UnityEngine;

public class VisualCalendar : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] CalendarItem yearPrefab;
	[SerializeField] CalendarItem monthPrefab;
	[SerializeField] CalendarItem dayPrefab;

	[SerializeField] RectTransform yearTab;
	[SerializeField] RectTransform monthTab;
	[SerializeField] RectTransform dayTab;

	[SerializeField] RectTransform dateSwitchContent;
	[SerializeField] RectTransform monthsContent;
	[SerializeField] RectTransform daysContent;

#pragma warning restore 0649

	const int MAX_DAY_ITEMS = 35;

	public void ShowYearTab(DateTime date)
	{
		ClearAllContent();
		DisableAllTabs();
		yearTab.gameObject.SetActive(true);

		CreateItem(date, CalendarItemType.Year, false, dateSwitchContent);

		for (int i = 1; i <= 12; ++i)
		{
			DateTime monthDate = new DateTime(date.Year, i, 1);
			CreateItem(monthDate, CalendarItemType.Month, false, monthsContent);
		}
	}

	public void ShowMonthTab(DateTime date)
	{
		ClearAllContent();
		DisableAllTabs();
		monthTab.gameObject.SetActive(true);

		CreateItem(date, CalendarItemType.Month, false, dateSwitchContent);

		CreateDaysFromPreviousMonth(date);
		for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); ++i)
		{
			DateTime dayDate = new DateTime(date.Year, date.Month, i);
			CreateItem(dayDate, CalendarItemType.Day, false, daysContent);
		}
		CreateDaysFromNextMonth(date);
	}

	public void ShowDayTab(DateTime date)
	{
		ClearAllContent();
		DisableAllTabs();
		dayTab.gameObject.SetActive(true);

		// ...
	}

	void CreateDaysFromPreviousMonth(DateTime date)
	{
		DateTime givenMonth = new DateTime(date.Year, date.Month, 1);

		if (givenMonth.DayOfWeek != DayOfWeek.Monday)
		{
			DateTime previousMonth = date.AddDays(-1);

			while (previousMonth.DayOfWeek != DayOfWeek.Monday)
			{
				previousMonth = previousMonth.AddDays(-1);
			}

			while (previousMonth.Day != 1)
			{
				CreateItem(previousMonth, CalendarItemType.Day, true, daysContent);
				previousMonth = previousMonth.AddDays(1);
			}
		}
	}

	void CreateDaysFromNextMonth(DateTime date)
	{
		DateTime givenMonth = new DateTime(date.Year, date.Month, 1);
		DateTime nextMonth = givenMonth.AddMonths(1);

		while (daysContent.childCount < MAX_DAY_ITEMS)
		{
			CreateItem(nextMonth, CalendarItemType.Day, true, daysContent);
			nextMonth = nextMonth.AddDays(1);
		}
	}

	CalendarItem CreateItem(DateTime date, CalendarItemType type, bool grayOut, RectTransform content)
	{
		CalendarItem item = null;

		switch (type)
		{
			case CalendarItemType.Year:
				item = Instantiate(yearPrefab);
				break;

			case CalendarItemType.Month:
				item = Instantiate(monthPrefab);
				break;

			case CalendarItemType.Day:
				item = Instantiate(dayPrefab);
				break;
		}

		item.Init(date, type, grayOut);
		item.transform.SetParent(content, false);
		item.OnClicked += ItemClicked;

		return item;
	}

	void DisableAllTabs()
	{
		yearTab.gameObject.SetActive(false);
		monthTab.gameObject.SetActive(false);
		dayTab.gameObject.SetActive(false);
	}

	void ClearAllContent()
	{
		ClearContent(dateSwitchContent);
		ClearContent(monthsContent);
		ClearContent(daysContent);
	}

	void ClearContent(RectTransform transform)
	{
		foreach (RectTransform obj in transform)
		{
			Destroy(obj.gameObject);
		}
	}

	// Events
	void ItemClicked(CalendarItem item)
	{
		switch (item.Type)
		{
			case CalendarItemType.Year:

				break;

			case CalendarItemType.Month:
				ShowMonthTab(item.Date);
				break;

			case CalendarItemType.Day:
				
				break;
		}
	}
	//
}