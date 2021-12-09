using System;
using UnityEngine;

public class TimeEvents : Singleton<TimeEvents>
{
	DateTime lastEvent;

	public static event Action OnOneMinutePassed;

	
	// Unity
	private void Update()
	{
		
	}
}