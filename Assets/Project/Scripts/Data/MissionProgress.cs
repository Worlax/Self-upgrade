using UnityEngine;
using Newtonsoft.Json;

public class MissionProgress
{
	[JsonProperty] public Mission Mission { get; private set; }
	[JsonProperty] public int Progress { get; private set; }

	[JsonConstructor] MissionProgress() { }

	public MissionProgress(Mission mission)
	{
		Mission = mission.Clone();
	}

	public void ChangeProgress(int value)
	{
		Progress = value;
		Progress = Progress < 0 ? 0 : Progress;
	}

	public void ChangeProgressBy(int by)
	{
		Progress += by;
		Progress = Progress < 0 ? 0 : Progress;
	}
}