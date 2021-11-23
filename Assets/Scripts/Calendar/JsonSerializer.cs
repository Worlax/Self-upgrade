using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class JsonSerializer
{
	static string SaveDirectory { get => Application.dataPath + "//Save"; }
	static string SaveFile { get => Application.dataPath + "//Save//" + "save.json"; }

	public static Action OnProgressSaved;
	public static Action OnProgressLoaded;

	public static void SaveProgress()
	{
		string json = JsonConvert.SerializeObject(AppData.GetData(), Formatting.Indented);
		CreateSaveDirectory();
		File.WriteAllText(SaveFile, json);

		OnProgressSaved?.Invoke();
	}

	public static void LoadProgress()
	{
		Debug.Log("loaded");
		string json = File.ReadAllText(SaveFile);
		AppData appData = JsonConvert.DeserializeObject<AppData>(json);
		AppData.LoadData(appData);

		OnProgressLoaded?.Invoke();
	}

	static void CreateSaveDirectory()
	{
		Directory.CreateDirectory(SaveDirectory);
	}
}