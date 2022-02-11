using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputLimit : MonoBehaviour, ISelectHandler, IDeselectHandler
{
#pragma warning disable 0649

	[SerializeField] InputField inputField;
	[SerializeField] [Min(0)] int min;
	[SerializeField] [Min(0)] int max;
	[SerializeField] FormatType formatType = FormatType.MaxZeros;


#pragma warning restore 0649

	public event Action<InputLimit> OnDigitLimitReached;
	bool changeInProcess;

	enum FormatType
	{
		None, OneZero, MaxZeros
	}

	public void Select()
	{
		inputField.Select();
	}

	public void Deselect()
	{
		if (!CurrentEventSystem.Instance.EventSystem.alreadySelecting)
		{
			CurrentEventSystem.Instance.EventSystem.SetSelectedGameObject(null);
		}
	}

	void Clamp(string value)
	{
		if (changeInProcess) return;
		changeInProcess = true;

		value = value.Replace("-", "");

		if (Int32.TryParse(value, out int intValue))
		{
			int clampedNumber = Mathf.Clamp(intValue, min, max);
			inputField.text = GetZerosBeforNumber(value) + clampedNumber.ToString();
		}
		else
		{
			inputField.text = "";
		}

		if (inputField.text.Length == max.ToString().Length)
		{
			OnDigitLimitReached?.Invoke(this);
		}

		changeInProcess = false;
	}

	string GetZerosBeforNumber(string value)
	{
		string zeros = "";

		foreach (char ch in value)
		{
			if (ch != '0') break;
			zeros += "0";
		}
		if (zeros.Length == value.Length && zeros.Length != 0)
		{
			zeros = zeros.Remove(0, 1);
		}

		return zeros;
	}

	void FormatInput()
	{
		switch (formatType)
		{
			case FormatType.OneZero:
				if (inputField.text.Length == 0)
				{
					inputField.text = "0";
				}
				break;

			case FormatType.MaxZeros:
				while (inputField.text.Length < max.ToString().Length)
				{
					inputField.text = "0" + inputField.text;
				}
				break;
		}
	}

	public void OnSelect(BaseEventData eventData)
	{
		inputField.text = "";
	}

	public void OnDeselect(BaseEventData eventData)
	{
		FormatInput();
	}

	// Unity
	private void OnEnable()
	{
		inputField.onValueChanged.AddListener(Clamp);
	}

	private void OnDisable()
	{
		inputField.onValueChanged.RemoveListener(Clamp);
	}
}