using System;
using UnityEngine;
using UnityEngine.UI;

public class TestItem : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text label;
	[SerializeField] Button button;
	[SerializeField] InputField inputField;

#pragma warning restore 0649

	public event Action<string> OnButtonsPressed;

	public void Init(string label, bool doesHaveInput)
	{
		this.label.text = label;

		if (!doesHaveInput)
		{
			Destroy(inputField.gameObject);
		}
	}

	public void ButtonPressed()
	{
		string input = "";

		if (inputField != null)
		{
			input = inputField.text;
		}

		OnButtonsPressed?.Invoke(input);
	}

	private void OnEnable()
	{
		button.onClick.AddListener(ButtonPressed);
	}

	private void OnDisable()
	{
		button.onClick.RemoveListener(ButtonPressed);
	}
}