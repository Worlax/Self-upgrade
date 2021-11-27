using System;
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

	private void Awake()
	{
		CreateTestItem("Create calendar", Calendar);
	}

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
		FindObjectOfType<VisualCalendar>().ShowYearTab(DateTime.Today);
	}
	//

	private void OnEnable()
	{
		closeButton.onClick.AddListener(CloseWindow);
	}

	private void OnDisable()
	{
		closeButton.onClick.RemoveListener(CloseWindow);
	}
}