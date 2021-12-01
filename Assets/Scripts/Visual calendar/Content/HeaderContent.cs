using System;
using UnityEngine;
using UnityEngine.UI;

public class HeaderContent : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform content;
	[SerializeField] Text dateInText;

#pragma warning restore 0649

	public CalendarItem FillContent(CalendarItemType type, DateTime date)
	{
		ClearContent();
		ShowDateInText(date, type);
		return ContentPrefabs.Instance.GetItem(type, date, content);
	}

	public CalendarItem GetItem()
	{
		return content.GetComponentInChildren<CalendarItem>();
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

	void ClearContent()
	{
		foreach(Transform obj in content.transform)
		{
			Destroy(obj.gameObject);
		}

		dateInText.text = "";
	}
}