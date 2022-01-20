using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MissionDay : Day
{
	[JsonProperty] IEnumerable<Mission> allScheduledMissions = new List<Mission>();
	[JsonProperty] List<MissionProgress> missionsProgress = new List<MissionProgress>();

	[JsonProperty] bool DayCompleted;

	[JsonConstructor] MissionDay() { }

	public MissionDay(IEnumerable<Mission> scheduledMissions, DateTime date)
	{
		this.allScheduledMissions = scheduledMissions;
		Date = date;

		Update();
	}

	public void ChangeProgressBy(int by)
	{
		Update();
		if (DayCompleted) return;

		MissionProgress activeMission = GetActiveMission();

		if (activeMission != null)
		{
			activeMission.ChangeProgressBy(by);
		}
	}

	public MissionProgress GetActiveMission()
	{
		Update();
		if (DayCompleted) return null;

		return missionsProgress.Find(obj => IsMissionStarted(obj) && !IsMissionEnded(obj));
	}

	public double GetScheduleCompletion()
	{
		Update();

		double schedule = 0;
		int missions = missionsProgress.Count;

		foreach (MissionProgress missionProgress in missionsProgress)
		{
			schedule = (double)missionProgress.Progress / (double)missionProgress.Mission.Goal;
		}

		return missions == 0 ? 0 : schedule / missions;
	}

	public bool HaveProgressMissions()
	{
		Update();

		return missionsProgress.Count > 0;
	}

	void Update()
	{
		if (!DayCompleted)
		{
			AddNewMissionsToProgress();

			if (IsDayFromThePast())
			{
				DayCompleted = true;
			}
		}
	}

	void AddNewMissionsToProgress()
	{
		IEnumerable<Mission> todayScheduledMissions = allScheduledMissions.Where(obj => obj.DayOfWeek == Date.DayOfWeek);
		foreach (Mission scheduledMission in todayScheduledMissions)
		{
			if (!IsMissionInProgress(scheduledMission) && IsMissionStarted(scheduledMission))
			{
				missionsProgress.Add(new MissionProgress(scheduledMission));
			}
		}
	}

	bool IsMissionForToday(Mission mission) => mission.DayOfWeek == Date.DayOfWeek;
	bool IsMissionInProgress(Mission mission) => missionsProgress.Any(obj => obj.Mission.ID == mission.ID);
	bool IsMissionEnded(Mission mission) => (IsMissionForToday(mission) && mission.TimeEnd < DateTime.Now.TimeOfDay) || IsDayFromThePast();
	bool IsMissionEnded(MissionProgress mission) => IsMissionEnded(mission.Mission);
	bool IsMissionStarted(Mission mission) => (IsMissionForToday(mission) && mission.TimeStart < DateTime.Now.TimeOfDay) || IsDayFromThePast();
	bool IsMissionStarted(MissionProgress mission) => IsMissionStarted(mission.Mission);
	bool IsDayFromThePast() => Date < DateTime.Today;
}