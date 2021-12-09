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

	public bool ShowBreakTime = true;

	void FillContent()
	{
		ClearContent();

		dateText.text = DateTime.Today.ToString("MMMM dd, dddd");
		List<ScheduleItem> createdItems = ScheduleItemConstructor.Instance.CreateItemsForDayOfTheWeek(DateTime.Today.DayOfWeek, ShowBreakTime, upgradeContent);
		createdItems.ForEach(obj => obj.OnClicked += OnItemClicled);

		CreateTimeItem(createdItems);
	}

	void CreateTimeItem(List<ScheduleItem> createdItems)
	{
		TimeSpan time = DateTime.Now.TimeOfDay;
		string timeText = time.Hours.ToString() + "h " + time.Minutes.ToString() + "m";
		int sublingIndex = -1;

		for (int i = 0; i < createdItems.Count; ++i)
		{
			if (createdItems[i].Upgrade.Type == UpgradeType.Timer)
			{
				if (createdItems[i].Mission.TimeStart + new TimeSpan(0, 0, createdItems[i].Mission.Goal) > time)
				{
					sublingIndex = i;
					break;
				}
				else
				{
					createdItems[i].GrayOut(true);
				}
			}
		}

		if (sublingIndex < 0)
		{
			sublingIndex = createdItems.Count;
		}

		RectTransform timeUI = Instantiate(timePrefab, upgradeContent);
		timeUI.GetComponentInChildren<Text>().text = timeText;
		timeUI.SetSiblingIndex(sublingIndex);
	}

	void ClearContent()
	{
		foreach (Transform transform in upgradeContent)
		{
			Destroy(transform.gameObject);
		}
	}

	// Events
	void OnItemClicled(ScheduleItem item)
	{
		if (item.Upgrade.Type == UpgradeType.Checker)
		{
			item.Upgrade.Calendar.ChangeValueBy(DateTime.Today, 1);
		}
		if (item.Upgrade.Type == UpgradeType.MultiChecker)
		{
			item.Upgrade.Calendar.ChangeValueBy(DateTime.Today, 10);
		}
		if (item.Upgrade.Type == UpgradeType.Timer)
		{
			UpgradesList.Instance.SetActive(item.Upgrade);
			Menu.GetMenu("Upgrade").OpenMenu();
		}
	}

	// Unity
	private void OnEnable()
	{
		FillContent();
	}

	private void OnDisable()
	{
		ClearContent();
	}
}