using System;
using System.Collections;
using System.IO;
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

	void Calendar(string input)
	{
		VisualCalendar calendar = FindObjectOfType<VisualCalendar>();

		calendar.OpenYearTab(DateTime.Today);
	}

	void DataTimeTiest(string input)
	{
		print("Today: " + DateTime.Today);
		print("Created day: " + new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day));
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
		CreateTestItem("Create calendar", Calendar);
		CreateTestItem("Test dataTime", DataTimeTiest);
	}
	//
}