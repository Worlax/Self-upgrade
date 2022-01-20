using UnityEngine;

public static class TimeConverter
{
	public static string TimeString(int seconds, bool showSecondsIfAny = true,
		bool alwaysShowHours = false, bool alwaysShowMinutes = false, bool alwaysShowSeconds = false,
		string hoursAbbr = "h", string minutesAbbr = "m", string secondsAbbr = "s")
	{
		string time = "";
		int h = Hours(seconds);
		int m = Minutes(seconds);
		int s = Seconds(seconds);
		bool separationIsNeeded = false;

		if (h > 0 || alwaysShowHours)
		{
			if (alwaysShowHours && h < 10)
			{
				time += "0";
			}

			time += h + hoursAbbr;
			separationIsNeeded = true;
		}

		if (m > 0 || alwaysShowMinutes)
		{
			if (separationIsNeeded)
			{
				time += " ";
			}

			if (alwaysShowMinutes && m < 10)
			{
				time += "0";
			}

			time += m + minutesAbbr;
			separationIsNeeded = true;
		}

		if ((s > 0 && showSecondsIfAny) || alwaysShowSeconds)
		{
			if (separationIsNeeded)
			{
				time += " ";
			}

			if (alwaysShowSeconds && s < 10)
			{
				time += "0";
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