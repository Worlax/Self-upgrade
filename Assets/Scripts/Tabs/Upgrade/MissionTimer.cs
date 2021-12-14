using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimer : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform plannedUpgradeLabel;
	[SerializeField] RectTransform missionEndsInLabel;
	[SerializeField] RectTransform breakTimeLabel;

	[SerializeField] Text plannedUpgradeLeft;
	[SerializeField] Text missionEndsIn;
	[SerializeField] Text breakTimeLeft;

#pragma warning restore 0649

	void UpdateDisplay()
	{
		Upgrade activeUpgrade = UpgradesList.Instance.GetActive()[0];
		Mission currentMission = activeUpgrade.Calendar.GetNowMission();

		if (currentMission != null)
		{
			ShowVisual(true);

			// Info math
			int goal = currentMission.Goal;
			int progress = activeUpgrade.Calendar.GetCurrentMissionProgress();

			TimeSpan timeEnd = currentMission.TimeEnd;
			TimeSpan timeNow = DateTime.Now.TimeOfDay;

			TimeSpan upgradeTimeLeft = new TimeSpan(0, 0, goal - progress);
			TimeSpan timeLeft = timeEnd - timeNow;
			TimeSpan breakLeft = timeLeft - upgradeTimeLeft;
			breakLeft = breakLeft < TimeSpan.Zero ? TimeSpan.Zero : breakLeft;

			// Display
			plannedUpgradeLeft.text = TimeConverter.TimeString((int)upgradeTimeLeft.TotalSeconds, true, false, true, true);
			missionEndsIn.text = TimeConverter.TimeString((int)timeLeft.TotalSeconds, true, false, true, true);
			breakTimeLeft.text = TimeConverter.TimeString((int)breakLeft.TotalSeconds, true, false, true, true);

			ShowMssionEndVisual(breakLeft == TimeSpan.Zero);
		}
		else
		{
			ShowVisual(false);
		}	
	}

	void ShowMssionEndVisual(bool value)
	{
		missionEndsInLabel.gameObject.SetActive(value);
		missionEndsIn.gameObject.SetActive(value);
	}

	void ShowVisual(bool value)
	{
		plannedUpgradeLabel.gameObject.SetActive(value);
		missionEndsInLabel.gameObject.SetActive(value);
		breakTimeLabel.gameObject.SetActive(value);

		plannedUpgradeLeft.gameObject.SetActive(value);
		missionEndsIn.gameObject.SetActive(value);
		breakTimeLeft.gameObject.SetActive(value);
	}

	//
	void StopWatchStart()
	{
		CancelInvoke("UpdateDisplay");

		Stopwatch.OnStopwatchSecondPassed += StopwatchSecondPassed;
	}

	void StopWatchStop()
	{
		InvokeRepeating("UpdateDisplay", 1, 1);

		Stopwatch.OnStopwatchSecondPassed -= StopwatchSecondPassed;
	}

	void StopwatchSecondPassed()
	{
		UpdateDisplay();
	}

	void ActiveUpgradesChanged(List<Upgrade> upgrade)
	{
		UpdateDisplay();
	}

	// Unity
	private void OnEnable()
	{
		InvokeRepeating("UpdateDisplay", 0, 1);

		Stopwatch.OnStopwatchSrart += StopWatchStart;
		Stopwatch.OnStopwatchStop += StopWatchStop;
		UpgradesList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}

	private void OnDisable()
	{
		CancelInvoke("UpdateDisplay");

		Stopwatch.OnStopwatchSrart -= StopWatchStart;
		Stopwatch.OnStopwatchStop -= StopWatchStop;
		UpgradesList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;
	}
}