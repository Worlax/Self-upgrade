using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICalendar : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] CalendarHeader header;
	[SerializeField] YearTab yearTab;
	[SerializeField] MonthTab monthTab;
	[SerializeField] DayTab dayTab;

	[SerializeField] Button priviousDate;
	[SerializeField] Button nextDate;
	[SerializeField] Button zoomOut;
	[SerializeField] Button findToday;

#pragma warning restore 0649

	// Open tab
	public void OpenYearTab(DateTime date)
	{
		EnableOneTab(yearTab.transform);

		header.FillContent(UICalendarItemType.Year, date);
		List<UICalendarItem> createdItems = yearTab.FillContent(date);
		foreach (UICalendarItem item in createdItems)
		{
			item.OnClicked += ZoomIn;
		}
	}

	public void OpenMonthTab(DateTime date)
	{
		EnableOneTab(monthTab.transform);

		header.FillContent(UICalendarItemType.Month, date);
		List<UICalendarItem> createdItems = monthTab.FillContent(date);
		foreach (UICalendarItem item in createdItems)
		{
			item.OnClicked += ZoomIn;
		}
	}

	public void OpenDayTab(DateTime date)
	{
		EnableOneTab(dayTab.transform);

		header.FillContent(UICalendarItemType.Day, date);
		dayTab.FillContent(date);
	}

	void EnableOneTab(Transform tab)
	{
		// Disable all tabs
		yearTab.gameObject.SetActive(false);
		monthTab.gameObject.SetActive(false);
		dayTab.gameObject.SetActive(false);

		// Enable one tab
		tab.gameObject.SetActive(true);
	}

	void UpdateVisual()
	{
		SwitchDate(0);
	}

	// Events
	void FindToday()
	{
		UICalendarItem head = header.GetItem();

		if (head)
		{
			switch (head.Type)
			{
				case UICalendarItemType.Year:
					OpenYearTab(DateTime.Today);
					break;

				case UICalendarItemType.Month:
					OpenMonthTab(DateTime.Today);
					break;

				case UICalendarItemType.Day:
					OpenDayTab(DateTime.Today);
					break;
			}
		}
	}

	void SwitchDate(int by)
	{
		UICalendarItem head = header.GetItem();

		if (head)
		{
			switch (head.Type)
			{
				case UICalendarItemType.Year:
					OpenYearTab(head.Date.AddYears(by));
					break;

				case UICalendarItemType.Month:
					OpenMonthTab(head.Date.AddMonths(by));
					break;

				case UICalendarItemType.Day:
					OpenDayTab(head.Date.AddDays(by));
					break;
			}
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

	void ZoomIn(UICalendarItem item)
	{
		switch (item.Type)
		{
			case UICalendarItemType.Month:
				OpenMonthTab(item.Date);
				zoomOut.interactable = true;
				break;

			case UICalendarItemType.Day:
				OpenDayTab(item.Date);
				zoomOut.interactable = true;
				break;
		}
	}

	void ZoomOut()
	{
		UICalendarItem Head = header.GetItem();

		switch (Head.Type)
		{
			case UICalendarItemType.Year:
				break;

			case UICalendarItemType.Month:
				zoomOut.interactable = false;
				OpenYearTab(Head.Date);
				break;

			case UICalendarItemType.Day:
				OpenMonthTab(Head.Date);
				break;
		}
	}

	// Unity
	private void OnEnable()
	{
		UpdateVisual();

		priviousDate.onClick.AddListener(ShowPreviousDate);
		nextDate.onClick.AddListener(ShowNextDate);
		zoomOut.onClick.AddListener(ZoomOut);
		findToday.onClick.AddListener(FindToday);
	}

	private void OnDisable()
	{
		priviousDate.onClick.RemoveListener(ShowPreviousDate);
		nextDate.onClick.RemoveListener(ShowNextDate);
		zoomOut.onClick.RemoveListener(ZoomOut);
		findToday.onClick.RemoveListener(FindToday);
	}
}