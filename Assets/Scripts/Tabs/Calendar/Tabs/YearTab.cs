using System;
using System.Collections.Generic;
using UnityEngine;

public class YearTab : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform content;

#pragma warning restore 0649

	public static event Action OnEnabled;
	public static event Action OnDisabled;

	public List<UICalendarItem> FillContent(DateTime date)
	{
		ClearContent();

		List<UICalendarItem> createdItems = new List<UICalendarItem>();

		for (int i = 1; i <= 12; ++i)
		{
			DateTime monthDate = new DateTime(date.Year, i, 1);
			UICalendarItem item = UICalendarItemConstructor.Instance.CreateCalendarItem(UICalendarItemType.Month, monthDate, content);
			HighlightIfCurrent(item);
			createdItems.Add(item);
		}

		return createdItems;
	}

	void HighlightIfCurrent(UICalendarItem item)
	{
		if (item.Date.Year == DateTime.Today.Year 
			&& item.Date.Month == DateTime.Today.Month)
		{
			item.Highlight(true);
		}
	}

	void ClearContent()
	{
		foreach (Transform obj in content.transform)
		{
			Destroy(obj.gameObject);
		}
	}

	// Unity
	private void OnEnable()
	{
		OnEnabled?.Invoke();
	}

	private void OnDisable()
	{
		OnDisabled?.Invoke();
	}
}