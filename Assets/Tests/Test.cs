using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Test : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button openWindow;
	[SerializeField] Canvas windowsCanvas;
	[SerializeField] TestWindow windowPrefab;

#pragma warning restore 0649

	void openTestWindow()
	{
		TestWindow window = Instantiate(windowPrefab, windowsCanvas.transform);
		window.OnWindowClosed += WindowClosed;

		openWindow.interactable = false;
	}

	// Events
	void WindowClosed()
	{
		openWindow.interactable = true;
	}
	//

	private void OnEnable()
	{
		openWindow.onClick.AddListener(openTestWindow);
	}

	private void OnDisable()
	{
		openWindow.onClick.RemoveListener(openTestWindow);
	}
}