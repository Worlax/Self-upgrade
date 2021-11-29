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

	Color normalColor;

	public DateTime Date { get; private set; }
	public CalendarItemType Type { get; private set; }

	public event Action<CalendarItem> OnClicked;

	public void Init(CalendarItemType type, DateTime date)
	{
		Date = date;
		Type = type;
		normalColor = progressFill.color;

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

	public void GrayOut(bool value)
	{
		if (value)
		{
			Highlight(false);

			grayOut.alpha = 0.35f;
		}
		else
		{
			grayOut.alpha = 1f;
		}
	}

	public void Highlight(bool value)
	{
		if (value)
		{
			GrayOut(false);

			highlight.alpha = 0.35f;
			progressFill.color = highlightColor;
		}
		else
		{
			highlight.alpha = 1f;
			progressFill.color = normalColor;
		}
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