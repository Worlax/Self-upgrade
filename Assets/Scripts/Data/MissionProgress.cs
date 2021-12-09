using System;
using UnityEngine;
using Newtonsoft.Json;

public class MissionProgress
{
	[JsonProperty] public TimeSpan TimeStart;
	[JsonProperty] public TimeSpan TimeEnd;
	[JsonProperty] public int Goal;
	[JsonProperty] public int Progress;

	public MissionProgress(Mission mission)
	{
		TimeStart = mission.TimeStart;
		TimeEnd = mission.TimeEnd;
		Goal = mission.Goal;
	}
}