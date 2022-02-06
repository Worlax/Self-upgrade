using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistic : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Dropdown statisticRange;

	[SerializeField] Toggle calculateFromMonday;
	[SerializeField] Toggle dayAverage;
	[SerializeField] Toggle compareToWeekAverage;

	//

	[SerializeField] Text hoursToday;
	[SerializeField] Text hoursThisWeek;
	[SerializeField] Text hoursThisMonth;
	[SerializeField] Text hoursThisYear;
	[SerializeField] Text hoursTotal;

#pragma warning restore 0649

	enum StatisticRange
	{
		Day, Week, Year
	}

	void UpdateDisplay()
	{
		IReadOnlyList<Upgrade> activeUpgrades = UpgradeList.Instance.GetActive();
		DateTime today = DateTime.Today;

		int hToday = 0;
		int hThisWeek = 0;
		int hThisMonth = 0;
		int hThisYear = 0;
		int hTotal = 0;

		//int missionsMissed = 0;
		//int missionsMissed = 0;

		foreach (Upgrade upgrade in activeUpgrades)
		{
			DateTime startOfTheWeek = GetThisWeekFirstDay(today);
			DateTime startOfTheMonth = new DateTime(today.Year, today.Month, 1);
			DateTime startOfTheYear = new DateTime(today.Year, 1, 1);

			hToday += upgrade.Progress.GetValue(today);
			hThisWeek += upgrade.Progress.GetValueInDiapason(startOfTheWeek, today);
			hThisMonth += upgrade.Progress.GetValueInDiapason(startOfTheMonth, today);
			hThisYear += upgrade.Progress.GetValueInDiapason(startOfTheYear, today);
			hTotal += upgrade.Progress.GetTotalValue();
		}

		hoursToday.text = TimeConverter.TimeString(hTotal, true, false, true);
		hoursThisWeek.text = TimeConverter.TimeString(hThisWeek, true, false, true);
		hoursThisMonth.text = TimeConverter.TimeString(hThisMonth, true, false, true);
		hoursThisYear.text = TimeConverter.TimeString(hThisYear, true, false, true);
		hoursTotal.text = TimeConverter.TimeString(hTotal, true, false, true);
	}

	DateTime GetThisWeekFirstDay(DateTime date)
	{
		DateTime firstDay = date;

		while (firstDay.DayOfWeek != DayOfWeek.Monday)
		{
			firstDay = firstDay.AddDays(-1);
		}

		return firstDay;
	}

	// Events
	void ActiveUpgradesChanged(IReadOnlyList<Upgrade> upgrades)
	{
		UpdateDisplay();
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();

		UpgradeList.Instance.OnActiveUpgradesChanged += ActiveUpgradesChanged;
	}

	private void OnDisable()
	{
		UpgradeList.Instance.OnActiveUpgradesChanged -= ActiveUpgradesChanged;
	}
}