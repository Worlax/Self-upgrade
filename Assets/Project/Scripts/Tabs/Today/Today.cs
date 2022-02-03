using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Today : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text dateText;

	[SerializeField] RectTransform upgradeContent;
	[SerializeField] RectTransform timePrefab;

#pragma warning restore 0649

	//public bool ShowBreakTime = true;

	//void UpdateDisplay()
	//{
	//	ClearContent();
	//	int timerPosition = 0 + upgradeContent.childCount;

	//	dateText.text = DateTime.Today.ToString("MMMM dd, dddd");
	//	List<ScheduleItem> createdItems = ScheduleItemConstructor.Instance.CreateItemsForDayOfTheWeek(DateTime.Today.DayOfWeek, ShowBreakTime, upgradeContent);
	//	//createdItems.ForEach(obj => obj.OnClicked += OnItemClicled);

	//	// All passed missions will be gray out and timer position will be right after them
	//	// (plus child count, because object destruction will happend only on the end of the frame)
	//	foreach (ScheduleItem item in createdItems)
	//	{
	//		if (item.Upgrade.Type == UpgradeType.Timer)
	//		{
	//			if (item.Mission.TimeEnd < DateTime.Now.TimeOfDay)
	//			{
	//				item.GrayOut(true);
	//				++timerPosition;
	//			}
	//		}
	//		else
	//		{
	//			++timerPosition;
	//		}	
	//	}
	//	CreateTimeItem(createdItems, timerPosition);
	//}

	//void CreateTimeItem(List<ScheduleItem> createdItems, int position)
	//{
	//	TimeSpan time = DateTime.Now.TimeOfDay;
	//	string timeText = time.Hours.ToString() + "h " + time.Minutes.ToString() + "m";

	//	RectTransform timeUI = Instantiate(timePrefab, upgradeContent);
	//	timeUI.GetComponentInChildren<Text>().text = timeText;
	//	timeUI.SetSiblingIndex(position);
	//}

	//void ClearContent()
	//{
	//	foreach (Transform transform in upgradeContent)
	//	{
	//		Destroy(transform.gameObject);
	//	}
	//}

	//// Events
	//void OnItemClicled(ScheduleItem item)
	//{
	//	if (item.Upgrade.Type == UpgradeType.Checker)
	//	{
	//		item.Upgrade.Progress.ChangeProgressBy(DateTime.Today, 1);
	//	}
	//	if (item.Upgrade.Type == UpgradeType.MultiChecker)
	//	{
	//		item.Upgrade.Progress.ChangeProgressBy(DateTime.Today, 10);
	//	}
	//	if (item.Upgrade.Type == UpgradeType.Timer)
	//	{
	//		UpgradeList.Instance.SetActive(item.Upgrade);
	//		Menu.GetMenu("Upgrade").OpenMenu();
	//	}
	//}

	//// Unity
	//private void OnEnable()
	//{
	//	UpdateDisplay();

	//	TimeEvents.OnOneMinutePassed += UpdateDisplay;
	//}

	//private void OnDisable()
	//{
	//	ClearContent();

	//	TimeEvents.OnOneMinutePassed -= UpdateDisplay;
	//}
}