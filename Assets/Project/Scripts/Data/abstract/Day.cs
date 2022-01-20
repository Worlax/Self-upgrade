using System;
using Newtonsoft.Json;
using UnityEngine;

public abstract class Day : IComparable<Day>
{
	[JsonProperty] public DateTime Date { get; protected set; }

	public int CompareTo(Day other)
	{
		if (other == null) return 1;

		return Date.CompareTo(other.Date);
	}
}