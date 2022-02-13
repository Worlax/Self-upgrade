using UnityEngine;
using UnityEngine.UI;

public class TTest : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] InputField inputField;
	[SerializeField] int qq;

#pragma warning restore 0649

	TouchScreenKeyboard.Status prevStatus;

	private void Start()
	{
		if (inputField.touchScreenKeyboard != null)
		prevStatus = inputField.touchScreenKeyboard.status;
	}

	private void Update()
	{
		if (inputField.touchScreenKeyboard != null && prevStatus != inputField.touchScreenKeyboard.status)
		{
			Log.Instance.Print(typeof(TTest), "Keyboard now changed to: " + inputField.touchScreenKeyboard.status.ToString());
			prevStatus = inputField.touchScreenKeyboard.status;
		}
	}
}