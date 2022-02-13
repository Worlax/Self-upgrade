using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChainInput : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] List<InputLimit> inputLimits;

#pragma warning restore 0649

	void IterateChain()
	{
		InputLimit focusedInput = GetActiveElement();

		if (focusedInput != null)
		{
			IterateChain(focusedInput);
		}
	}

	InputLimit GetActiveElement()
	{
		GameObject selectedObject = CurrentEventSystem.Instance.EventSystem.currentSelectedGameObject;

		if (selectedObject != null)
		{
			InputLimit selectedElement = selectedObject.GetComponent<InputLimit>();

			if (selectedElement != null && inputLimits.Contains(selectedElement))
			{
				return selectedElement;
			}
		}

		return null;
	}

	void IterateChain(InputLimit inputLimit)
	{
		InputLimit nextChain = GetNextChain(inputLimit);

		if (nextChain != null)
		{
			nextChain.Activate();
		}
		else
		{
			inputLimit.Deactivate();
		}
	}

	InputLimit GetNextChain(InputLimit inputLimit)
	{
		int currentIndex = inputLimits.IndexOf(inputLimit);

		return inputLimits.Count > currentIndex + 1 ? inputLimits[currentIndex + 1] : null;
	}

	// Unity
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			IterateChain();
		}
	}

	private void OnEnable()
	{
		inputLimits.ForEach(obj => obj.OnDigitLimitReached += IterateChain);
	}

	private void OnDisable()
	{
		inputLimits.ForEach(obj => obj.OnDigitLimitReached -= IterateChain);
	}
}