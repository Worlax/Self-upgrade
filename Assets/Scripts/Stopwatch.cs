using System;
using System.Linq;
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

	Upgrade upgrade;
	bool timerIsRunning;
	float nextUpdateSecond;

	public static event Action OnStopwatchSrart;
	public static event Action OnStopwatchStop;

	private void Update()
	{
		if (timerIsRunning && UnityEngine.Time.time > nextUpdateSecond)
		{
			upgrade.Calendar.ChangeTodayValueBy(1);
			UpdateDisplay();

			nextUpdateSecond += 1;
		}
	}

	public void ChangeActiveUpgrade(Upgrade upgrade)
	{
		this.upgrade = upgrade;
		UpdateDisplay();
	}

	void UpdateDisplay()
	{
		if (upgrade != null)
		{
			upgradeName.text = upgrade.Name;

			int sec = upgrade.Calendar.GetTodayValue();

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
		upgrade.Calendar.ChangeTodayValueBy(-minutes * 60);
		UpdateDisplay();

		disappearingText.Play("-" + minutes);
	}

	// Events
	void ActiveUpgradeChanged(Upgrade upgrade)
	{
		ChangeActiveUpgrade(upgrade);
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
	//

	private void OnEnable()
	{
		ChangeActiveUpgrade(UpgradeDropdown.Instance.GetActive());

		UpgradeDropdown.Instance.OnActiveUpgradeChanged += ActiveUpgradeChanged;

		start.onClick.AddListener(StartStopwatch);
		stop.onClick.AddListener(StopStopwatch);
		gotDistractedBy15.onClick.AddListener(GotDistractedBy15);
		gotDistractedBy30.onClick.AddListener(GotDistractedBy30);
		gotDistractedCustom.onClick.AddListener(GotDistractedCustom);
	}

	private void OnDisable()
	{
		StopStopwatch();

		UpgradeDropdown.Instance.OnActiveUpgradeChanged -= ActiveUpgradeChanged;

		start.onClick.RemoveListener(StartStopwatch);
		stop.onClick.RemoveListener(StopStopwatch);
		gotDistractedBy15.onClick.RemoveListener(GotDistractedBy15);
		gotDistractedBy30.onClick.RemoveListener(GotDistractedBy30);
		gotDistractedCustom.onClick.RemoveListener(GotDistractedCustom);
	}
}