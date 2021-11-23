using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Test : MonoBehaviour
{
	[SerializeField] Button button1;
	[SerializeField] Button button2;
	[SerializeField] Button button3;
	[SerializeField] Button button4;

	void Button1()
	{
		print("Save");

		JsonSerializer.SaveProgress();
	}

	void Button2()
	{
		print("Load");

		JsonSerializer.LoadProgress();
	}

	void Button3()
	{
		print("Hui created!");

		Upgrade.NewUpgrade("Hui", UpgradeType.Timer);
	}

	void Button4()
	{
		print("Pizda created!");

		Upgrade.NewUpgrade("Pizda", UpgradeType.Timer);
	}

	private void OnEnable()
	{
		button1.onClick.AddListener(Button1);
		button2.onClick.AddListener(Button2);
		button3.onClick.AddListener(Button3);
		button4.onClick.AddListener(Button4);
	}

	private void OnDisable()
	{
		button1.onClick.RemoveListener(Button1);
		button2.onClick.RemoveListener(Button2);
		button3.onClick.RemoveListener(Button3);
		button4.onClick.RemoveListener(Button4);
	}
}