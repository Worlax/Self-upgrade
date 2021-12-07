using System.Collections.Generic;
using UnityEngine;

public class BackupItemConstructor : Singleton<BackupItemConstructor>
{
#pragma warning disable 0649

	[SerializeField] BackupItem itemPrefab;

#pragma warning restore 0649

	public List<BackupItem> CreateAllItems(Transform parent)
	{
		List<BackupItem> createdItems = new List<BackupItem>();

		foreach (AppData data in BackupFileSystem.GetAllBackups())
		{
			BackupItem item = Instantiate(itemPrefab, parent);
			item.Init(data);
			createdItems.Add(item);
		}

		return createdItems;
	}
}