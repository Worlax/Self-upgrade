using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Schedule : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button newSchedule;

	[SerializeField] int daysPerPage;
	[SerializeField] Button previousDaysPage;
	[SerializeField] Button nextDaysPage;

	[SerializeField] ToggleGroup toggleGroup;

	[SerializeField] RectTransform monday;
	[SerializeField] RectTransform tuesday;
	[SerializeField] RectTransform wednesday;
	[SerializeField] RectTransform thursday;
	[SerializeField] RectTransform friday;
	[SerializeField] RectTransform saturday;
	[SerializeField] RectTransform sunday;

	[SerializeField] RectTransform mondayContent;
	[SerializeField] RectTransform tuesdayContent;
	[SerializeField] RectTransform wednesdayContent;
	[SerializeField] RectTransform thursdayContent;
	[SerializeField] RectTransform fridayContent;
	[SerializeField] RectTransform saturdayContent;
	[SerializeField] RectTransform sundayContent;

#pragma warning restore 0649

	//public static event Action OnEnabled;
	//public static event Action OnDisabled;
	public static event Action<ScheduleItem> OnItemDisplayed;

	public bool ShowBreakTime = true;

	List<RectTransform> days = new List<RectTransform>();
	List<RectTransform> displayedDays = new List<RectTransform>();

	void UpdateContent()
	{
		ClearContent();
		FillContent();
	}

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

		createdItems.ForEach(obj => OnItemDisplayed?.Invoke(obj));
		return createdItems;
	}

	List<ScheduleItem> FillOneDay(DayOfWeek dayOfWeek, RectTransform parent)
	{
		return ScheduleItemConstructor.Instance.CreateItemsForDayOfTheWeek(dayOfWeek, ShowBreakTime, parent, toggleGroup).ToList();
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

	void DisplayDays(int index, int count)
	{
		displayedDays.Clear();
		days.ForEach(obj => obj.gameObject.SetActive(false));

		displayedDays = days.GetPossibleRange(index, count);
		displayedDays.ForEach(obj => obj.gameObject.SetActive(true));

		UpdateDaysBrowsingButtons();
	}

	void UpdateDaysBrowsingButtons()
	{
		bool firstDayShowed = displayedDays.Contains(days.First());
		bool lastDayShowed = displayedDays.Contains(days.Last());

		previousDaysPage.interactable = !firstDayShowed;
		nextDaysPage.interactable = !lastDayShowed;
	}

	void HighlightToday()
	{
		float defaultAlpha = 0.6f;
		float highlightAlpha = 1f;
		RectTransform higlightedDay = null;

		switch (DateTime.Today.DayOfWeek)
		{
			case DayOfWeek.Monday:
				higlightedDay = monday;
				break;

			case DayOfWeek.Tuesday:
				higlightedDay = tuesday;
				break;

			case DayOfWeek.Wednesday:
				higlightedDay = wednesday;
				break;

			case DayOfWeek.Thursday:
				higlightedDay = thursday;
				break;

			case DayOfWeek.Friday:
				higlightedDay = friday;
				break;

			case DayOfWeek.Saturday:
				higlightedDay = saturday;
				break;

			case DayOfWeek.Sunday:
				higlightedDay = sunday;
				break;
		}

		days.ForEach(obj => obj.GetComponent<CanvasGroup>().alpha = defaultAlpha);
		higlightedDay.GetComponent<CanvasGroup>().alpha = highlightAlpha;
	}

	// Events
	void NewSchedule()
	{
		WindowManager.Instance.CreateNewScheduleItemWindow();
	}

	void PreviousDaysPage()
	{
		int firstDisplayedDay = days.IndexOf(displayedDays.First());
		DisplayDays(firstDisplayedDay - 1, -daysPerPage);
	}

	void NextDaysPage()
	{
		int lastDisplayedDay = days.IndexOf(displayedDays.Last());
		DisplayDays(lastDisplayedDay + 1, daysPerPage);
	}

	// Unity

	private void Start()
	{
		days = new List<RectTransform>()
		{
			monday, tuesday, wednesday, thursday, friday, saturday, sunday
		};

		HighlightToday();
		UpdateContent();
		DisplayDays(0, daysPerPage);
	}

	private void OnEnable()
	{
		UpdateContent();
		newSchedule.onClick.AddListener(NewSchedule);
		previousDaysPage.onClick.AddListener(PreviousDaysPage);
		nextDaysPage.onClick.AddListener(NextDaysPage);
		MissionCalendar.OnMissionAddedToSchedule += UpdateContent;
		MissionCalendar.OnMissionRemovedFromSchedule += UpdateContent;
	}

	private void OnDisable()
	{
		newSchedule.onClick.RemoveListener(NewSchedule);
		previousDaysPage.onClick.RemoveListener(PreviousDaysPage);
		nextDaysPage.onClick.RemoveListener(NextDaysPage);
		MissionCalendar.OnMissionAddedToSchedule -= UpdateContent;
		MissionCalendar.OnMissionRemovedFromSchedule -= UpdateContent;
	}
}