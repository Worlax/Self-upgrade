using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayOfWeekDropdown : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Dropdown dropdown;

#pragma warning restore 0649

	public DayOfWeek GetActive()
	{
		string activeName = dropdown.options[dropdown.value].text;
		return GetElements().First(obj => obj.ToString() == activeName);
	}

	void UpdateDisplay()
	{
		List<string> allValues = GetElements().Select(obj => obj.ToString()).ToList();
		dropdown.ClearOptions();
		dropdown.AddOptions(allValues);
	}

	IEnumerable<DayOfWeek> GetElements()
	{
		List<DayOfWeek> daysOfWeek = new List<DayOfWeek>()
		{
			DayOfWeek.Monday,
			DayOfWeek.Tuesday,
			DayOfWeek.Wednesday,
			DayOfWeek.Thursday,
			DayOfWeek.Friday,
			DayOfWeek.Saturday,
			DayOfWeek.Sunday,
		};

		return daysOfWeek;
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();
	}
}