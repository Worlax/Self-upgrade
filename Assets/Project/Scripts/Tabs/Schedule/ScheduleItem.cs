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
	[SerializeField] Button selfButton;

#pragma warning restore 0649

	public Upgrade Upgrade { get; private set; }
	public Mission Mission { get; private set; }

	public event Action<ScheduleItem> OnClicked;

	public void Init(Upgrade upgrade, Mission mission)
	{
		Upgrade = upgrade;
		Mission = mission;

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
		selfButton.interactable = value;

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
	void SelfButtonClicked()
	{
		OnClicked?.Invoke(this);
	}

	// Unity
	private void OnEnable()
	{
		selfButton.onClick.AddListener(SelfButtonClicked);
	}

	private void OnDisable()
	{
		selfButton.onClick.RemoveListener(SelfButtonClicked);
	}
}