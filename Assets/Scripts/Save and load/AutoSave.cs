using UnityEngine;

public class AutoSave : MonoBehaviour
{
	[SerializeField] float minutesDelay = 5;
	float secondsDelay { get => minutesDelay * 60; }

	float nextSave;

	private void Start()
	{
		nextSave = secondsDelay;
	}

	private void Update()
	{
		if (UnityEngine.Time.time >= nextSave)
		{
			Save();
			nextSave = UnityEngine.Time.time + secondsDelay;
		}
	}

	void Save()
	{
		JsonSerializer.SaveProgress();
	}

	// Events
	void NewUpgradeCreated(Upgrade upgrade)
	{
		Save();
	}

	void StopwatchStop()
	{
		Save();
	}	
	//

	private void OnEnable()
	{
		Upgrade.OnNewUpgradeCreated += NewUpgradeCreated;
		Stopwatch.OnStopwatchStop += StopwatchStop;
	}

	private void OnDisable()
	{
		Upgrade.OnNewUpgradeCreated -= NewUpgradeCreated;
		Stopwatch.OnStopwatchStop -= StopwatchStop;
	}
}