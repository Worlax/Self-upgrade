using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class TestWindow : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button closeButton;

	[SerializeField] RectTransform content;
	[SerializeField] TestItem testItemPrefab;

#pragma warning restore 0649

	public event Action OnWindowClosed;

	void CreateTestItem(string label, Action<string> callBack, bool doesHaveInput = false)
	{
		TestItem item = Instantiate(testItemPrefab, content);

		item.Init(label, doesHaveInput);
		item.OnButtonsPressed += callBack;
	}

	// Events
	void CloseWindow()
	{
		OnWindowClosed?.Invoke();
		Destroy(gameObject);
	}

	void SomeTesting(string input)
	{
		foreach (Upgrade upgrade in UpgradesList.Instance.GetActive())
		{
			FailedGoals fg = upgrade.Calendar.GetFailedGoals(DateTime.Today);

			if (fg != null)
			{
				foreach (Mission mission in fg.FailedMissions)
				{
					print("Mission in '" + upgrade.Name + "' failed: ");
					print("time start: " + mission.TimeStart.ToString("g") + " time end: " + mission.TimeEnd.ToString("g"));
					print("goal failed: " + mission.Goal);
				}
			}
		}
	}
	//

	// Unity
	private void OnEnable()
	{
		closeButton.onClick.AddListener(CloseWindow);
	}

	private void OnDisable()
	{
		closeButton.onClick.RemoveListener(CloseWindow);
	}

	private void Start()
	{
		CreateTestItem("XDD", SomeTesting, true);
	}
	//
}