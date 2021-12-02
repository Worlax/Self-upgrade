using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class JsonSerializer
{
	static string SaveDirectory { get => Application.dataPath + "//Save"; }
	static string SaveFile { get => Application.dataPath + "//Save//" + "save.json"; }

	public static Action OnProgressSaved;
	public static Action<AppData> OnLoadingBegins;

	public static void SaveData()
	{
		if (Upgrade.AllUpgrades != null && Upgrade.AllUpgrades.Count > 0)
		{
			string json = JsonConvert.SerializeObject(GetData(), Formatting.Indented);
			CreateSaveDirectory();
			File.WriteAllText(SaveFile, json);

			OnProgressSaved?.Invoke();
		}
	}

	static AppData GetData()
	{
		return new AppData()
		{
			Upgrades = Upgrade.AllUpgrades
		};
	}

	public static void LoadData()
	{
		if (File.Exists(SaveFile))
		{
			string json = File.ReadAllText(SaveFile);
			AppData data = JsonConvert.DeserializeObject<AppData>(json);

			OnLoadingBegins?.Invoke(data);
		}
	}

	static void CreateSaveDirectory()
	{
		Directory.CreateDirectory(SaveDirectory);
	}
}