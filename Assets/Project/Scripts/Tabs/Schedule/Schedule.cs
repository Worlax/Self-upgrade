using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Schedule : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform mondayContent;
	[SerializeField] RectTransform tuesdayContent;
	[SerializeField] RectTransform wednesdayContent;
	[SerializeField] RectTransform thursdayContent;
	[SerializeField] RectTransform fridayContent;
	[SerializeField] RectTransform saturdayContent;
	[SerializeField] RectTransform sundayContent;

#pragma warning restore 0649

	public bool ShowBreakTime = true;

	public static event Action OnEnabled;
	public static event Action OnDisabled;

	List<ScheduleItem> FillContent()
	{
		ClearContent();
		List<ScheduleItem> createdItems = new List<ScheduleItem>();

		createdItems.AddRange(FillOneDay(DayOfWeek.Monday, mondayContent));
		createdItems.AddRange(FillOneDay(DayOfWeek.Tuesday, tuesdayContent));
		createdItems.AddRange(FillOneDay(DayOfWeek.Wednesday, wednesdayContent));
		createdItems.AddRange(FillOneDay(DayOfWeek.Thursday, thursdayContent));
		createdItems.AddRange(FillOneDay(DayOfWeek.Friday, fridayContent));
		createdItems.AddRange(FillOneDay(DayOfWeek.Saturday, saturdayContent));
		createdItems.AddRange(FillOneDay(DayOfWeek.Sunday, sundayContent));

		return createdItems;
	}

	List<ScheduleItem> FillOneDay(DayOfWeek dayOfWeek, RectTransform parent)
	{
		return ScheduleItemConstructor.Instance.CreateItemsForDayOfTheWeek(dayOfWeek, ShowBreakTime, parent).ToList();
	}

	void ClearContent()
	{
		foreach (Transform transform in mondayContent) Destroy(transform.gameObject);
		foreach (Transform transform in tuesdayContent) Destroy(transform.gameObject);
		foreach (Transform transform in wednesdayContent) Destroy(transform.gameObject);
		foreach (Transform transform in thursdayContent) Destroy(transform.gameObject);
		foreach (Transform transform in fridayContent) Destroy(transform.gameObject);
		foreach (Transform transform in saturdayContent) Destroy(transform.gameObject);
		foreach (Transform transform in sundayContent) Destroy(transform.gameObject);
	}

	// Unity
	private void OnEnable()
	{
		FillContent();

		OnEnabled?.Invoke();
	}

	private void OnDisable()
	{
		OnDisabled?.Invoke();
	}
}