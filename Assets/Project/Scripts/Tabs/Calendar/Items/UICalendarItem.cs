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

	[SerializeField] Slider progressBar;
	[SerializeField] Image progressBarHighlight;
	[SerializeField] Text ProgressBarText;

#pragma warning restore 0649

	Color normalColor;

	public DateTime Date { get; private set; }
	public UICalendarItemType Type { get; private set; }

	public event Action<UICalendarItem> OnClicked;

	public void Init(UICalendarItemType type, DateTime date)
	{
		Date = date;
		Type = type;
		normalColor = progressBarHighlight.color;

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
			progressBarHighlight.color = highlightColor;
		}
		else
		{
			highlight.alpha = 0f;
			progressBarHighlight.color = normalColor;
		}
	}

	void UpdateHoursVisual(DateTime date)
	{
		IReadOnlyList<Upgrade> activeUpgrades = UpgradeList.Instance.GetActive();
		int secondsInMyDate = 0;

		switch (Type)
		{
			case UICalendarItemType.Year:
				DateTime startOfTheYear = new DateTime(date.Year, 1, 1);
				DateTime endOfTheYear = new DateTime(date.Year, 12, 31);

				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate += upgrade.Progress.GetValueInDiapason(startOfTheYear, endOfTheYear);
				}

				UpdateScheduleVisual(startOfTheYear, endOfTheYear);
				break;

			case UICalendarItemType.Month:
				int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
				DateTime startOfTheMonth = new DateTime(date.Year, date.Month, 1);
				DateTime endOfTheMonth = new DateTime(date.Year, date.Month, daysInMonth);

				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate += upgrade.Progress.GetValueInDiapason(startOfTheMonth, endOfTheMonth);
				}

				UpdateScheduleVisual(startOfTheMonth, endOfTheMonth);
				break;

			case UICalendarItemType.Day:
				foreach (Upgrade upgrade in activeUpgrades)
				{
					secondsInMyDate += upgrade.Progress.GetValue(date);
				}

				UpdateScheduleVisual(date, date);
				break;
		}

		timeText.text = TimeConverter.TimeString(secondsInMyDate, false);
	}

	void UpdateScheduleVisual(DateTime dateStart, DateTime dateEnd)
	{
		IReadOnlyList<Upgrade> activeUpgrades = UpgradeList.Instance.GetActive();
		double schedule = 0;
		bool haveProgressMissions = false;

		foreach (Upgrade upgrade in activeUpgrades)
		{
			MissionCalendar calendar = upgrade.Progress.MissionCalendar;
			if (calendar.IsDiapasonHaveMissionProgress(dateStart, dateEnd))
			{
				schedule += calendar.GetScheduleCompletionInDiapason(dateStart, dateEnd);
				haveProgressMissions = true;
			}
		}

		if (haveProgressMissions)
		{
			schedule = Math.Truncate(schedule * 100) / 100;

			progressBar.value = (float)schedule;
			ProgressBarText.text = (schedule * 100).ToString() + "%";
		}
		else
		{
			progressBar.value = 0;
			ProgressBarText.text = "";
		}
	}

	// Events
	void SelfButtonClicked()
	{
		OnClicked?.Invoke(this);
	}

	void ActiveUpgradesChanged(IReadOnlyList<Upgrade> upgrade)
	{
		UpdateHoursVisual(Date);
	}
	//

	private void OnEnable()
	{
		selfButton?.onClick.AddListener(SelfButtonClicked);

		UpgradeList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}

	private void OnDisable()
	{
		selfButton?.onClick.RemoveListener(SelfButtonClicked);

		UpgradeList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;
	}
}