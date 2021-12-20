using System;
using UnityEngine;
using UnityEngine.UI;

public class BackupItem : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Text text;

#pragma warning restore 0649

	public AppData Data { get; private set; }

	public void Init(AppData data)
	{
		Data = data;
		text.text = data.dateCreated.ToString("yyyy MMM dd") + " | Time: " + TimeConverter.TimeString(data.totalSeconds);
	}
}