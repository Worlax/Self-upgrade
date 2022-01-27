using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMinutesWindow : Window
{
#pragma warning disable 0649

	[SerializeField] bool subtruct;
	[SerializeField] InputField inputField;
	[SerializeField] Button add15Minutes;
	[SerializeField] Button subtruct15Minutes;

#pragma warning restore 0649

	void Add15Minutes()
	{
		int inputMinutes = GetInputMinutes();
		int newValue = inputMinutes + 15;
		inputField.text = newValue.ToString();
	}

	void Distruct15Minutes()
	{
		int inputMinutes = GetInputMinutes();
		int newValue = inputMinutes - 15;
		newValue = newValue < 0 ? 0 : newValue;
		inputField.text = newValue.ToString();
	}

	protected override bool Execute()
	{
		int inputMinutes = GetInputMinutes();
		inputMinutes = subtruct ? inputMinutes * -1 : inputMinutes;
		UpgradeList.Instance.GetActive()[0].Progress.ChangeProgressBy(DateTime.Today, inputMinutes * 60);

		return true;
	}

	int GetInputMinutes()
	{
		return Int32.TryParse(inputField.text, out int minutes) ? minutes : 0;
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		add15Minutes.onClick.AddListener(Add15Minutes);
		subtruct15Minutes.onClick.AddListener(Distruct15Minutes);
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		add15Minutes.onClick.RemoveListener(Add15Minutes);
		subtruct15Minutes.onClick.RemoveListener(Distruct15Minutes);
	}
}