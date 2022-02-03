using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveMission : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform content;

	[SerializeField] Text untilFullUpgrade;
	[SerializeField] Text missionEndsIn;
	[SerializeField] Text breakTimeLeft;

	[SerializeField] Slider untilFullUpgradeSlider;
	[SerializeField] Slider missionEndsInSlider;
	[SerializeField] Slider breakTimeLeftSlider;

#pragma warning restore 0649

	void UpdateDisplay()
	{
		Upgrade activeUpgrade = UpgradeList.Instance.GetActive()[0];
		MissionProgress activeMission = activeUpgrade.Progress.MissionCalendar.GetActiveMission();
		ShowVisual(activeMission != null);

		if (activeMission != null)
		{
			// Info math
			int goal = activeMission.Mission.Goal;
			int progress = activeMission.Progress;

			TimeSpan timeEnd = activeMission.Mission.TimeEnd;
			TimeSpan timeNow = DateTime.Now.TimeOfDay;

			TimeSpan progressTime = new TimeSpan(0, 0, progress);
			TimeSpan upgradeTimeLeft = new TimeSpan(0, 0, goal - progress);
			TimeSpan timeGoal = new TimeSpan(0, 0, goal);
			TimeSpan timePassed = timeNow - activeMission.Mission.TimeStart;
			TimeSpan timeLeft = timeEnd - timeNow;
			TimeSpan breakLeft = timeLeft - upgradeTimeLeft;
			breakLeft = breakLeft < TimeSpan.Zero ? TimeSpan.Zero : breakLeft;
			TimeSpan breakTotal = new TimeSpan(0, 0, activeMission.Mission.BreakSeconds);
			TimeSpan breakPassed = breakTotal - breakLeft;

			// Display
			untilFullUpgrade.text = TimeConverter.TimeString((int)progressTime.TotalSeconds, true, false, true, true);
			missionEndsIn.text = TimeConverter.TimeString((int)timeLeft.TotalSeconds, true, false, true, true);
			breakTimeLeft.text = TimeConverter.TimeString((int)breakLeft.TotalSeconds, true, false, true, true);

			untilFullUpgradeSlider.value = (float)progress / (float)goal;
			missionEndsInSlider.value = 1f - (float)timePassed.TotalSeconds / (float)timeGoal.TotalSeconds;
			breakTimeLeftSlider.value = 1f - (float)breakPassed.TotalSeconds / (float)breakTotal.TotalSeconds;
		}
	}

	void ShowVisual(bool value)
	{
		if (value == true)
		{
			GetComponent<CanvasGroup>().alpha = 1f;
		}
		else
		{
			GetComponent<CanvasGroup>().alpha = 0f;
		}
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

	void ActiveUpgradesChanged(IReadOnlyList<Upgrade> upgrade)
	{
		UpdateDisplay();
	}

	// Unity
	private void OnEnable()
	{
		InvokeRepeating("UpdateDisplay", 0, 1);

		Stopwatch.OnStopwatchSrart += StopWatchStart;
		Stopwatch.OnStopwatchStop += StopWatchStop;
		UpgradeList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}

	private void OnDisable()
	{
		CancelInvoke("UpdateDisplay");

		Stopwatch.OnStopwatchSrart -= StopWatchStart;
		Stopwatch.OnStopwatchStop -= StopWatchStop;
		UpgradeList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;
	}
}