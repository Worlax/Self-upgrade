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
		Log.Instance.Print(typeof(GeneralFileSystem), "LoadData.");
		Load(PathFinder.SaveFile());
	}

	public static void LoadDataFromBackup(DateTime dateCreated)
	{
		Load(PathFinder.BackupFile(dateCreated));
	}

	static void Load(string filePath)
	{
		Log.Instance.Print(typeof(GeneralFileSystem), "Load.");
		Log.Instance.Print(typeof(GeneralFileSystem), "File path: " + filePath);
		if (File.Exists(filePath))
		{
			Log.Instance.Print(typeof(GeneralFileSystem), "File exists!");
			string json = File.ReadAllText(filePath);
			AppData data = JsonConvert.DeserializeObject<AppData>(json);

			OnLoadingBegins?.Invoke(data);
		}
		else
		{
			Log.Instance.Print(typeof(GeneralFileSystem), "File not found.");
		}
	}
}