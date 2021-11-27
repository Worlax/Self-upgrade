using System;
using UnityEngine;
using UnityEngine.UI;

public enum CalendarItemType
{
	Year, Month, Day
}

public class CalendarItem : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text dateText;
	[SerializeField] Text hoursText;

	[SerializeField] Button selfButton;

	[SerializeField] CanvasGroup grayOut;

#pragma warning restore 0649

	public DateTime Date { get; private set; }
	public CalendarItemType Type { get; private set; }

	public event Action<CalendarItem> OnClicked;

	public void Init(DateTime dateTime, CalendarItemType type, bool isGrayOut)
	{
		Date = dateTime;
		Type = type;

		if (isGrayOut)
		{
			grayOut.alpha = 0.35f;
		}

		switch (Type)
		{
			case CalendarItemType.Year:
				dateText.text = Date.Year.ToString();
				break;

			case CalendarItemType.Month:
				dateText.text = Date.ToString("MMM");
				break;

			case CalendarItemType.Day:
				dateText.text = Date.Day.ToString();
				break;
		}

		hoursText.text = "0h 0m";
	}

	// Events
	void SelfButtonClicked()
	{
		OnClicked?.Invoke(this);
	}
	//

	private void OnEnable()
	{
		selfButton?.onClick.AddListener(SelfButtonClicked);
	}

	private void OnDisable()
	{
		selfButton?.onClick.RemoveListener(SelfButtonClicked);
	}
}