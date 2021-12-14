using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Calendar
{
	[JsonProperty] List<Day> Days = new List<Day>();
	[JsonProperty] List<FailedGoals> failedGoals = new List<FailedGoals>();
	[JsonProperty] public List<Mission> ActiveMissions { get; private set; } = new List<Mission>();

	// Change, get value
	public void ChangeValueBy(DateTime date, int value)
	{
		CreateDayIfDosentExists(date);
		Day day = GetDay(date);
		day.ChangeProgressBy(value);
	}

	public int GetValue(DateTime date)
	{
		if (GetDay(date) != null)
		{
			return GetDay(date).Progress;
		}
		else
		{
			return 0;
		}		
	}

	public int GetValueInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		if (dateEnd < dateStart) throw new Exception("dateEnd going before dateStart.");

		dateStart = RemoveTimeFromDate(dateStart);
		dateEnd = RemoveTimeFromDate(dateEnd);

		int value = 0;

		DateTime currentDate = dateStart;
		while (currentDate <= dateEnd)
		{
			value += GetValue(currentDate);
			currentDate = currentDate.AddDays(1);
		}

		return value;
	}

	public int GetTotalValue()
	{
		int totalValue = 0;
		foreach (Day day in Days)
		{
			totalValue += day.Progress;
		}

		return totalValue;
	}

	// Schedule
	public double GetScheduleCompletion(DateTime date)
	{
		Day day = GetDay(date);

		if (day != null)
		{
			return day.GetScheduleCompletion();
		}
		else
		{
			return -1;
		}
	}

	public double GetScheduleCompletionInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		if (dateEnd < dateStart) throw new Exception("dateEnd going before dateStart.");

		dateStart = RemoveTimeFromDate(dateStart);
		dateEnd = RemoveTimeFromDate(dateEnd);

		double totalSchedule = 0;
		int daysHaveValue = 0;

		DateTime currentDate = dateStart;
		while (currentDate <= dateEnd)
		{
			Day day = GetDay(currentDate);

			if (day != null && day.HaveScheduleCompletion())
			{
				totalSchedule = day.GetScheduleCompletion();
				++daysHaveValue;
			}

			currentDate = currentDate.AddDays(1);
		}

		return daysHaveValue == 0 ? -1 : totalSchedule / daysHaveValue;
	}

	// Missions
	public List<Mission> GetTodayMissions()
	{
		return ActiveMissions.Where(obj => obj.DayOfWeek == DateTime.Today.DayOfWeek).ToList();
	}

	public Mission GetNowMission()
	{
		TimeSpan nowTime = DateTime.Now.TimeOfDay;
		List<Mission> todayMissions = ActiveMissions.Where(obj => obj.DayOfWeek == DateTime.Today.DayOfWeek).ToList();
		List<Mission> alreadyStartedMissions = GetTodayMissions().Where(obj => obj.TimeStart < nowTime).ToList();
		Mission haventFinishedMission = alreadyStartedMissions.Find(obj => obj.TimeEnd > nowTime);

		return haventFinishedMission;
	}

	public int GetCurrentMissionProgress()
	{
		Day day = GetDay(DateTime.Today);

		if (day != null)
		{
			return day.GetCurrentMissionProgress();
		}
		else
		{
			return 0;
		}
	}

	public void AddMission(Mission mission)
	{
		ActiveMissions.Add(mission);
	}

	public void DeleteMission(Mission mission)
	{
		ActiveMissions.Remove(mission);
	}

	// Failed goals
	void DeleteGoalsFromTheFuture()
	{
		if (failedGoals.Count > 0)
		{
			failedGoals.Sort();

			if (failedGoals.Last().Date > DateTime.Today)
			{
				Debug.Log("Date have been changed, failed goals adjasted to the changes.");

				while (failedGoals.Last().Date > DateTime.Today || failedGoals.Count() == 0)
				{
					failedGoals.Remove(failedGoals.Last());
				}
			}
		}
	}

	void UpdateLastFailedGoals()
	{
		if (failedGoals.Count > 0)
		{
			FailedGoals lastFailedGoal = failedGoals.Last();
			failedGoals.Remove(lastFailedGoal);

			// Updating data in dependence of the current day
			if (!lastFailedGoal.FullDay)
			{
				if (lastFailedGoal.Date == DateTime.Today)
				{
					CreateFailedGoals(DateTime.Now, false);
				}
				else
				{
					CreateFailedGoals(lastFailedGoal.Date, true);
				}
			}
		}
	}

	void CreateFailedGoals(DateTime date, bool fullDay)
	{
		if (fullDay)
		{
			date = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
		}

		List<Mission> currentDateMissions = ActiveMissions.Where(obj => obj.DayOfWeek == date.DayOfWeek).ToList();
		List<Mission> endedMissions = currentDateMissions.Where(obj => obj.TimeEnd < date.TimeOfDay).ToList();
		List<Mission> missionsWithoutProgress = new List<Mission>();

		foreach (Mission mission in endedMissions)
		{
			Day day = GetDay(date);

			if (day == null || !day.DoesMissionHaveProgress(mission))
			{
				missionsWithoutProgress.Add(mission);
			}
		}

		failedGoals.Add(new FailedGoals(date, missionsWithoutProgress, fullDay));
	}

	void AddDaysBeforeToday()
	{
		if (failedGoals.Count > 0)
		{
			DateTime yesterday = DateTime.Today.AddDays(-1);

			while (failedGoals.Last().Date < yesterday)
			{
				DateTime dayAfterLast = failedGoals.Last().Date.AddDays(1);
				CreateFailedGoals(dayAfterLast, true);
			}
		}
	}

	void AddToday()
	{
		if (failedGoals.Count > 0 && failedGoals.Last().Date < DateTime.Today)
		{
			CreateFailedGoals(DateTime.Now, false);
		}
	}

	void UpdateFailedGoals()
	{
		if (failedGoals.Count == 0)
		{
			CreateFailedGoals(DateTime.Now, false);
		}
		else
		{
			DeleteGoalsFromTheFuture();
			UpdateLastFailedGoals();
			AddDaysBeforeToday();
			AddToday();
		}
	}

	public FailedGoals GetFailedGoals(DateTime date)
	{
		UpdateFailedGoals();
		return failedGoals.Find(obj => obj.Date == date);
	}

	// Dys
	void CreateDayIfDosentExists(DateTime date)
	{
		date = RemoveTimeFromDate(date);

		if (GetDay(date) == null)
		{
			Days.Add(new Day(date, ActiveMissions));
		}
	}

	Day GetDay(DateTime date)
	{
		date = RemoveTimeFromDate(date);

		return Days.Find(obj => obj.Date == date);
	}

	DateTime RemoveTimeFromDate(DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day);
	}
}