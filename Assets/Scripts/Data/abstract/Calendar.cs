using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public abstract class Calendar<T> where T : Day
{
	[JsonProperty] protected List<T> days = new List<T>();

	protected virtual T GetDay(DateTime date)
	{
		date = RemoveTimeFromDate(date);
		return days.Find(obj => obj.Date == date);
	}

	protected virtual DateTime RemoveTimeFromDate(DateTime date)
	{
		return new DateTime(date.Year, date.Month, date.Day);
	}
}