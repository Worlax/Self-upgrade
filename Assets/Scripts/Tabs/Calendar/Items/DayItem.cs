using System;
using UnityEngine;
using UnityEngine.UI;

public class DayItem : MonoBehaviour, IHighlightable
{
#pragma warning disable 0649

	[SerializeField] Text dayText;
	[SerializeField] Image image;
	[SerializeField] Color baseColor;
	[SerializeField] Color highlightColor;

#pragma warning restore 0649

	DateTime date;

	public void Init(DateTime date)
	{
		this.date = date;

		dayText.text = date.Day.ToString();
		Highlight(date == DateTime.Today);
	}

	public void Highlight(bool value)
	{
		if (value)
		{
			image.color = highlightColor;
		}
		else
		{
			image.color = baseColor;
		}
	}
}