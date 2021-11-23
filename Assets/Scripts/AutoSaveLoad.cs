using UnityEngine;

public class AutoSaveLoad : MonoBehaviour
{
	[SerializeField] float saveIntervalInMinutes = 5;
	float saveIntervalInSecond { get => saveIntervalInMinutes * 60; }

	float nextSave;

	private void Awake()
	{
		JsonSerializer.LoadProgress();
	}

	private void Start()
	{
		nextSave = saveIntervalInSecond;
	}

	private void Update()
	{
		if (UnityEngine.Time.time >= nextSave)
		{
			JsonSerializer.SaveProgress();
			nextSave = UnityEngine.Time.time + saveIntervalInSecond;
		}
	}
}