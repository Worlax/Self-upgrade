using System;
using Newtonsoft.Json;
using UnityEngine;

public class ProgressDay : Day
{
	[JsonProperty] public int Progress { get; private set; }

	[JsonConstructor] ProgressDay() { }

	public ProgressDay(DateTime date)
	{
		Date = date;
	}

	public void ChangeProgressBy(int by)
	{
		Progress += by;
		Progress = Progress < 0 ? 0 : Progress;
	}
}