using System;
using System.Collections.Generic;
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
			grayOut.alpha = 0f;
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
			highlight.alpha = 0f;
			progressFill.color = normalColor;
		}
	}

	void UpdateHoursVisual(DateTime date)
	{
		List<Upgrade> activeUpgrades = UpgradeDropdown.Instance.GetActive();
		int secondsInMyDate = 0;

		switch (Type)
		{
			case CalendarItemType.Year:
				DateTime startOfTheYear = new DateTime(date.Year, 1, 1);
				DateTime endOfTheYear = new DateTime(date.Year, 12, 31);

				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate = upgrade.Calendar.GetValueInDiapason(startOfTheYear, endOfTheYear);
				}
				break;

			case CalendarItemType.Month:
				int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
				DateTime startOfTheMonth = new DateTime(date.Year, date.Month, 1);
				DateTime endOfTheMonth = new DateTime(date.Year, date.Month, daysInMonth);

				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate = upgrade.Calendar.GetValueInDiapason(startOfTheMonth, endOfTheMonth);
				}
				break;

			case CalendarItemType.Day:
				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate = upgrade.Calendar.GetValue(date);
				}
				break;
		}

		hoursText.text = TimeConverter.TimeString(secondsInMyDate);
	}

	// Events
	void SelfButtonClicked()
	{
		OnClicked?.Invoke(this);
	}

	void ActiveUpgradesChanged(List<Upgrade> upgrade)
	{
		UpdateHoursVisual(Date);
	}
	//

	private void OnEnable()
	{
		selfButton?.onClick.AddListener(SelfButtonClicked);

		UpgradeDropdown.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}

	private void OnDisable()
	{
		selfButton?.onClick.RemoveListener(SelfButtonClicked);

		UpgradeDropdown.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;
	}
}