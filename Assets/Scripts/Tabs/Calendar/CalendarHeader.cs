using System;
using UnityEngine;
using UnityEngine.UI;

public class CalendarHeader : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform content;
	[SerializeField] Text dateInText;

#pragma warning restore 0649

	public UICalendarItem FillContent(UICalendarItemType type, DateTime date)
	{
		ClearContent();
		ShowDateInText(date, type);
		return UICalendarItemConstructor.Instance.CreateItem(type, date, content);
	}

	public UICalendarItem GetItem()
	{
		return content.GetComponentInChildren<UICalendarItem>();
	}

	void ShowDateInText(DateTime date, UICalendarItemType type)
	{
		switch (type)
		{
			case UICalendarItemType.Year:
				dateInText.text = "";
				break;

			case UICalendarItemType.Month:
				dateInText.text = date.ToString("yyyy");
				break;

			case UICalendarItemType.Day:
				dateInText.text = date.ToString("yyyy MMMM");
				break;
		}
	}

	void ClearContent()
	{
		foreach(Transform obj in content.transform)
		{
			Destroy(obj.gameObject);
		}

		dateInText.text = "";
	}
}