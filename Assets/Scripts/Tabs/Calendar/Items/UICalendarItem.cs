using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UICalendarItemType
{
	Year, Month, Day
}

public class UICalendarItem : MonoBehaviour, IHighlightable
{
#pragma warning disable 0649

	[SerializeField] Text dateText;
	[SerializeField] Text timeText;

	[SerializeField] Button selfButton;

	[SerializeField] CanvasGroup grayOut;
	[SerializeField] CanvasGroup highlight;
	[SerializeField] Color highlightColor;
	[SerializeField] Image progressFill;

	[SerializeField] Scrollbar scheduleScroll;
	[SerializeField] Text scheduleText;

#pragma warning restore 0649

	Color normalColor;

	public DateTime Date { get; private set; }
	public UICalendarItemType Type { get; private set; }

	public event Action<UICalendarItem> OnClicked;

	public void Init(UICalendarItemType type, DateTime date)
	{
		Date = date;
		Type = type;
		normalColor = progressFill.color;

		// Update date visual
		switch (Type)
		{
			case UICalendarItemType.Year:
				dateText.text = Date.Year.ToString();
				break;

			case UICalendarItemType.Month:
				dateText.text = Date.ToString("MMM");
				break;

			case UICalendarItemType.Day:
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
		List<Upgrade> activeUpgrades = UpgradesList.Instance.GetActive();
		int secondsInMyDate = 0;

		switch (Type)
		{
			case UICalendarItemType.Year:
				DateTime startOfTheYear = new DateTime(date.Year, 1, 1);
				DateTime endOfTheYear = new DateTime(date.Year, 12, 31);

				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate += upgrade.Calendar.GetValueInDiapason(startOfTheYear, endOfTheYear);
				}

				UpdateScheduleVisual(startOfTheYear, endOfTheYear);
				break;

			case UICalendarItemType.Month:
				int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
				DateTime startOfTheMonth = new DateTime(date.Year, date.Month, 1);
				DateTime endOfTheMonth = new DateTime(date.Year, date.Month, daysInMonth);

				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate += upgrade.Calendar.GetValueInDiapason(startOfTheMonth, endOfTheMonth);
				}

				UpdateScheduleVisual(startOfTheMonth, endOfTheMonth);
				break;

			case UICalendarItemType.Day:
				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate += upgrade.Calendar.GetValue(date);
				}

				UpdateScheduleVisual(date, date);
				break;
		}

		timeText.text = TimeConverter.TimeString(secondsInMyDate);
	}

	void UpdateScheduleVisual(DateTime dateStart, DateTime dateEnd)
	{
		List<Upgrade> activeUpgrades = UpgradesList.Instance.GetActive();
		double schedule = 0;

		foreach (Upgrade upgrade in activeUpgrades)
		{
			schedule += upgrade.Calendar.GetScheduleCompletionInDiapason(dateStart, dateEnd);
		}

		print("From: " + dateStart.ToString("dd") + " To: " + dateEnd.ToString("dd") + " Schedule: " + schedule);

		if (schedule == -1)
		{
			//schedule
		}
		else
		{

		}
		schedule = Math.Truncate(schedule * 100) / 100;

		scheduleScroll.size = (float)schedule;
		scheduleText.text = (schedule * 100).ToString() + "%";
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

		UpgradesList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}

	private void OnDisable()
	{
		selfButton?.onClick.RemoveListener(SelfButtonClicked);

		UpgradesList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;
	}
}