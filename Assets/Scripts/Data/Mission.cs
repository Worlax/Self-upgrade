using System;
using Newtonsoft.Json;
using UnityEngine;

public class Mission
{
	[JsonProperty] static int lastID;
	[JsonProperty] public int ID { get; private set; }
	[JsonProperty] public int Goal { get; private set; }
	[JsonProperty] public DayOfWeek DayOfWeek { get; private set; }
	[JsonProperty] public TimeSpan TimeStart { get; private set; }
	[JsonIgnore] public TimeSpan TimeEnd { get => TimeStart + new TimeSpan(0, 0, Goal) + new TimeSpan(0, 0, BreakSeconds); }
	[JsonProperty] public int WeeksBeforeRepeat { get; private set; }
	[JsonProperty] public int BreakSeconds { get; private set; }

	[JsonConstructor] Mission() { }

	public Mission(int goal, DayOfWeek dayOfWeek, TimeSpan timeStart, int weeksBeforeRepeat, int breakSeconds = 0)
	{
		Goal = goal;
		DayOfWeek = dayOfWeek;
		TimeStart = timeStart;
		WeeksBeforeRepeat = weeksBeforeRepeat;
		BreakSeconds = breakSeconds;

		ID = ++lastID;
	}

	public Mission Clone()
	{
		return new Mission()
		{
			Goal = Goal,
			DayOfWeek = DayOfWeek,
			TimeStart = TimeStart,
			WeeksBeforeRepeat = WeeksBeforeRepeat,
			BreakSeconds = BreakSeconds,

			ID = ID
		};
	}
}