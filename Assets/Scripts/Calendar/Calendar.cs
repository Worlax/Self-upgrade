using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Calendar
{
	[JsonProperty] Dictionary<DateTime, int> Days = new Dictionary<DateTime, int>();

	public void ChangeValueBy(DateTime date, int value)
	{
		CreateDayIfItDoesntExists(date);
		Days[date] += value;
		
		if (Days[date] < 0)
		{
			Days[date] = 0;
		}
	}

	public void ChangeTodayValueBy(int value)
	{
		ChangeValueBy(DateTime.Today, value);
	}

	public int GetValue(DateTime date)
	{
		CreateDayIfItDoesntExists(date);
		return Days[date];
	}

	public int GetTodayValue()
	{
		return GetValue(DateTime.Today);
	}

	void CreateDayIfItDoesntExists(DateTime date)
	{
		if (!Days.ContainsKey(date))
		{
			Days[date] = 0;
		}
	}
}