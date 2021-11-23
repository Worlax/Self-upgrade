using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
	Upgrade Upgrade;
	bool timerIsRunning;
	float nextUpdateSecond;

	[SerializeField] Text seconds;
	[SerializeField] Text minutes;
	[SerializeField] Text hours;

	[SerializeField] Button start;
	[SerializeField] Button stop;

	private void Start()
	{
		print("stopwatch Satrart");
		DisableTimer();
	}

	private void Update()
	{
		if (timerIsRunning && UnityEngine.Time.time > nextUpdateSecond)
		{
			Upgrade.Calendar.ChangeTodayValueBy(1);
			UpdateDisplay();

			nextUpdateSecond += 1;
		}
	}

	public void SetUpgrade(Upgrade upgrade)
	{
		Upgrade = upgrade;
		UpdateDisplay();
	}

	void UpdateDisplay()
	{
		if (Upgrade != null)
		{
			int sec = Upgrade.Calendar.GetTodayValue();

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

	void ActivateTimer()
	{
		if (!timerIsRunning)
		{
			nextUpdateSecond = UnityEngine.Time.time + 1;
			timerIsRunning = true;
		}
	}

	void DisableTimer()
	{
		if (timerIsRunning)
		{
			timerIsRunning = false;
		}
	}

	private void OnEnable()
	{
		SetUpgrade(UpgradeDropdown.Instance.GetActive());

		start.onClick.AddListener(ActivateTimer);
		stop.onClick.AddListener(DisableTimer);

		UpgradeDropdown.Instance.OnActiveUpgradeChanged += SetUpgrade;
	}

	private void OnDisable()
	{
		start.onClick.RemoveListener(ActivateTimer);
		stop.onClick.RemoveListener(DisableTimer);

		UpgradeDropdown.Instance.OnActiveUpgradeChanged += SetUpgrade;
	}
}