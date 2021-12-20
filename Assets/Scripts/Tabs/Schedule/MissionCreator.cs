using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MissionCreator : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] InputField upgradeName;
	[SerializeField] InputField goal;
	[SerializeField] InputField dayOfTheWeek;
	[SerializeField] InputField TimeStartHours;
	[SerializeField] InputField TimeStartMinutes;
	[SerializeField] InputField BreakSeconds;

	[SerializeField] Button createMission;

#pragma warning restore 0649

	//public event Action OnMissionCreated;
	//public event Action OnMissionDeleted;

	// Events
	void Create()
	{
		Upgrade upgrade = Upgrade.AllUpgrades.ToList().Find(obj => obj.Name == upgradeName.text);
		Mission mission = new Mission(Int32.Parse(goal.text), (DayOfWeek)Int32.Parse(dayOfTheWeek.text),
			new TimeSpan(Int32.Parse(TimeStartHours.text), Int32.Parse(TimeStartMinutes.text), 0), 1, Int32.Parse(BreakSeconds.text));

		upgrade.Progress.MissionCalendar.AddMissionToSchedule(mission);
	}

	// Unity
	private void OnEnable()
	{
		createMission.onClick.AddListener(Create);
	}

	private void OnDisable()
	{
		createMission.onClick.RemoveListener(Create);
	}
}