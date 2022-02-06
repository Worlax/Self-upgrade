using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticInfo : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] StatisticSettings settings;
	[SerializeField] bool comparisonInfo;

	[SerializeField] Text header;
	[SerializeField] Text dateScope;

	[SerializeField] Text totalUpgrades;
	[SerializeField] Text planned;
	[SerializeField] Text completed;
	[SerializeField] Text fullyCompleted;
	[SerializeField] Text failed;

	[SerializeField] Text completedPercent;
	[SerializeField] Text fullyCompletedPercent;
	[SerializeField] Text failedPercent;

#pragma warning restore 0649

	void UpdateVisual()
	{
		StatisticData data = GetStatisticData();

		DisplayHeader();
		DisplayScope(data);
		DisplayTime(data);
		DisplayPercents(data);
	}

	StatisticData GetStatisticData()
	{
		DateTime dateStart = settings.CalendarDependent.isOn ? GetDateStartCalendarDependent() : GetDateStart();
		DateTime dateEnd = settings.CalendarDependent.isOn ? GetDateEndCalendarDependent() : GetDateEnd();

		return new StatisticData(dateStart, dateEnd);
	}

	// Get date scope
	DateTime GetDateStart()
	{
		int scope = GetStatisticScopeInDays();
		int daysIndent = comparisonInfo ? scope * 2 : scope;
		// Minus one day because today counts as starting point and boundaries are inclusive
		daysIndent -= 1;
		return DateTime.Today.AddDays(-daysIndent);
	}

	DateTime GetDateEnd()
	{
		int scope = GetStatisticScopeInDays();
		int daysIndent = comparisonInfo ? scope : 0;
		return DateTime.Today.AddDays(-daysIndent);
	}

	DateTime GetDateStartCalendarDependent()
	{
		DateTime today = DateTime.Today;
		DateTime returnDate = today;

		switch (settings.StatisticScope.GetActive())
		{
			case StatisticScopeDropdown.StatisticRange.Day:
				returnDate = comparisonInfo ? today.AddDays(-1) : today;
				break;

			case StatisticScopeDropdown.StatisticRange.Week:
				returnDate = comparisonInfo ? today.GetStartOfWeek().AddDays(-1).GetStartOfWeek() : today.GetStartOfWeek();
				break;

			case StatisticScopeDropdown.StatisticRange.Month:
				returnDate = comparisonInfo ? today.GetStartOfMonth().AddDays(-1).GetStartOfMonth() : today.GetStartOfMonth();
				break;

			case StatisticScopeDropdown.StatisticRange.Year:
				returnDate = comparisonInfo ? today.GetStartOfYear().AddDays(-1).GetStartOfYear() : today.GetStartOfYear();
				break;
		}

		return returnDate;
	}

	DateTime GetDateEndCalendarDependent()
	{
		DateTime today = DateTime.Today;
		DateTime returnDate = today;

		switch (settings.StatisticScope.GetActive())
		{
			case StatisticScopeDropdown.StatisticRange.Day:
				returnDate = comparisonInfo ? today.AddDays(-1) : today;
				break;

			case StatisticScopeDropdown.StatisticRange.Week:
				returnDate = comparisonInfo ? today.GetStartOfWeek().AddDays(-1) : today.GetEndOfWeek();
				break;

			case StatisticScopeDropdown.StatisticRange.Month:
				returnDate = comparisonInfo ? today.GetStartOfMonth().AddDays(-1) : today.GetEndOfMonth();
				break;

			case StatisticScopeDropdown.StatisticRange.Year:
				returnDate = comparisonInfo ? today.GetStartOfYear().AddDays(-1) : today.GetEndOfYear();
				break;
		}

		return returnDate > today ? today : returnDate;
	}

	int GetStatisticScopeInDays()
	{
		int range = 0;

		switch (settings.StatisticScope.GetActive())
		{
			case StatisticScopeDropdown.StatisticRange.Day:
				range = 1;
				break;

			case StatisticScopeDropdown.StatisticRange.Week:
				range = 7;
				break;

			case StatisticScopeDropdown.StatisticRange.Month:
				range = 31;
				break;

			case StatisticScopeDropdown.StatisticRange.Year:
				range = 365;
				break;
		}

		return range;
	}

	// Display
	void DisplayHeader()
	{
		string headerString = "";

		if (settings.CompareToAverage.isOn && comparisonInfo)
		{
			if (settings.DayAverage.isOn)
			{
				headerString = "Average day";
			}
			else
			{
				switch (settings.StatisticScope.GetActive())
				{
					case StatisticScopeDropdown.StatisticRange.Day:
						headerString = "Average day";
						break;

					case StatisticScopeDropdown.StatisticRange.Week:
						headerString = "Average week";
						break;

					case StatisticScopeDropdown.StatisticRange.Month:
						headerString = "Average month";
						break;

					case StatisticScopeDropdown.StatisticRange.Year:
						headerString = "Average year";
						break;
				}
			}
		}
		else
		{
			switch (settings.StatisticScope.GetActive())
			{
				case StatisticScopeDropdown.StatisticRange.Day:
					headerString = comparisonInfo ? "Yesterday" : "Today";
					break;

				case StatisticScopeDropdown.StatisticRange.Week:
					headerString = comparisonInfo ? "Previous week" : "Current week";
					break;

				case StatisticScopeDropdown.StatisticRange.Month:
					headerString = comparisonInfo ? "Previous month" : "Current month";
					break;

				case StatisticScopeDropdown.StatisticRange.Year:
					headerString = comparisonInfo ? "Previous year" : "Current year";
					break;
			}
		}

		header.text = headerString;
	}

	void DisplayScope(StatisticData data)
	{
		string from = data.DateStart.ToString("dd MMM");
		string to = data.DateEnd.ToString("dd MMM");
		string fullText;

		if (settings.StatisticScope.GetActive() == StatisticScopeDropdown.StatisticRange.Day)
		{
			fullText = from;
		}
		else
		{
			if (data.DateStart.Year != data.DateEnd.Year)
			{
				from += " (" + data.DateStart.Year + ")";
				to += " (" + data.DateEnd.Year + ")";
			}

			fullText = from + " - " + to;
		}

		dateScope.text = fullText;
	}

	void DisplayTime(StatisticData data)
	{
		int totalProgressSeconds;
		int plannedSeconds;
		int completedSeconds;
		int fullyCompletedSeconds;
		int failedSeconds;

		if (comparisonInfo && settings.CompareToAverage.isOn)
		{
			StatisticData globalData = GetGlobalData();

			int days = settings.DayAverage.isOn ? 1 : GetStatisticScopeInDays();

			totalProgressSeconds = globalData.TotalProgressMinutesAverage(days) * 60;
			plannedSeconds = globalData.PlannedMinutesAverage(days) * 60;
			completedSeconds = globalData.CompletedMinutesAverage(days) * 60;
			fullyCompletedSeconds = globalData.FullyCompletedMinutesAverage(days) * 60;
			failedSeconds = globalData.FailedMinutesAverage(days) * 60;
		}
		else
		{
			totalProgressSeconds = data.TotalProgressMinutes * 60;
			plannedSeconds = data.PlannedMinutes * 60;
			completedSeconds = data.CompletedMinutes * 60;
			fullyCompletedSeconds = data.FullyCompletedMinutes * 60;
			failedSeconds = data.FailedMinutes * 60;

			if (settings.DayAverage.isOn)
			{
				totalProgressSeconds /= data.DaysInDiapason;
				plannedSeconds /= data.DaysInDiapason;
				completedSeconds /= data.DaysInDiapason;
				fullyCompletedSeconds /= data.DaysInDiapason;
				failedSeconds /= data.DaysInDiapason;
			}
		}

		totalUpgrades.text = TimeConverter.TimeString(totalProgressSeconds, false);
		planned.text = TimeConverter.TimeString(plannedSeconds, false);
		completed.text = TimeConverter.TimeString(completedSeconds, false);
		fullyCompleted.text = TimeConverter.TimeString(fullyCompletedSeconds, false);
		failed.text = TimeConverter.TimeString(failedSeconds, false);
	}

	void DisplayPercents(StatisticData data)
	{
		if (data.PlannedMinutes == 0)
		{
			completedPercent.text = "";
			fullyCompletedPercent.text = "";
			failedPercent.text = "";
		}
		else
		{
			completedPercent.text = data.CompletedPercent.ToString() + "%";
			fullyCompletedPercent.text = data.FullyCompletedPercent.ToString() + "%";
			failedPercent.text = data.FailedPercent.ToString() + "%";
		}
	}

	StatisticData GetGlobalData()
	{
		return new StatisticData(GetFirstRecordedDay(), DateTime.Today);
	}

	DateTime GetFirstRecordedDay()
	{
		DateTime date = DateTime.Today;

		foreach (Upgrade upgrade in UpgradeList.Instance.GetActive())
		{
			DateTime upgradeDate = upgrade.Progress.GetFirstRecordedDay();
			date = date < upgradeDate ? date : upgradeDate;
		}

		return date;
	}

	// Events
	void ActiveUpgradesChanged(IReadOnlyList<Upgrade> upgrades)
	{
		UpdateVisual();
	}

	void SettingsChanged()
	{
		UpdateVisual();
	}

	// Unity
	private void OnEnable()
	{
		UpdateVisual();

		UpgradeList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
		settings.OnSettingsChanged += SettingsChanged;
	}

	private void OnDisable()
	{
		UpgradeList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;
		settings.OnSettingsChanged -= SettingsChanged;
	}
}