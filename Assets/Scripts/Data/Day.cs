using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Day
{
	[JsonProperty] public DateTime Date { get; private set; }
	[JsonProperty] public int Progress { get; private set; }

	[JsonProperty] List<MissionProgress> missionsProgress = new List<MissionProgress>();
	[JsonProperty] List<Mission> activeMissions;

	[JsonConstructor] Day() { }

	public Day(DateTime date, List<Mission> activeMissions)
	{
		Date = date;
		this.activeMissions = activeMissions;
	}

	public bool HaveScheduleCompletion()
	{
		return GetScheduleCompletion() != -1;
	}

	public double GetScheduleCompletion()
	{
		if (missionsProgress.Count() == 0) return -1;

		double total = 0;

		foreach (MissionProgress mp in missionsProgress)
		{
			total = mp.Progress / mp.Goal;
		}

		return total / missionsProgress.Count();
	}

	public void ChangeProgressBy(int by)
	{
		Progress += by;
		if (Progress < 0) Progress = 0;

		ChangeActiveMissionProgress(by);
	}

	public int GetCurrentMissionProgress()
	{
		MissionProgress activeWrittenMission = FindActiveMissionInWritten();

		if (activeWrittenMission != null)
		{
			return activeWrittenMission.Progress;
		}
		else
		{
			return 0;
		}
	}

	public bool DoesMissionHaveProgress(Mission mission)
	{
		foreach (MissionProgress mp in missionsProgress)
		{
			if (mp.TimeStart == mission.TimeStart && mp.TimeEnd == mission.TimeEnd)
			{
				return true;
			}
		}

		return false;
	}

	void ChangeActiveMissionProgress(int by)
	{
		// Looking for active mission in written missions (MissionsProgress list)
		MissionProgress activeMission = FindActiveMissionInWritten();

		// If there is none - looking for global active mission and writing it
		if (activeMission == null)
		{
			Mission notWrittenMission = FindActiveMissionInGlobal();

			if (notWrittenMission != null)
			{
				activeMission = WriteMissionProgress(notWrittenMission);
			}
		}

		// If we have our active mission at the end - changing it progress
		// or removing it if progress is <= 0
		if (activeMission != null)
		{
			activeMission.Progress += by;

			if (activeMission.Progress <= 0) missionsProgress.Remove(activeMission);
		}
	}

	MissionProgress FindActiveMissionInWritten()
	{
		TimeSpan nowTime = DateTime.Now.TimeOfDay;
		List<MissionProgress> alreadyStartedMissions = missionsProgress.Where(obj => obj.TimeStart < nowTime).ToList();
		MissionProgress haventFinishedMission = alreadyStartedMissions.Find(obj => obj.TimeEnd > nowTime);

		return haventFinishedMission;
	}

	Mission FindActiveMissionInGlobal()
	{
		TimeSpan nowTime = DateTime.Now.TimeOfDay;
		List<Mission> alreadyStartedMissions = activeMissions.Where(obj => obj.TimeStart < nowTime).ToList();
		Mission haventFinishedMission = alreadyStartedMissions.Find(obj => obj.TimeEnd > nowTime);

		return haventFinishedMission;
	}

	MissionProgress WriteMissionProgress(Mission mission)
	{
		MissionProgress missionProgress = new MissionProgress(mission);
		missionsProgress.Add(missionProgress);

		return missionProgress;
	}
}