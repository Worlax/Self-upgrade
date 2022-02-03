using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadBackup : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform parent;
	[SerializeField] ToggleGroup toggleGroup;
	[SerializeField] Button load;

#pragma warning restore 0649

	void UpdateDisplay()
	{
		DeleteAllItems();

		List<BackupItem> items = BackupItemConstructor.Instance.CreateAllItems(parent);
		List<Toggle> toggles = items.Select(obj => obj.GetComponent<Toggle>()).ToList();
		toggles.ForEach(obj => obj.group = toggleGroup);
	}

	void DeleteAllItems()
	{
		foreach (Transform transform in parent)
		{
			Destroy(transform.gameObject);
		}
	}
	// Events
	void Load()
	{
		DateTime date = toggleGroup.GetFirstActiveToggle().GetComponent<BackupItem>().Data.dateCreated;
		GeneralFileSystem.LoadDataFromBackup(date);
	}

	// Unity
	private void OnEnable()
	{
		UpdateDisplay();

		load.onClick.AddListener(Load);
	}

	private void OnDisable()
	{
		load.onClick.RemoveListener(Load);
	}
}