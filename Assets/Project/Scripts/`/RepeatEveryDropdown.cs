using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatEveryDropdown : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Dropdown dropdown;

#pragma warning restore 0649

	public enum Interval
	{
		Day, Week
	}

	public Interval GetActive()
	{
		string activeName = dropdown.options[dropdown.value].text;
		return GetElements().First(obj => obj.ToString() == activeName);
	}

	void UpdateDisplay()
	{
		List<string> allValues = GetElements().Select(obj => obj.ToString()).ToList();
		dropdown.ClearOptions();
		dropdown.AddOptions(allValues);
		dropdown.value = 1;
	}

	IEnumerable<Interval> GetElements()
	{
		return Enum.GetValues(typeof(Interval)).Cast<Interval>();
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();
	}
}