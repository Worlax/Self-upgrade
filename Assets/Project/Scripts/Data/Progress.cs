using System;
using Newtonsoft.Json;
using UnityEngine;

public class Progress : Calendar<ProgressDay>
{
	[JsonProperty] public MissionCalendar MissionCalendar { get; private set; } = new MissionCalendar();

	public int GetValue(DateTime date)
	{
		ProgressDay ourDay = GetDay(date);
		return ourDay == null ? 0 : ourDay.Progress;
	}

	public int GetValueInDiapason(DateTime dateStart, DateTime dateEnd)
	{
		if (dateEnd < dateStart) throw new Exception("dateEnd happens before dateStart.");

		dateStart = RemoveTimeFromDate(dateStart);
		dateEnd = RemoveTimeFromDate(dateEnd);
		DateTime currentDate = dateStart;

		int value = 0;

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
		days.ForEach(obj => totalValue += obj.Progress);
		return totalValue;
	}

	public void ChangeProgressBy(DateTime date, int value)
	{
		CreateDayIfDosentExists(date);
		ProgressDay day = GetDay(date);
		day.ChangeProgressBy(value);

		MissionCalendar.ChangeProgressBy(date, value);
	}

	public DateTime GetFirstRecordedDay()
	{
		return days.Count > 0 ? days[0].Date : DateTime.Today;
	}

	void CreateDayIfDosentExists(DateTime date)
	{
		date = RemoveTimeFromDate(date);
		if (GetDay(date) == null)
		{
			ProgressDay day = new ProgressDay(date);
			days.Add(day);
		}
	}
}