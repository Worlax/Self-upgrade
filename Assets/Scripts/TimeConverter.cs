using UnityEngine;
using Newtonsoft.Json;

public static class TimeConverter
{
	public static int Seconds(int seconds)
	{
		return seconds - (Minutes(seconds) * 60 + Hours(seconds) * 3600);
	}
	
	public static int Minutes(int seconds)
	{ 
		return seconds / 60 - Hours(seconds) * 60; 
	}

	public static int Hours(int seconds)
	{ 
		return seconds / 60 / 60;
	}
}