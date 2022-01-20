using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class GeneralFileSystem
{
	public static Action OnProgressSaved;
	public static Action<AppData> OnLoadingBegins;

	public static void SaveData()
	{
		if (Upgrade.AllUpgrades != null && Upgrade.AllUpgrades.Count > 0)
		{
			File.WriteAllText(PathFinder.SaveFile(), AppData.GetDataAsJson());

			OnProgressSaved?.Invoke();
		}
	}

	public static void LoadData()
	{
		Load(PathFinder.SaveFile());
	}

	public static void LoadDataFromBackup(DateTime dateCreated)
	{
		Load(PathFinder.BackupFile(dateCreated));
	}

	static void Load(string filePath)
	{
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			AppData data = JsonConvert.DeserializeObject<AppData>(json);

			OnLoadingBegins?.Invoke(data);
		}
	}
}