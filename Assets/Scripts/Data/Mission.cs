using System;
using UnityEngine;
using Newtonsoft.Json;

public class Mission
{
	[JsonProperty] public int Goal { get; private set; }
	[JsonProperty] public DayOfWeek DayOfWeek { get; private set; }
	[JsonProperty] public TimeSpan TimeStart { get; private set; }
	[JsonProperty] public int BreakSeconds { get; private set; }
	[JsonProperty] public int WeeksBeforeRepeat { get; private set; }

	public Mission(int goal, DayOfWeek dayOfWeek, TimeSpan timeStart, int weeksBeforeRepeat, int breakSeconds = 0)
	{
		Goal = goal;
		DayOfWeek = dayOfWeek;
		TimeStart = timeStart;
		BreakSeconds = breakSeconds;
		WeeksBeforeRepeat = weeksBeforeRepeat;
	}
}