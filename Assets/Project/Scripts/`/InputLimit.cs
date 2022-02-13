using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputLimit : MonoBehaviour, ISelectHandler
{
#pragma warning disable 0649

	[SerializeField] InputField inputField;
	[SerializeField] InputLimit nextChain;
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

	public void Activate() => inputField.ActivateInputField();
	public void Deactivate() => inputField.DeactivateInputField();

	void FormatOnSelect()
	{
		inputField.SetTextWithoutNotify("");
	}

	void FormatEditEnd()
	{
		string formattedText = inputField.text;

		switch (formatType)
		{
			case FormatType.OneZero:
				if (inputField.text.Length == 0)
				{
					formattedText = "0";
				}
				break;

			case FormatType.MaxZeros:
				formattedText = FillZerosToReachMaxLength(formattedText, max.ToString().Length);
				break;
		}

		inputField.SetTextWithoutNotify(formattedText);
	}

	void FormatOnEdit(string value)
	{
		value = value.Replace("-", "");

		if (Int32.TryParse(value, out int intValue))
		{
			int clampedNumber = Mathf.Clamp(intValue, min, max);
			value = GetZerosBeforNumber(value) + clampedNumber.ToString();
			
		}
		else
		{
			value = "";
		}

		inputField.SetTextWithoutNotify(value);

		if (inputField.text.Length == max.ToString().Length)
		{
			OnDigitLimitReached?.Invoke(this);
		}
	}

	string FillZerosToReachMaxLength(string text, int maxLength)
	{
		while (text.Length < maxLength)
		{
			text = "0" + text;
		}

		return text;
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

	void IterateChain()
	{
		if (nextChain != null)
		{
			if (inputField.touchScreenKeyboard != null && inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done)
			{
				nextChain.Activate();
			}
		}
	}

	// Event
	public void OnSelect(BaseEventData eventData) => FormatOnSelect();
	void EditEnd(string call)
	{
		FormatEditEnd();
		IterateChain();
	}

	// Unity
	private void OnEnable()
	{
		inputField.onValueChanged.AddListener(FormatOnEdit);
		inputField.onEndEdit.AddListener(EditEnd);
	}

	private void OnDisable()
	{
		inputField.onValueChanged.RemoveListener(FormatOnEdit);
		inputField.onEndEdit.RemoveListener(EditEnd);
	}
}