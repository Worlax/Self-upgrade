using System;
using UnityEngine;
using Newtonsoft.Json;

public class CompletedMission : MonoBehaviour
{
	[JsonProperty] public int Goal;
	[JsonProperty] public int GoalCompleted;
	[JsonProperty] public DateTime Date;

	public CompletedMission(int goal, int goalCompleted, DateTime date)
	{
		Goal = goal;
		GoalCompleted = goalCompleted;
		Date = date;
	}
}