using System;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleItem : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text upgradeName;
	[SerializeField] Text upgradeValue;
	[SerializeField] Text upgradeBreak;

#pragma warning restore 0649

	public Upgrade Upgrade { get; private set; }

	public void Init(Upgrade upgrade, Mission mission)
	{
		upgradeName.text = upgrade.Name;

		if (upgradeValue)
		{
			string value = "";

			switch (upgrade.Type)
			{
				case UpgradeType.Timer:
					value = mission.TimeStart.Hours.ToString() + ":" + mission.TimeStart.Minutes.ToString();
					value += " - ";
					TimeSpan endOfMissionTime = mission.TimeStart.Add(new System.TimeSpan(0, 0, mission.Goal));
					value += endOfMissionTime.Hours.ToString() + ":" + endOfMissionTime.Minutes.ToString();
					break;

				case UpgradeType.MultiChecker:
					value = mission.Goal.ToString();
					break;
			}

			upgradeValue.text = value;
		}

		if (upgradeBreak)
		{
			upgradeBreak.text = TimeConverter.TimeString(mission.BreakSeconds);
		}
	}
}