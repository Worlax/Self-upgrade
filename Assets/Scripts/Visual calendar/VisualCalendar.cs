using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VisualCalendar : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform yearTab;
	[SerializeField] RectTransform monthTab;
	[SerializeField] RectTransform dayTab;

	[SerializeField] CalendarItem yearPrefab;
	[SerializeField] CalendarItem monthPrefab;
	[SerializeField] CalendarItem dayPrefab;

	[SerializeField] RectTransform monthsContent;
	[SerializeField] RectTransform daysContent;

	[SerializeField] RectTransform dateHeaderContent;
	[SerializeField] Button dateHeaderSwitchPrevious;
	[SerializeField] Button dateHeaderSwitchNext;

	[SerializeField] Text dateInText;

	[SerializeField] Button back;

#pragma warning restore 0649

	const int MAX_DAY_ITEMS = 42;

	// Open tab
	public void OpenYearTab(DateTime date)
	{
		ClearAllAndEnableOneTab(yearTab);

		date = new DateTime(date.Year, 1, 1);
		back.interactable = false;
		CreateHeader(date, CalendarItemType.Year);
		for (int i = 1; i <= 12; ++i)
		{
			DateTime monthDate = new DateTime(date.Year, i, 1);
			CreateItem(monthDate, CalendarItemType.Month, monthsContent, false, true);
		}
	}

	public void OpenMonthTab(DateTime date)
	{
		ClearAllAndEnableOneTab(monthTab);

		date = new DateTime(date.Year, date.Month, 1);
		back.interactable = true;
		CreateHeader(date, CalendarItemType.Month);
		CreateDaysFromPreviousMonth(date);
		for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); ++i)
		{
			DateTime dayDate = new DateTime(date.Year, date.Month, i);

			CreateItem(dayDate, CalendarItemType.Day, daysContent, false, true);
		}
		CreateDaysFromNextMonth(date);
	}

	public void OpenDayTab(DateTime date)
	{
		ClearAllAndEnableOneTab(dayTab);

		back.interactable = true;
		// ...
	}

	void SwitchDate(int by)
	{
		CalendarItem item = dateHeaderContent.GetComponentInChildren<CalendarItem>();

		if (item)
		{
			switch (item.Type)
			{
				case CalendarItemType.Year:
					OpenYearTab(item.Date.AddYears(by));
					break;

				case CalendarItemType.Month:
					OpenMonthTab(item.Date.AddMonths(by));
					break;

				case CalendarItemType.Day:
					OpenDayTab(item.Date.AddDays(by));
					break;
			}
		}
	}
	//

	void UpdateVisual()
	{
		CalendarItem header = dateHeaderContent.GetComponentInChildren<CalendarItem>();

		if (header)
		{
			switch (header.Type)
			{
				case CalendarItemType.Year:
					OpenYearTab(header.Date);
					break;

				case CalendarItemType.Month:
					OpenMonthTab(header.Date);
					break;

				case CalendarItemType.Day:
					OpenDayTab(header.Date);
					break;
			}
		}
	}

	// Create item
	CalendarItem CreateItem(DateTime date, CalendarItemType type, RectTransform content, bool grayOut, bool highlightedIfToday)
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

		bool highlight = false;
		if (highlightedIfToday)
		{
			highlight = IsToday(date, type);
		}

		item.Init(date, type, grayOut, highlight);
		item.transform.SetParent(content, false);
		item.OnClicked += ItemClicked;

		return item;
	}

	bool IsToday(DateTime date, CalendarItemType type)
	{
		bool isToday = false;

		switch (type)
		{
			case CalendarItemType.Year:
				isToday = date.Year == DateTime.Today.Year;
				break;

			case CalendarItemType.Month:
				isToday = date.Year == DateTime.Today.Year
					&& date.Month == DateTime.Today.Month;
				break;

			case CalendarItemType.Day:
				isToday = date.Year == DateTime.Today.Year
					&& date.Month == DateTime.Today.Month
					&& date.Day == DateTime.Today.Day;
				break;
		}

		return isToday;
	}

	CalendarItem CreateHeader(DateTime date, CalendarItemType type)
	{
		CalendarItem item = CreateItem(date, type, dateHeaderContent, false, false);
		ShowDateInText(date, type);

		return item;
	}

	void ShowDateInText(DateTime date, CalendarItemType type)
	{
		switch (type)
		{
			case CalendarItemType.Year:
				dateInText.text = "";
				break;

			case CalendarItemType.Month:
				dateInText.text = date.ToString("yyyy");
				break;

			case CalendarItemType.Day:
				dateInText.text = date.ToString("yyyy MMMM");
				break;
		}
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
				CreateItem(previousMonth, CalendarItemType.Day, daysContent, true, true);
				previousMonth = previousMonth.AddDays(1);
			}
		}
	}

	void CreateDaysFromNextMonth(DateTime date)
	{
		// Counting already created items
		int itemsAlreadyCreated = 0;
		itemsAlreadyCreated += DateTime.DaysInMonth(date.Year, date.Month);

		DateTime lookingForFirstMonday = new DateTime(date.Year, date.Month, 1);
		while (lookingForFirstMonday.DayOfWeek != DayOfWeek.Monday)
		{
			++itemsAlreadyCreated;
			lookingForFirstMonday = lookingForFirstMonday.AddDays(-1);
		}

		// Filling content with items from the next month
		DateTime givenMonth = new DateTime(date.Year, date.Month, 1);
		DateTime nextMonth = givenMonth.AddMonths(1);

		while (itemsAlreadyCreated < MAX_DAY_ITEMS)
		{
			CreateItem(nextMonth, CalendarItemType.Day, daysContent, true, true);
			nextMonth = nextMonth.AddDays(1);
			++itemsAlreadyCreated;
		}
	}
	//

	// Clear data
	void ClearAllAndEnableOneTab(RectTransform tab)
	{
		// Clear all content
		ClearContent(dateHeaderContent);
		ClearContent(monthsContent);
		ClearContent(daysContent);
		dateInText.text = "";
		back.interactable = false;

		// Disable all tabs
		yearTab.gameObject.SetActive(false);
		monthTab.gameObject.SetActive(false);
		dayTab.gameObject.SetActive(false);

		// Enable one tab
		tab.gameObject.SetActive(true);
	}

	void ClearContent(RectTransform transform)
	{
		foreach (RectTransform obj in transform)
		{
			Destroy(obj.gameObject);
		}
	}
	//

	// Events
	void ItemClicked(CalendarItem item)
	{
		switch (item.Type)
		{
			case CalendarItemType.Year:

				break;

			case CalendarItemType.Month:
				OpenMonthTab(item.Date);
				break;

			case CalendarItemType.Day:
				
				break;
		}
	}

	void ShowPreviousDate()
	{
		SwitchDate(-1);
	}

	void ShowNextDate()
	{
		SwitchDate(1);
	}

	void Back()
	{
		CalendarItem item = dateHeaderContent.GetComponentInChildren<CalendarItem>();

		if (item)
		{
			switch (item.Type)
			{
				case CalendarItemType.Year:
					break;

				case CalendarItemType.Month:
					OpenYearTab(item.Date);
					break;

				case CalendarItemType.Day:
					OpenMonthTab(item.Date);
					break;
			}
		}
	}
	//

	// Unity
	private void OnEnable()
	{
		UpdateVisual();

		dateHeaderSwitchPrevious.onClick.AddListener(ShowPreviousDate);
		dateHeaderSwitchNext.onClick.AddListener(ShowNextDate);

		back.onClick.AddListener(Back);
	}

	private void OnDisable()
	{
		dateHeaderSwitchPrevious.onClick.RemoveListener(ShowPreviousDate);
		dateHeaderSwitchNext.onClick.RemoveListener(ShowNextDate);

		back.onClick.RemoveListener(Back);
	}
	//
}