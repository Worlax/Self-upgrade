using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainInput : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] List<InputLimit> inputLimits;

#pragma warning restore 0649

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			IterateChain();
		}
	}

	void IterateChain()
	{
		GameObject selectedObject = CurrentEventSystem.Instance.EventSystem.currentSelectedGameObject;

		if (selectedObject != null)
		{
			InputLimit inputLimit = selectedObject.GetComponent<InputLimit>();

			if (inputLimit != null && inputLimits.Contains(inputLimit))
			{
				IterateChain(inputLimit);
			}
		}
	}

	void IterateChain(InputLimit inputLimit)
	{
		InputLimit nextChain = GetNextChain(inputLimit);

		if (nextChain != null)
		{
			nextChain.Select();
		}
		else
		{
			inputLimit.Deselect();
		}
	}

	InputLimit GetNextChain(InputLimit inputLimit)
	{
		int currentIndex = inputLimits.IndexOf(inputLimit);

		return inputLimits.Count > currentIndex + 1 ? inputLimits[currentIndex + 1] : null;
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