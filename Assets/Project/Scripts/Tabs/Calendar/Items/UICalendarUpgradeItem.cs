using System;
using UnityEngine;
using UnityEngine.UI;

public class UICalendarUpgradeItem : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text upgradeName;
	[SerializeField] Text upgradeValue;

#pragma warning restore 0649

	public void Init(Upgrade upgrade, DateTime date)
	{
		upgradeName.text = upgrade.Name;

		if (upgradeValue)
		{
			string valueText = "";

			switch (upgrade.Type)
			{
				case UpgradeType.Timer:
					int seconds = upgrade.Progress.GetValue(date);
					valueText = TimeConverter.TimeString(seconds, false);
					break;

				case UpgradeType.MultiChecker:
					valueText = upgrade.Progress.GetValue(date).ToString();
					break;
			}

			upgradeValue.text = valueText; upgrade.Progress.GetValue(date).ToString();
		}
	}
}