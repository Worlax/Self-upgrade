using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualCalendar : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] HeaderContent header;
	[SerializeField] YearContent yearTab;
	[SerializeField] MonthContent monthTab;
	[SerializeField] DayContent dayTab;

	[SerializeField] Button priviousDate;
	[SerializeField] Button nextDate;
	[SerializeField] Button zoomOut;

#pragma warning restore 0649

	// Open tab
	public void OpenYearTab(DateTime date)
	{
		EnableOneTab(yearTab.transform);

		header.FillContent(CalendarItemType.Year, date);
		List<CalendarItem> createdItems = yearTab.FillContent(date);
		foreach (CalendarItem item in createdItems)
		{
			item.OnClicked += ZoomIn;
		}
	}

	public void OpenMonthTab(DateTime date)
	{
		EnableOneTab(monthTab.transform);

		header.FillContent(CalendarItemType.Month, date);
		List<CalendarItem> createdItems = monthTab.FillContent(date);
		foreach (CalendarItem item in createdItems)
		{
			item.OnClicked += ZoomIn;
		}
	}

	public void OpenDayTab(DateTime date)
	{
		EnableOneTab(dayTab.transform);

		header.FillContent(CalendarItemType.Day, date);
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
	void SwitchDate(int by)
	{
		CalendarItem head = header.GetItem();

		if (head)
		{
			switch (head.Type)
			{
				case CalendarItemType.Year:
					OpenYearTab(head.Date.AddYears(by));
					break;

				case CalendarItemType.Month:
					OpenMonthTab(head.Date.AddMonths(by));
					break;

				case CalendarItemType.Day:
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

	void ZoomIn(CalendarItem item)
	{
		switch (item.Type)
		{
			case CalendarItemType.Month:
				OpenMonthTab(item.Date);
				zoomOut.interactable = true;
				break;

			case CalendarItemType.Day:
				OpenDayTab(item.Date);
				zoomOut.interactable = true;
				break;
		}
	}

	void ZoomOut()
	{
		CalendarItem Head = header.GetItem();

		switch (Head.Type)
		{
			case CalendarItemType.Year:
				break;

			case CalendarItemType.Month:
				zoomOut.interactable = false;
				OpenYearTab(Head.Date);
				break;

			case CalendarItemType.Day:
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
	}

	private void OnDisable()
	{
		priviousDate.onClick.RemoveListener(ShowPreviousDate);
		nextDate.onClick.RemoveListener(ShowNextDate);

		zoomOut.onClick.RemoveListener(ZoomOut);
	}
}