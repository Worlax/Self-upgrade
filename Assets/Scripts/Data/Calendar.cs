using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Calendar
{
	[JsonProperty] List<Day> Days = new List<Day>();
	[JsonProperty] public List<Mission> ActiveMissions { get; private set; } = new List<Mission>();

	public void ChangeValueBy(DateTime date, int value)
	{
		CreateDayIfDosentExists(date);
		Day day = GetDay(date);
		day.ProgressValue += value;

		if (day.ProgressValue < 0)
		{
			day.ProgressValue = 0;
		}
	}

	public int GetValue(DateTime date)
	{
		if (GetDay(date) != null)
		{
			return GetDay(date).ProgressValue;
		}
		else
		{
			return 0;
		}		
	}

	public int GetValueInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		dateStart = RemoveTimeFromDate(dateStart);
		dateEnd = RemoveTimeFromDate(dateEnd);

		int value = 0;

		DateTime currentDate = dateStart;
		while (currentDate <= dateEnd)
		{
			value += GetValue(currentDate);
			currentDate = currentDate.AddDays(1);
		}

		return value;
	}

	public int GetTotalValue()
	{
		int totalValue = 0;
		foreach (Day day in Days)
		{
			totalValue += day.ProgressValue;
		}

		return totalValue;
	}

	public void AddMission(Mission mission)
	{
		ActiveMissions.Add(mission);
	}

	public List<Mission> GetAllMisssionsByDayOfTheWeek(DayOfWeek dayOfWeek)
	{
		return ActiveMissions.Where(obj => obj.DayOfWeek == dayOfWeek).ToList();
	}

	void CreateDayIfDosentExists(DateTime date)
	{
		date = RemoveTimeFromDate(date);

		if (GetDay(date) == null)
		{
			Days.Add(new Day(date));
		}
	}

	Day GetDay(DateTime date)
	{
		date = RemoveTimeFromDate(date);

		return Days.Find(obj => obj.Date == date);
	}

	DateTime RemoveTimeFromDate(DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day);
	}
}