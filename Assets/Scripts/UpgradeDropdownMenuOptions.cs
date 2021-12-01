using UnityEngine;

public class UpgradeDropdownMenuOptions : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] bool timers;
	[SerializeField] bool timersAll;
	[SerializeField] bool checkers;
	[SerializeField] bool checkersAll;
	[SerializeField] bool multiCheckers;
	[SerializeField] bool multiCheckersAll;

#pragma warning restore 0649

	private void OnEnable()
	{
		UpgradeDropdown.Options.SetToggles(timers, checkers, multiCheckers);
		UpgradeDropdown.Options.SetDropdown(timersAll, checkersAll, multiCheckersAll);
	}
}