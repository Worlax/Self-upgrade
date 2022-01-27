using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text hours1;
	[SerializeField] Text hours2;
	[SerializeField] Text minutes1;
	[SerializeField] Text minutes2;
	[SerializeField] Text seconds1;
	[SerializeField] Text seconds2;

	[SerializeField] Button start;
	[SerializeField] Button stop;
	[SerializeField] Button addMinutes;
	[SerializeField] Button subtructMinutes;
 
#pragma warning restore 0649

	bool timerIsRunning;
	float nextUpdateSecond;

	public static event Action OnStopwatchSrart;
	public static event Action OnStopwatchStop;
	public static event Action OnStopwatchSecondPassed;

	private void Update()
	{
		if (timerIsRunning && UnityEngine.Time.time > nextUpdateSecond)
		{
			Upgrade activeUpgrade = UpgradeList.Instance.GetActive()[0];

			activeUpgrade.Progress.ChangeProgressBy(DateTime.Today, 1);
			UpdateDisplay();
			nextUpdateSecond += 1;

			OnStopwatchSecondPassed?.Invoke();
		}
	}

	void UpdateDisplay()
	{
		Upgrade activeUpgrade = UpgradeList.Instance.GetActive()[0];

		if (activeUpgrade != null)
		{
			int sec = activeUpgrade.Progress.GetValue(DateTime.Today);
			string hoursFirst = GetFirstDigit(TimeConverter.Hours(sec)).ToString();

			hours1.text = GetFirstDigit(TimeConverter.Hours(sec));
			hours2.text = GetSecondDigit(TimeConverter.Hours(sec));

			minutes1.text = GetFirstDigit(TimeConverter.Minutes(sec));
			minutes2.text = GetSecondDigit(TimeConverter.Minutes(sec));

			seconds1.text = GetFirstDigit(TimeConverter.Seconds(sec));
			seconds2.text = GetSecondDigit(TimeConverter.Seconds(sec));
		}
	}

	string GetFirstDigit(int number)
	{
		return (number / 10).ToString();
	}

	string GetSecondDigit(int number)
	{
		return (number - number / 10 * 10).ToString();
	}

	void StartStopwatch()
	{
		start.gameObject.SetActive(false);
		stop.gameObject.SetActive(true);

		if (!timerIsRunning)
		{
			nextUpdateSecond = UnityEngine.Time.time + 1;
			timerIsRunning = true;
		}

		OnStopwatchSrart?.Invoke();
	}

	void StopStopwatch()
	{
		start.gameObject.SetActive(true);
		stop.gameObject.SetActive(false);

		if (timerIsRunning)
		{
			timerIsRunning = false;
		}

		OnStopwatchStop?.Invoke();
	}

	void AddMinutes()
	{
		StopStopwatch();
		ChangeMinutesWindow window = WindowManager.Instance.CreateAddMinutesWindow();
		window.OnDestroy += UpdateDisplay;
	}

	void SubtructMinutes()
	{
		StopStopwatch();
		ChangeMinutesWindow window = WindowManager.Instance.CreateSubtructMinutesWindow();
		window.OnDestroy += UpdateDisplay;
	}

	// Events
	void ActiveUpgradesChanged(IReadOnlyList<Upgrade> upgrades)
	{
		StopStopwatch();
		UpdateDisplay();
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();

		UpgradeList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;

		start.onClick.AddListener(StartStopwatch);
		stop.onClick.AddListener(StopStopwatch);
		addMinutes.onClick.AddListener(AddMinutes);
		subtructMinutes.onClick.AddListener(SubtructMinutes);
	}

	private void OnDisable()
	{
		StopStopwatch();

		UpgradeList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;

		start.onClick.RemoveListener(StartStopwatch);
		stop.onClick.RemoveListener(StopStopwatch);
		addMinutes.onClick.RemoveListener(AddMinutes);
		subtructMinutes.onClick.RemoveListener(SubtructMinutes);
	}
}