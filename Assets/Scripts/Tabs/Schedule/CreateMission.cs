using System;
using UnityEngine;
using UnityEngine.UI;

public class CreateMission : MonoBehaviour
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

	// Events
	void Create()
	{
		Upgrade upgrade = Upgrade.AllUpgrades.Find(obj => obj.Name == upgradeName.text);
		Mission mission = new Mission(Int32.Parse(goal.text), (DayOfWeek)Int32.Parse(dayOfTheWeek.text),
			new TimeSpan(Int32.Parse(TimeStartHours.text), Int32.Parse(TimeStartMinutes.text), 0), 1, Int32.Parse(BreakSeconds.text));

		upgrade.Calendar.AddMission(mission);
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