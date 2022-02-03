using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsToolbar : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform content;
	[SerializeField] Button edit;
	[SerializeField] Button delete;

#pragma warning restore 0649

	List<SettingsItem> toggledItems = new List<SettingsItem>();

	void Show(bool value)
	{
		content.gameObject.SetActive(value);
	}

	// Events
	void ItemDisplayed(SettingsItem item)
	{
		item.OnToggleChanged += ItemToggleChanged;
	}

	void ItemToggleChanged(SettingsItem item, bool value)
	{
		if (value == true)
		{
			toggledItems.Add(item);
		}
		else
		{
			toggledItems.Remove(item);
		}

		Show(toggledItems.Count > 0);
	}

	void Edit()
	{

	}

	void Delete()
	{
		foreach (SettingsItem item in toggledItems.ToArray())
		{
			Upgrade.DeleteUpgrade(item.Upgrade.Name);
			toggledItems.Remove(item);
		}

		Show(toggledItems.Count > 0);
	}

	// Unity
	private void OnEnable()
	{
		Settings.OnItemDisplayed += ItemDisplayed;
		edit.onClick.AddListener(Edit);
		delete.onClick.AddListener(Delete);
	}

	private void OnDisable()
	{
		Settings.OnItemDisplayed -= ItemDisplayed;
		edit.onClick.RemoveListener(Edit);
		delete.onClick.RemoveListener(Delete);
	}
}