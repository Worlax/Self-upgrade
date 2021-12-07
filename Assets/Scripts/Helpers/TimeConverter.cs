using UnityEngine;

public static class TimeConverter
{
	public static string TimeString(int seconds, bool showSeconds = true,
		string hoursAbbr = "h", string minutesAbbr = "m", string secondsAbbr = "s")
	{
		string time = "";
		int h = Hours(seconds);
		int m = Minutes(seconds);
		int s = Seconds(seconds);
		bool separationIsNeeded = false;

		if (h > 0)
		{
			time += h + hoursAbbr;
			separationIsNeeded = true;
		}

		if (m > 0)
		{
			if (separationIsNeeded)
			{
				time += " ";
			}

			time += m + minutesAbbr;
			separationIsNeeded = true;
		}

		if (s > 0 && showSeconds)
		{
			if (separationIsNeeded)
			{
				time += " ";
			}

			time += s + secondsAbbr;
		}

		return time;
	}

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