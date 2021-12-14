using System;
using UnityEngine;
using Newtonsoft.Json;

public class Mission
{
	// Should always be 1 for checkers
	[JsonProperty] public int Goal { get; private set; }
	[JsonProperty] public DayOfWeek DayOfWeek { get; private set; }
	// Should always be 0:00 for checkers and multi checkers
	[JsonProperty] public TimeSpan TimeStart { get; private set; }
	// Should always be 23:59 for checkers and multi checkers
	[JsonIgnore] public TimeSpan TimeEnd { get => TimeStart + new TimeSpan(0, 0, Goal) + new TimeSpan(0, 0, BreakSeconds); }
	// Should always be 0 for checkers and multi checkers
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