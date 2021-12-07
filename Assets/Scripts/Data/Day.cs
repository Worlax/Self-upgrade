using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Day
{
	[JsonProperty] public DateTime Date;
	[JsonProperty] public int ProgressValue;
	[JsonProperty] public List<CompletedMission> CompletedMissions = new List<CompletedMission>();

	public Day(DateTime date)
	{
		Date = date;
	}
}