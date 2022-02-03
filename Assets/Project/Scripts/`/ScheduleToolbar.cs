using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleToolbar : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform content;
	[SerializeField] Button edit;
	[SerializeField] Button delete;

#pragma warning restore 0649

	List<ScheduleItem> toggledItems = new List<ScheduleItem>();

	void Show(bool value)
	{
		content.gameObject.SetActive(value);
	}

	// Events
	void ScheduleItemDisplayed(ScheduleItem item)
	{
		item.OnToggleChanged += ScheduleItemToggleChanged;
	}

	void ScheduleItemToggleChanged(ScheduleItem item, bool value)
	{
		if (value == true)
		{
			toggledItems.Add(item);
		}
		else
		{
			toggledItems.Remove(item);
		}

		Show(toggledItems.Count > 0);
	}

	void Edit()
	{

	}

	void Delete()
	{
		foreach (ScheduleItem item in toggledItems.ToArray())
		{
			item.Upgrade.Progress.MissionCalendar.RemoveMissionFromSchedule(item.Mission);
			toggledItems.Remove(item);
		}

		Show(toggledItems.Count > 0);
	}

	// Unity
	private void OnEnable()
	{
		Schedule.OnItemDisplayed += ScheduleItemDisplayed;
		edit.onClick.AddListener(Edit);
		delete.onClick.AddListener(Delete);
	}

	private void OnDisable()
	{
		Schedule.OnItemDisplayed -= ScheduleItemDisplayed;
		edit.onClick.RemoveListener(Edit);
		delete.onClick.RemoveListener(Delete);
	}
}