using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeWindow : Window
{
#pragma warning disable 0649

	[SerializeField] InputField upgradeName;

#pragma warning restore 0649

	protected override bool Execute()
	{
		Upgrade.CreateUpgrade(upgradeName.text, UpgradeType.Timer);

		return true;
	}
}