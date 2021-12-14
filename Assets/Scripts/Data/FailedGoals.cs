using System;
using System.Collections.Generic;
using UnityEngine;

public class FailedGoals : IComparable<FailedGoals>
{
	public DateTime Date;
	public bool FullDay;
	public List<Mission> FailedMissions;

	public FailedGoals(DateTime date, List<Mission> missions, bool fullDay)
	{
		Date = new DateTime(date.Year, date.Month, date.Day);
		FailedMissions = missions;
		FullDay = fullDay;
	}

	public int CompareTo(FailedGoals other)
	{
		if (other == null) return 1;

		return Date.CompareTo(other.Date);
	}
}