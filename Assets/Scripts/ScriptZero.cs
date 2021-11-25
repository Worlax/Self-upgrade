using UnityEngine;

public class ScriptZero : Singleton<ScriptZero>
{
	protected override void Awake()
	{
		base.Awake();

		JsonSerializer.LoadProgress();

		DeleteMeLater();
	}

	void DeleteMeLater()
	{
		if (Upgrade.AllUpgrades.Count == 0)
		{
			Upgrade.CreateUpgrade("Autocreated", UpgradeType.Timer);
		}
	}
}