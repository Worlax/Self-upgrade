using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Calendar
{
	[JsonProperty] Dictionary<DateTime, int> Days = new Dictionary<DateTime, int>();

	public void ChangeValueBy(DateTime date, int value)
	{
		date = RemoveTime(date);

		CreateDayIfDosentExists(date);

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
		date = RemoveTime(date);

		if (Days.ContainsKey(date))
		{
			return Days[date];
		}
		else
		{
			return 0;
		}		
	}

	public int GetTodayValue()
	{
		return GetValue(DateTime.Today);
	}

	public int GetValueInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		dateStart = RemoveTime(dateStart);
		dateEnd = RemoveTime(dateEnd);

		int value = 0;

		DateTime currentDate = dateStart;
		while (currentDate <= dateEnd)
		{
			value += GetValue(currentDate);
			currentDate = currentDate.AddDays(1);
		}

		return value;
	}

	void CreateDayIfDosentExists(DateTime date)
	{
		date = RemoveTime(date);

		if (!Days.ContainsKey(date))
		{
			Days[date] = 0;
		}
	}

	DateTime RemoveTime(DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day);
	}
}