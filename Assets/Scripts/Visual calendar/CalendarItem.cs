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
	[SerializeField] CanvasGroup highlight;
	[SerializeField] Color highlightColor;
	[SerializeField] Image progressFill;

#pragma warning restore 0649

	public DateTime Date { get; private set; }
	public CalendarItemType Type { get; private set; }

	public event Action<CalendarItem> OnClicked;

	public void Init(DateTime date, CalendarItemType type, bool isGrayOut, bool isHighlighted)
	{
		Date = date;
		Type = type;

		if (isGrayOut && !isHighlighted)
		{
			grayOut.alpha = 0.35f;
		}

		if (isHighlighted)
		{
			highlight.alpha = 0.35f;
			progressFill.color = highlightColor;
		}

		// Update date visual
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

		UpdateHoursVisual(date);
	}

	void UpdateHoursVisual(DateTime date)
	{
		Upgrade activeUpgrade = UpgradeDropdown.Instance.GetActive();
		int secondsInMyDate = 0;

		switch (Type)
		{
			case CalendarItemType.Year:
				DateTime startOfTheYear = new DateTime(date.Year, 1, 1);
				DateTime endOfTheYear = new DateTime(date.Year, 12, 31);

				secondsInMyDate = activeUpgrade.Calendar.GetValueInDiapason(startOfTheYear, endOfTheYear);
				break;

			case CalendarItemType.Month:
				int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
				DateTime startOfTheMonth = new DateTime(date.Year, date.Month, 1);
				DateTime endOfTheMonth = new DateTime(date.Year, date.Month, daysInMonth);

				secondsInMyDate = activeUpgrade.Calendar.GetValueInDiapason(startOfTheMonth, endOfTheMonth);
				break;

			case CalendarItemType.Day:
				secondsInMyDate = activeUpgrade.Calendar.GetValue(date);
				break;
		}

		hoursText.text = TimeConverter.TimeString(secondsInMyDate);
	}

	// Events
	void SelfButtonClicked()
	{
		OnClicked?.Invoke(this);
	}

	void ActiveUpgradeChanged(Upgrade upgrade)
	{
		UpdateHoursVisual(Date);
	}
	//

	private void OnEnable()
	{
		selfButton?.onClick.AddListener(SelfButtonClicked);

		UpgradeDropdown.Instance.OnActiveUpgradeChanged += ActiveUpgradeChanged;
	}

	private void OnDisable()
	{
		selfButton?.onClick.RemoveListener(SelfButtonClicked);

		UpgradeDropdown.Instance.OnActiveUpgradeChanged -= ActiveUpgradeChanged;
	}
}