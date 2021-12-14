using System;
using UnityEngine;

public class TimeEvents : Singleton<TimeEvents>
{
	DateTime lastEvent;

	public static event Action OnOneMinutePassed;

	void ResetLastEventTime()
	{
		DateTime now = DateTime.Now;
		lastEvent = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
	}

	// Unity
	private void Update()
	{
		if (lastEvent.AddMinutes(1) < DateTime.Now)
		{
			ResetLastEventTime();
			OnOneMinutePassed?.Invoke();
		}
	}

	private void OnEnable()
	{
		ResetLastEventTime();
	}
}