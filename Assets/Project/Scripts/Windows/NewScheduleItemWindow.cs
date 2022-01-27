using System;
using UnityEngine;
using UnityEngine.UI;

public class NewScheduleItemWindow : Window
{
#pragma warning disable 0649

	[SerializeField] UpgradeDropdown upgradeDropdown;
	[SerializeField] DayOfWeekDropdown dayOfWeekDropdown;
	[SerializeField] RepeatEveryDropdown repeatEveryDropdown;

	[SerializeField] InputField startHours;
	[SerializeField] InputField startMinutes;
	[SerializeField] InputField endHours;
	[SerializeField] InputField endMinutes;
	[SerializeField] InputField breakHours;
	[SerializeField] InputField breakMinutes;

#pragma warning restore 0649

	//public event Action OnMissionCreated;
	//public event Action OnMissionDeleted;

	protected override bool Execute()
	{
		Upgrade upgrade = upgradeDropdown.GetActive();
		DayOfWeek dayOfWeek = dayOfWeekDropdown.GetActive();

		TimeSpan startTime = GetTime(startHours.text, startMinutes.text);
		TimeSpan endTime = GetTime(endHours.text, endMinutes.text);
		TimeSpan breakTime = GetTime(breakHours.text, breakMinutes.text);

		int goalSeconds = (int)(endTime - startTime).TotalSeconds;
		int breakSeconds = (int)breakTime.TotalSeconds;

		Mission mission = new Mission(goalSeconds, dayOfWeek, startTime, 1, breakSeconds);
		upgrade.Progress.MissionCalendar.AddMissionToSchedule(mission);

		return true;
	}

	TimeSpan GetTime(string hours, string minutes)
	{
		return new TimeSpan(Int32.Parse(hours), Int32.Parse(minutes), 0);
	}
}