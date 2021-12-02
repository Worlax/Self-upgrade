using System;
using System.Collections.Generic;
using UnityEngine;

public class MonthTab : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform content;

#pragma warning restore 0649

	const int MAX_ITEMS = 42;

	public List<UICalendarItem> FillContent(DateTime date)
	{
		ClearContent();
		List<UICalendarItem> createdItems = new List<UICalendarItem>();

		// Previous month
		List<UICalendarItem> previousMonth = CreateDaysFromPreviousMonth(date);
		GrayOutItems(previousMonth);
		createdItems.AddRange(previousMonth);

		// This month
		for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); ++i)
		{
			DateTime dayDate = new DateTime(date.Year, date.Month, i);
			UICalendarItem item = UICalendarItemConstructor.Instance.CreateItem(UICalendarItemType.Day, dayDate, content);
			HighlightIfCurrent(item);
			createdItems.Add(item);
		}

		// Next month
		List<UICalendarItem> nextMonth = CreateDaysFromNextMonth(date);
		GrayOutItems(nextMonth);
		createdItems.AddRange(nextMonth);

		return createdItems;
	}

	List<UICalendarItem> CreateDaysFromPreviousMonth(DateTime date)
	{
		List<UICalendarItem> createdItems = new List<UICalendarItem>();

		DateTime firstDayOfGivenMonth = new DateTime(date.Year, date.Month, 1);

		// If current month is not starting from monday -
		// adding days from previous month to the content until content starts from monday
		if (firstDayOfGivenMonth.DayOfWeek != DayOfWeek.Monday)
		{
			DateTime previousMonth = firstDayOfGivenMonth.AddDays(-1);

			// Trying to find last monday in previous month
			while (previousMonth.DayOfWeek != DayOfWeek.Monday)
			{
				previousMonth = previousMonth.AddDays(-1);
			}

			// Adding all days (from the last month's monday) to the content
			while (previousMonth.Day != 1)
			{
				UICalendarItem item = UICalendarItemConstructor.Instance.CreateItem(UICalendarItemType.Day, previousMonth, content);
				createdItems.Add(item);
				previousMonth = previousMonth.AddDays(1);
			}
		}

		return createdItems;
	}

	List<UICalendarItem> CreateDaysFromNextMonth(DateTime date)
	{
		List<UICalendarItem> createdItems = new List<UICalendarItem>();

		// Counting already created items
		int itemsAlreadyCreated = 0;
		itemsAlreadyCreated += DateTime.DaysInMonth(date.Year, date.Month);

		// If items from previous month was created (in terms to start content from monday) -
		// adding them to the count
		DateTime lookingForFirstMonday = new DateTime(date.Year, date.Month, 1);
		while (lookingForFirstMonday.DayOfWeek != DayOfWeek.Monday)
		{
			++itemsAlreadyCreated;
			lookingForFirstMonday = lookingForFirstMonday.AddDays(-1);
		}

		// Filling content with items from the next month
		DateTime givenMonth = new DateTime(date.Year, date.Month, 1);
		DateTime nextMonth = givenMonth.AddMonths(1);

		while (itemsAlreadyCreated < MAX_ITEMS)
		{
			UICalendarItem item = UICalendarItemConstructor.Instance.CreateItem(UICalendarItemType.Day, nextMonth, content);
			HighlightIfCurrent(item);
			createdItems.Add(item);
			nextMonth = nextMonth.AddDays(1);
			++itemsAlreadyCreated;
		}

		return createdItems;
	}

	void HighlightIfCurrent(UICalendarItem item)
	{
		if (item.Date.Year == DateTime.Today.Year
			&& item.Date.Month == DateTime.Today.Month
			&& item.Date.Day == DateTime.Today.Day)
		{
			item.Highlight(true);
		}
	}

	void GrayOutItems(List<UICalendarItem> items)
	{
		foreach(UICalendarItem item in items)
		{
			item.GrayOut(true);
		}
	}

	void ClearContent()
	{
		foreach (Transform obj in content.transform)
		{
			Destroy(obj.gameObject);
		}
	}
}