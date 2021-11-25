using UnityEngine;
using UnityEngine.UI;

public class DisappearingText : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text text;
	[SerializeField] CanvasGroup canvasGroup;
	[SerializeField] float speed;

#pragma warning restore 0649

	private void Update()
	{
		if (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= Time.deltaTime * speed;
		}
	}

	public void Play(string txt)
	{
		text.text = txt;

		canvasGroup.alpha = 1;
	}
}