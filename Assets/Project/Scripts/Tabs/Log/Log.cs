using System;
using UnityEngine;
using UnityEngine.UI;

public class Log : Singleton<Log>
{
#pragma warning disable 0649

	[SerializeField] Toggle show;
	[SerializeField] RectTransform scrollView;
	[SerializeField] RectTransform content;
	[SerializeField] RectTransform textPrefab;

#pragma warning restore 0649

	string classColorOpen = "<color=#00ffffff>";
	string classColorClosed = "</color>";

	string stackTraceColorOpen = "<color=#6A6A6A>";
	string stackTraceColorClosed = "</color>";

	public void Print(Type senderClass, string message)
	{
		RectTransform text = Instantiate(textPrefab, content);
		string formattedClass = classColorOpen + senderClass.Name + ": " + classColorClosed;

		text.GetComponentInChildren<Text>().text = formattedClass + message;
	}

	// Event
	void Show(bool value)
	{
		scrollView.gameObject.SetActive(value);
	}

	void logMessageReceived(string condition, string stackTrace, LogType type)
	{
		string formattedStackTrace = stackTraceColorOpen + stackTrace + stackTraceColorClosed;
		string message = condition + "\n" + formattedStackTrace;

		Print(typeof(Log), message);
	}

	// Unity
	private void OnEnable()
	{
		Application.logMessageReceived += logMessageReceived;
		show.onValueChanged.AddListener(Show);
	}

	private void OnDisable()
	{
		Application.logMessageReceived -= logMessageReceived;
		show.onValueChanged.AddListener(Show);
	}
}