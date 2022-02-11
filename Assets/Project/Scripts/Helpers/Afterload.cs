using UnityEngine;
using UnityEngine.UI;

public class Afterload : Singleton<Afterload>
{
#pragma warning disable 0649

	[SerializeField] ToggleGroup menuToggleGroup;
	[SerializeField] MenuToggle startingMenuToggle;

#pragma warning restore 0649

	private void Start()
	{
		startingMenuToggle.isOn = true;
		menuToggleGroup.allowSwitchOff = false;
	}
}