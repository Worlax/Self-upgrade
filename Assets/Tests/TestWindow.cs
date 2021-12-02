using Newtonsoft.Json;
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
		UICalendar calendar = FindObjectOfType<UICalendar>();

		calendar.OpenYearTab(DateTime.Today);
	}

	void DataTimeTiest(string input)
	{
		DateTime date1 = DateTime.Today;
		DateTime date2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
		DateTime date3 = date1.Date;
		DateTime date4 = new DateTime(date1.Ticks);
		DateTime date5 = new DateTime(date3.Ticks);
		DateTime date6 = DateTime.Now;
		DateTime date7 = new DateTime(date6.Year, date6.Month, date6.Day);
		DateTime date8 = date6.Date;

		print("1: " + date1);
		print("2: " + date2);
		print("3: " + date3);
		print("4: " + date4);
		print("5: " + date5);
		print("6: " + date6);
		print("7: " + date7);
		print("8: " + date8);

		//

		Serialize(date1, "date1.json");
		Serialize(date2, "date2.json");
		Serialize(date3, "date3.json");
		Serialize(date4, "date4.json");
		Serialize(date5, "date5.json");
		Serialize(date6, "date6.json");
		Serialize(date7, "date7.json");
		Serialize(date8, "date8.json");
	}

	void Serialize(DateTime date, string name)
	{
		string json = JsonConvert.SerializeObject(date);
		File.WriteAllText(Application.dataPath + "//SaveTest//" + name, json);
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