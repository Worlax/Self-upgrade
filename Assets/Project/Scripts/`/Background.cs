using UnityEngine;

public class Background : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform headerDays;
	[SerializeField] RectTransform headerDaysOfTheWeek;
	[SerializeField] RectTransform smallBody;
	[SerializeField] RectTransform bigBody;

#pragma warning restore 0649

	//void DisableAll()
	//{
	//	headerDays.gameObject.SetActive(false);
	//	headerDaysOfTheWeek.gameObject.SetActive(false);
	//	smallBody.gameObject.SetActive(false);
	//	bigBody.gameObject.SetActive(false);
	//}

	//void ActivateGraphic(params RectTransform[] graphic)
	//{
	//	foreach (RectTransform transform in graphic)
	//	{
	//		transform.gameObject.SetActive(true);
	//	}
	//}

	//// Events
	//void DayTabEnabled()
	//{
	//	ActivateGraphic(headerDays, bigBody);
	//}

	//void MonthTabEnabled()
	//{
	//	ActivateGraphic(headerDaysOfTheWeek, bigBody);
	//}

	//void YerTabEnabled()
	//{
	//	ActivateGraphic(smallBody);
	//}

	//void ScheduleTabEnabled()
	//{
	//	ActivateGraphic(headerDaysOfTheWeek, bigBody);
	//}

	//void TabDisabled()
	//{
	//	DisableAll();
	//}

	//// Unity
	//private void OnEnable()
	//{
	//	DisableAll();

	//	DayTab.OnEnabled += DayTabEnabled;
	//	MonthTab.OnEnabled += MonthTabEnabled;
	//	YearTab.OnEnabled += YerTabEnabled;
	//	Schedule.OnEnabled += ScheduleTabEnabled;

	//	DayTab.OnDisabled += TabDisabled;
	//	MonthTab.OnDisabled += TabDisabled;
	//	YearTab.OnDisabled += TabDisabled;
	//	Schedule.OnDisabled += TabDisabled;
	//}

	//private void OnDisable()
	//{
	//	DayTab.OnEnabled -= DayTabEnabled;
	//	MonthTab.OnEnabled -= MonthTabEnabled;
	//	YearTab.OnEnabled -= YerTabEnabled;
	//	Schedule.OnEnabled -= ScheduleTabEnabled;

	//	DayTab.OnDisabled -= TabDisabled;
	//	MonthTab.OnDisabled -= TabDisabled;
	//	YearTab.OnDisabled -= TabDisabled;
	//	Schedule.OnDisabled -= TabDisabled;
	//}
}