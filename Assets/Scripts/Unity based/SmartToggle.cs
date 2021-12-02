using UnityEngine;
using UnityEngine.UI;

public class SmartToggle : Toggle
{
	bool savedState;

	[SerializeField] public new bool interactable
	{
		get
		{
			return base.interactable;
		}
		set
		{
			if (interactable == false && value == true)
			{
				isOn = savedState;
				base.interactable = true;
			}
			else if (interactable == true && value == false)
			{
				savedState = isOn;
				isOn = false;
				base.interactable = false;
			}
		}
	}
}