using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MissionCalendar : Calendar<MissionDay>
{
	[JsonProperty(IsReference = true)] public List<Mission> ScheduledMissions { get; private set; } = new List<Mission>();

	public static event Action OnMissionAddedToSchedule;
	public static event Action OnMissionRemovedFromSchedule;

	public void ChangeProgressBy(DateTime date, int by)
	{
		Update();
		// Missions will progress only if changes happened today
		if (date == DateTime.Today)
		{
			CreateDayIfDosentExists(date);
			MissionDay day = GetDay(date);
			day.ChangeProgressBy(by);
		}
	}

	public void AddMissionToSchedule(Mission mission)
	{
		Update();
		if (!IsMissionInSchedule(mission))
		{
			ScheduledMissions.Add(mission);
			OnMissionAddedToSchedule?.Invoke();
		}
	}

	public void RemoveMissionFromSchedule(Mission mission)
	{
		Update();
		if (IsMissionInSchedule(mission))
		{
			ScheduledMissions.Remove(mission);
			OnMissionRemovedFromSchedule?.Invoke();
		}
	}

	public MissionProgress GetActiveMission()
	{
		Update();
		return GetDay(DateTime.Today).GetActiveMission();
	}

	public int GetGoalInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		Update();
		if (dateEnd < dateStart) throw new Exception("dateEnd happens before dateStart.");

		DateTime currentDate = RemoveTimeFromDate(dateStart);
		DateTime lastDate = RemoveTimeFromDate(dateEnd);
		int goal = 0;

		while (currentDate <= lastDate)
		{
			MissionDay day = GetDay(currentDate);

			if (day != null)
			{
				goal += day.GetGoal();
			}

			currentDate = currentDate.AddDays(1);
		}

		return goal;
	}

	public int GetProgressInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		Update();
		if (dateEnd < dateStart) throw new Exception("dateEnd happens before dateStart.");

		DateTime currentDate = RemoveTimeFromDate(dateStart);
		DateTime lastDate = RemoveTimeFromDate(dateEnd);
		int progress = 0;

		while (currentDate <= lastDate)
		{
			MissionDay day = GetDay(currentDate);

			if (day != null)
			{
				progress += day.GetProgress();
			}

			currentDate = currentDate.AddDays(1);
		}

		return progress;
	}

	public int GetFullyCompletedProgressInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		Update();
		if (dateEnd < dateStart) throw new Exception("dateEnd happens before dateStart.");

		DateTime currentDate = RemoveTimeFromDate(dateStart);
		DateTime lastDate = RemoveTimeFromDate(dateEnd);
		int progress = 0;

		while (currentDate <= lastDate)
		{
			MissionDay day = GetDay(currentDate);

			if (day != null)
			{
				progress += day.GetFullyCompletedProgress();
			}

			currentDate = currentDate.AddDays(1);
		}

		return progress;
	}

	public double GetScheduleCompletionInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		Update();
		if (dateEnd < dateStart) throw new Exception("dateEnd happens before dateStart.");

		DateTime currentDate = RemoveTimeFromDate(dateStart);
		dateEnd = RemoveTimeFromDate(dateEnd);
		double scheduleCompletion = 0;
		int days = 0;

		while (currentDate <= dateEnd)
		{
			MissionDay day = GetDay(currentDate);

			if (day != null)
			{
				scheduleCompletion += day.GetScheduleCompletion();
				++days;
			}

			currentDate = currentDate.AddDays(1);
		}

		return days == 0 ? 0 : scheduleCompletion / days;
	}

	public bool IsDiapasonHaveMissionProgress(DateTime dateStart, DateTime dateEnd)
	{
		Update();
		if (dateEnd < dateStart) throw new Exception("dateEnd happens before dateStart.");

		dateStart = RemoveTimeFromDate(dateStart);
		dateEnd = RemoveTimeFromDate(dateEnd);
		DateTime currentDate = dateStart;

		while (currentDate <= dateEnd)
		{
			MissionDay day = GetDay(currentDate);

			if (day != null && day.HaveProgressMissions())
			{
				return true;
			}

			currentDate = currentDate.AddDays(1);
		}


		return false;
	}

	void Update()
	{
		if (days.Count == 0)
		{
			CreateDayIfDosentExists(DateTime.Today);
		}
		else
		{
			FillMissedDays();
		}
	}

	void FillMissedDays()
	{
		if (DateTime.Today <= days.Last().Date) return;

		while (days.Last().Date < DateTime.Today)
		{
			DateTime lastDate = days.Last().Date;
			MissionDay newDay = new MissionDay(ScheduledMissions, lastDate.AddDays(1));
			days.Add(newDay);
		}
	}

	void CreateDayIfDosentExists(DateTime date)
	{
		date = RemoveTimeFromDate(date);
		if (GetDay(date) == null)
		{
			MissionDay day = new MissionDay(ScheduledMissions, date);
			days.Add(day);
		}
	}

	bool IsMissionInSchedule(Mission mission) => ScheduledMissions.Any(obj => obj.ID == mission.ID);
}