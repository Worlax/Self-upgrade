using System;
using System.Collections.Generic;
using UnityEngine;

public class VisualCalendar : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] CalendarItem yearPrefab;
	[SerializeField] CalendarItem monthPrefab;
	[SerializeField] CalendarItem dayPrefab;

	[SerializeField] RectTransform dateSwitchContent;
	[SerializeField] RectTransform monthsContent;
	[SerializeField] RectTransform daysContent;

#pragma warning restore 0649

	public void ShowYear(DateTime date)
	{
		ClearAllContent();

		CalendarItem itemYear = Instantiate(yearPrefab, dateSwitchContent);
		itemYear.Init(date, ClendarItemType.Year);

		for (int i = 1; i <= 12; ++i)
		{
			DateTime monthDate = new DateTime(date.Year, i, 1);

			CalendarItem itemMonth = Instantiate(monthPrefab, monthsContent);
			itemMonth.Init(monthDate, ClendarItemType.Month);
		}
	}

	public void ShowMonth(DateTime date)
	{

	}

	public void ShowDay(DateTime date)
	{

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
}