using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticScopeDropdown : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Dropdown dropdown;

#pragma warning restore 0649

	public event Action OnValueChanged;

	public enum StatisticRange
	{
		Day, Week, Month, Year
	}

	public StatisticRange GetActive()
	{
		if (dropdown.options.Count == 0)
		{
			UpdateDisplay();
		}

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

	IEnumerable<StatisticRange> GetElements()
	{
		return Enum.GetValues(typeof(StatisticRange)).Cast<StatisticRange>();
	}

	// Events
	void ValueChanged(int value)
	{
		OnValueChanged?.Invoke();
	}

	// Unity
	private void Start()
	{
		UpdateDisplay();
	}

	private void OnEnable()
	{
		dropdown.onValueChanged.AddListener(ValueChanged);
	}

	private void OnDisable()
	{
		dropdown.onValueChanged.RemoveListener(ValueChanged);
	}
}