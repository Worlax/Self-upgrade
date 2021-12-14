using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text upgradeName;

	[SerializeField] Text seconds;
	[SerializeField] Text minutes;
	[SerializeField] Text hours;

	[SerializeField] Button start;
	[SerializeField] Button stop;

	[SerializeField] Button gotDistractedBy15;
	[SerializeField] Button gotDistractedBy30;
	[SerializeField] Button gotDistractedCustom;
	[SerializeField] InputField gotDistractedInputField;
	[SerializeField] DisappearingText disappearingText;
 
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
			Upgrade activeUpgrade = UpgradesList.Instance.GetActive()[0];

			activeUpgrade.Calendar.ChangeValueBy(DateTime.Today, 1);
			UpdateDisplay();
			nextUpdateSecond += 1;

			OnStopwatchSecondPassed?.Invoke();
		}
	}

	void UpdateDisplay()
	{
		Upgrade activeUpgrade = UpgradesList.Instance.GetActive()[0];

		if (activeUpgrade != null)
		{
			upgradeName.text = activeUpgrade.Name;

			int sec = activeUpgrade.Calendar.GetValue(DateTime.Today);

			seconds.text = FormatNumberForDisplay(TimeConverter.Seconds(sec));
			minutes.text = FormatNumberForDisplay(TimeConverter.Minutes(sec));
			hours.text = FormatNumberForDisplay(TimeConverter.Hours(sec));
		}
	}

	string FormatNumberForDisplay(int number)
	{
		if (number < 10)
		{
			return "0" + number.ToString();
		}
		else
		{
			return number.ToString();
		}
	}

	void StartStopwatch()
	{
		if (!timerIsRunning)
		{
			nextUpdateSecond = UnityEngine.Time.time + 1;
			timerIsRunning = true;
		}

		OnStopwatchSrart?.Invoke();
	}

	void StopStopwatch()
	{
		if (timerIsRunning)
		{
			timerIsRunning = false;
		}

		OnStopwatchStop?.Invoke();
	}

	void GotDistracted(int minutes)
	{
		Upgrade activeUpgrade = UpgradesList.Instance.GetActive()[0];

		activeUpgrade.Calendar.ChangeValueBy(DateTime.Today, -minutes * 60);
		UpdateDisplay();

		disappearingText.Play("-" + minutes);
	}

	// Events
	void ActiveUpgradesChanged(List<Upgrade> upgrades)
	{
		StopStopwatch();
		UpdateDisplay();
	}

	void GotDistractedBy15()
	{
		GotDistracted(15);
	}

	void GotDistractedBy30()
	{
		GotDistracted(30);
	}

	void GotDistractedCustom()
	{
		int distractedBy = Int32.Parse(gotDistractedInputField.text);
		GotDistracted(distractedBy);
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();

		UpgradesList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;

		start.onClick.AddListener(StartStopwatch);
		stop.onClick.AddListener(StopStopwatch);
		gotDistractedBy15.onClick.AddListener(GotDistractedBy15);
		gotDistractedBy30.onClick.AddListener(GotDistractedBy30);
		gotDistractedCustom.onClick.AddListener(GotDistractedCustom);
	}

	private void OnDisable()
	{
		StopStopwatch();

		UpgradesList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;

		start.onClick.RemoveListener(StartStopwatch);
		stop.onClick.RemoveListener(StopStopwatch);
		gotDistractedBy15.onClick.RemoveListener(GotDistractedBy15);
		gotDistractedBy30.onClick.RemoveListener(GotDistractedBy30);
		gotDistractedCustom.onClick.RemoveListener(GotDistractedCustom);
	}
}