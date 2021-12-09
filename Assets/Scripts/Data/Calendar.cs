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
		day.ChangeProgressBy(value);
	}

	public int GetValue(DateTime date)
	{
		if (GetDay(date) != null)
		{
			return GetDay(date).Progress;
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
			totalValue += day.Progress;
		}

		return totalValue;
	}

	public List<Mission> GetTodayMissions()
	{
		return ActiveMissions.Where(obj => obj.DayOfWeek == DateTime.Today.DayOfWeek).ToList();
	}

	public Mission GetNowMission()
	{
		TimeSpan nowTime = DateTime.Now.TimeOfDay;
		List<Mission> alreadyStartedMissions = GetTodayMissions().Where(obj => obj.TimeStart < nowTime).ToList();
		Mission haventFinishedMission = alreadyStartedMissions.Find(obj => obj.TimeEnd > nowTime);

		return haventFinishedMission;
	}

	//public int GetCurrentMissionProgress()
	//{
	//	//
	//}

	public void AddMission(Mission mission)
	{
		ActiveMissions.Add(mission);
		UpdateTodayGoal();
	}

	public void DeleteMission(Mission mission)
	{
		ActiveMissions.Remove(mission);
		UpdateTodayGoal();
	}

	void UpdateTodayGoal()
	{
		//Day today = GetDay(DateTime.Today);

		//if (today != null)
		//{
		//	today.UpdateGoal(ActiveMissions);
		//}
	}

	void CreateDayIfDosentExists(DateTime date)
	{
		date = RemoveTimeFromDate(date);

		if (GetDay(date) == null)
		{
			Days.Add(new Day(date, ActiveMissions));
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