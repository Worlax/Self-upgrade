using System;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleItem : MonoBehaviour, IComparable<ScheduleItem>
{
#pragma warning disable 0649

	[SerializeField] Text upgradeName;
	[SerializeField] Text upgradeValue;
	[SerializeField] Text upgradeBreak;

	[SerializeField] CanvasGroup grayOut;
	[SerializeField] Toggle selfToggle;

#pragma warning restore 0649

	public Upgrade Upgrade { get; private set; }
	public Mission Mission { get; private set; }

	public event Action<ScheduleItem, bool> OnToggleChanged;

	public void Init(Upgrade upgrade, Mission mission, ToggleGroup toggleGroup)
	{
		Upgrade = upgrade;
		Mission = mission;
		selfToggle.group = toggleGroup;

		upgradeName.text = upgrade.Name;

		if (upgradeValue)
		{
			string value = "";

			switch (upgrade.Type)
			{
				case UpgradeType.Timer:
					
					value = mission.TimeStart.ToString("hh") + ":" + mission.TimeStart.ToString("mm");
					value += " - ";
					value += mission.TimeEnd.ToString("hh") + ":" + mission.TimeEnd.ToString("mm");
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

	public void GrayOut(bool value)
	{
		selfToggle.interactable = value;

		if (value)
		{
			grayOut.alpha = 0.35f;
		}
		else
		{
			grayOut.alpha = 0f;
		}
	}

	public int CompareTo(ScheduleItem other)
	{
		if (other == null) return 1;

		return Mission.TimeStart.CompareTo(other.Mission.TimeStart);
	}

	// Events
	void ToggleValueChanged(bool value)
	{
		OnToggleChanged?.Invoke(this, value);
	}

	// Unity
	private void OnEnable()
	{
		selfToggle.onValueChanged.AddListener(ToggleValueChanged);
	}

	private void OnDisable()
	{
		selfToggle.onValueChanged.RemoveListener(ToggleValueChanged);
	}
}