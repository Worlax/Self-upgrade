using System;
using UnityEngine;
using UnityEngine.UI;

public class StatisticSettings : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] public Toggle showSettings;
	[SerializeField] Image iconOn;
	[SerializeField] Image iconOff;
	[SerializeField] RectTransform content;
	[SerializeField] RectTransform calendarDependentContent;
	[SerializeField] RectTransform dayAverageContent;

	[SerializeField] Text calendarDependentScopeLabel;
	[SerializeField] Text compareToAverageLabel;

	[field: SerializeField] public StatisticScopeDropdown StatisticScope { get; private set; }
	[field: SerializeField] public Toggle CalendarDependent { get; private set; }
	[field: SerializeField] public Toggle DayAverage { get; private set; }
	[field: SerializeField] public Toggle CompareToAverage { get; private set; }

#pragma warning restore 0649

	public event Action OnSettingsChanged;

	void UpdateVisibleSettings()
	{
		if (StatisticScope.GetActive() == StatisticScopeDropdown.StatisticRange.Day)
		{
			calendarDependentContent.gameObject.SetActive(false);
			dayAverageContent.gameObject.SetActive(false);
		}
		else
		{
			calendarDependentContent.gameObject.SetActive(true);
			dayAverageContent.gameObject.SetActive(true);
		}
	}

	void UpdateLabels()
	{
		string calendarDependentScopeString = "Calculate from ";
		string compareToAverageString = "Compare to average ";

		switch (StatisticScope.GetActive())
		{
			case StatisticScopeDropdown.StatisticRange.Day:
				compareToAverageString += "day";
				break;

			case StatisticScopeDropdown.StatisticRange.Week:
				calendarDependentScopeString += "monday";
				compareToAverageString += "week";
				break;

			case StatisticScopeDropdown.StatisticRange.Month:
				calendarDependentScopeString += "start of the month";
				compareToAverageString += "month";
				break;

			case StatisticScopeDropdown.StatisticRange.Year:
				calendarDependentScopeString += "start of the year";
				compareToAverageString += "year";
				break;
		}

		calendarDependentScopeLabel.text = calendarDependentScopeString;
		compareToAverageLabel.text = compareToAverageString;
	}

	// Events
	void ShowSettings(bool value)
	{
		content.gameObject.SetActive(value);
		iconOn.gameObject.SetActive(value);
		iconOff.gameObject.SetActive(!value);
	}

	void StatisticRangeValueChanged()
	{
		UpdateVisibleSettings();
		UpdateLabels();
		OnSettingsChanged?.Invoke();
	}

	void CalculateFromMondayValueChanged(bool value)
	{
		OnSettingsChanged?.Invoke();
	}

	void DayAverageValueChanged(bool value)
	{
		OnSettingsChanged?.Invoke();
	}

	void CompareToWeekAverageValueChanged(bool value)
	{
		OnSettingsChanged?.Invoke();
	}

	// Unity
	private void Start()
	{
		UpdateVisibleSettings();
		UpdateLabels();
	}

	private void OnEnable()
	{
		showSettings.onValueChanged.AddListener(ShowSettings);
		StatisticScope.OnValueChanged += StatisticRangeValueChanged;
		CalendarDependent.onValueChanged.AddListener(CalculateFromMondayValueChanged);
		DayAverage.onValueChanged.AddListener(DayAverageValueChanged);
		CompareToAverage.onValueChanged.AddListener(CompareToWeekAverageValueChanged);
	}

	private void OnDisable()
	{
		showSettings.onValueChanged.RemoveListener(ShowSettings);
		StatisticScope.OnValueChanged -= StatisticRangeValueChanged;
		CalendarDependent.onValueChanged.RemoveListener(CalculateFromMondayValueChanged);
		DayAverage.onValueChanged.RemoveListener(DayAverageValueChanged);
		CompareToAverage.onValueChanged.RemoveListener(CompareToWeekAverageValueChanged);
	}
}