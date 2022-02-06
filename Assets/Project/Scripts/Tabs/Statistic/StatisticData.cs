using System;
using UnityEngine;

public class StatisticData
{
	public DateTime DateStart { get; private set; }
	public DateTime DateEnd { get; private set; }

	public int DaysInDiapason { get; private set; }
	public int TotalProgressMinutes { get; private set; }
	public int PlannedMinutes { get; private set; }
	public int CompletedMinutes { get; private set; }
	public int FullyCompletedMinutes { get; private set; }
	public int FailedMinutes { get; private set; }

	public int CompletedPercent { get; private set; }
	public int FullyCompletedPercent { get; private set; }
	public int FailedPercent { get; private set; }

	public StatisticData(DateTime dateStart, DateTime dateEnd)
	{
		DateStart = dateStart;
		DateEnd = dateEnd;

		CalculateDaysInDiapason();
		CalculateMinutes();
		CalculatePercents();
	}

	void CalculateDaysInDiapason()
	{
		DateTime currentDate = new DateTime(DateStart.Year, DateStart.Month, DateStart.Day);
		DateTime lastDate = new DateTime(DateEnd.Year, DateEnd.Month, DateEnd.Day);
		int days = 0;

		while (currentDate <= lastDate)
		{
			currentDate = currentDate.AddDays(1);
			++days;
		}

		DaysInDiapason = days;
	}

	void CalculateMinutes()
	{
		// Seconds
		int totalProgressSeconds = 0;
		int plannedSeconds = 0;
		int completedSeconds = 0;
		int fullyCompletedSeconds = 0;

		foreach (Upgrade upgrade in UpgradeList.Instance.GetActive())
		{
			totalProgressSeconds += upgrade.Progress.GetValueInDiapason(DateStart, DateEnd);
			plannedSeconds += upgrade.Progress.MissionCalendar.GetGoalInDiapason(DateStart, DateEnd);
			completedSeconds += upgrade.Progress.MissionCalendar.GetProgressInDiapason(DateStart, DateEnd);
			fullyCompletedSeconds += upgrade.Progress.MissionCalendar.GetFullyCompletedProgressInDiapason(DateStart, DateEnd);
		}

		int failedSeconds = plannedSeconds - completedSeconds;

		// Rounding up
		failedSeconds += failedSeconds % 60 == 0 ? 0 : 60;

		// Minutes
		TotalProgressMinutes = totalProgressSeconds / 60;
		PlannedMinutes = plannedSeconds / 60;
		CompletedMinutes = completedSeconds / 60;
		FullyCompletedMinutes = fullyCompletedSeconds / 60;
		FailedMinutes = failedSeconds / 60;
	}

	void CalculatePercents()
	{
		// Unit intervals [0, 1]
		double UICompleted = PlannedMinutes == 0 ? 0 : (float)CompletedMinutes / (float)PlannedMinutes;
		double UIfullyCompleted = PlannedMinutes == 0 ? 0 : (float)FullyCompletedMinutes / (float)PlannedMinutes;
		double UIfailed = PlannedMinutes == 0 ? 0 : (float)FailedMinutes / (float)PlannedMinutes;

		// Rounding up
		UIfailed += UIfailed * 100 - (int)(UIfailed * 100) == 0 ? 0 : 0.01;

		// Percents
		CompletedPercent = UIToPercent(UICompleted);
		FullyCompletedPercent = UIToPercent(UIfullyCompleted);
		FailedPercent = UIToPercent(UIfailed);
	}

	int UIToPercent(double UnitInterval)
	{
		return (int)(UnitInterval * 100);
	}

	// Average output
	public int TotalProgressMinutesAverage(int days) => GetAverage(TotalProgressMinutes, days);
	public int PlannedMinutesAverage(int days) => GetAverage(PlannedMinutes, days);
	public int CompletedMinutesAverage(int days) => GetAverage(CompletedMinutes, days);
	public int FullyCompletedMinutesAverage(int days) => GetAverage(FullyCompletedMinutes, days);
	public int FailedMinutesAverage(int days) => GetAverage(FailedMinutes, days);

	int GetAverage(int value, int days)
	{
		double dayAverage = (double)value / (double)DaysInDiapason;
		return (int)(dayAverage * days);
	}
}