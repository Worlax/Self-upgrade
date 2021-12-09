using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text upgradeTimeText;
	[SerializeField] Text missionTimeText;
	[SerializeField] Text breakTimeText;

	[SerializeField] Text upgradeTimeLeft;
	[SerializeField] Text missionTimeLeft;
	[SerializeField] Text breakTimeLeft;

#pragma warning restore 0649

	void sa()
	{
		Upgrade activeUpgrade = UpgradesList.Instance.GetActive()[0];
		Mission currentMission = activeUpgrade.Calendar.GetNowMission();

		if (currentMission != null)
		{
			ShowVisual(true);

			//TimeSpan missionTimeL = currentMission.TimeEnd - DateTime.Now.TimeOfDay;
			////общее время на перерыв - ((текущее время - начало тренировки) - время что мы прозанимались)
			//upgradeTimeLeft.text = 
			//missionTimeLeft.text = TimeConverter.TimeString((int)missionTimeL.TotalSeconds);

			//currentMission.BreakSeconds - (DateTime.Now.Date - currentMission.TimeStart) -
			//breakTimeLeft.text =

			//

			//upgradeTimeLeft.text = currentMission.Goal - activeUpgrade.Calendar.
		}
		else
		{
			ShowVisual(false);
		}	
	}

	void ShowVisual(bool value)
	{
		upgradeTimeText.gameObject.SetActive(value);
		missionTimeText.gameObject.SetActive(value);
		breakTimeText.gameObject.SetActive(value);

		upgradeTimeLeft.gameObject.SetActive(value);
		missionTimeLeft.gameObject.SetActive(value);
		breakTimeLeft.gameObject.SetActive(value);
	}

	void UpdateBreakTime()
	{

	}

	// Unity
	private void OnEnable()
	{
		InvokeRepeating("UpdateBreakTime", 0, 1);
	}

	private void OnDisable()
	{
		CancelInvoke("UpdateBreakTime");
	}
}