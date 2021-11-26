using System;
using UnityEngine;
using UnityEngine.UI;

public enum ClendarItemType
{
	Year, Month, Day
}

public class CalendarItem : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text date;
	[SerializeField] Text hours;

#pragma warning restore 0649

	public DateTime DateTime { get; private set; }
	public ClendarItemType Type { get; private set; }

	public void Init(DateTime dateTime, ClendarItemType type)
	{
		DateTime = dateTime;
		Type = type;

		switch (Type)
		{
			case ClendarItemType.Year:
				date.text = DateTime.Year.ToString();
				break;

			case ClendarItemType.Month:
				date.text = DateTime.ToString("MMM");
				break;

			case ClendarItemType.Day:
				date.text = DateTime.Day.ToString();
				break;
		}

		hours.text = "0h 0m";
	}
}