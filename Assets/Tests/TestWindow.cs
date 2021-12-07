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
		TimeSpan time = new TimeSpan(0, 0, Int32.Parse(input));

		print(time.Days * 24 + time.Hours + ":" + time.Minutes + ":" + time.Seconds);
		//print(TimeConverter.TimeString(Int32.Parse(input)));
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