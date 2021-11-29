using System;
using System.Collections.Generic;
using UnityEngine;

public class YearContent : MonoBehaviour
{
	public List<CalendarItem> FillContent(DateTime date)
	{
		ClearContent();

		List<CalendarItem> createdItems = new List<CalendarItem>();

		for (int i = 1; i <= 12; ++i)
		{
			DateTime monthDate = new DateTime(date.Year, i, 1);
			CalendarItem item = ContentPrefabs.Instance.GetItem(CalendarItemType.Month, monthDate, transform);
			HighlightIfCurrent(item);
			createdItems.Add(item);
		}

		return createdItems;
	}

	void HighlightIfCurrent(CalendarItem item)
	{
		if (item.Date.Year == DateTime.Today.Year 
			&& item.Date.Month == DateTime.Today.Month)
		{
			item.Highlight(true);
		}
	}

	void ClearContent()
	{
		foreach (Transform obj in transform)
		{
			Destroy(obj.gameObject);
		}
	}
}