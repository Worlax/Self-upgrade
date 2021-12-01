using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class JsonSerializer
{
	static string SaveDirectory { get => Application.dataPath + "//Save"; }
	static string SaveFile { get => Application.dataPath + "//Save//" + "save.json"; }

	static string SaveFileTest { get => Application.dataPath + "//Save//" + "save"; }

	public static Action OnProgressSaved;
	public static Action OnProgressLoaded;

	public static void SaveProgress()
	{
		if (Upgrade.AllUpgrades != null && Upgrade.AllUpgrades.Count > 0)
		{
			int saveFileIterator = 1;
			string saveFileName = SaveFileTest;
			while (File.Exists(saveFileName + ".json"))
			{
				saveFileName = SaveFileTest + saveFileIterator;
				++saveFileIterator;
			}

			string json = JsonConvert.SerializeObject(AppData.GetData(), Formatting.Indented);
			CreateSaveDirectory();
			File.WriteAllText(saveFileName + ".json", json);

			OnProgressSaved?.Invoke();

			//
			//Debug.Log("File #" + saveFileIterator + " was saved.");
			CheckCorruption(json, true);
		}
	}

	public static void LoadProgress()
	{
		if (File.Exists(SaveFile))
		{
			string lastValidSaveFile = SaveFileTest;
			string currentlyTestedSaveFile = SaveFileTest;
			int saveFileIterator = 1;

			while (File.Exists(currentlyTestedSaveFile + ".json"))
			{
				lastValidSaveFile = currentlyTestedSaveFile;
				currentlyTestedSaveFile = SaveFileTest + saveFileIterator;
				++saveFileIterator;
			}

			string json = File.ReadAllText(lastValidSaveFile + ".json");
			AppData appData = JsonConvert.DeserializeObject<AppData>(json);
			AppData.LoadData(appData);

			OnProgressLoaded?.Invoke();

			//Debug.Log("File #" + saveFileIterator + " was loaded");
			CheckCorruption(json, false);
		}
		else
		{
			Debug.LogError("No load file found");
		}
	}

	static void OldSsave()
	{
		string json = JsonConvert.SerializeObject(AppData.GetData(), Formatting.Indented);
		CreateSaveDirectory();
		File.WriteAllText(SaveFile, json);

		OnProgressSaved?.Invoke();
	}

	static void OldLoad()
	{
		if (File.Exists(SaveFile))
		{
			string json = File.ReadAllText(SaveFile);
			AppData appData = JsonConvert.DeserializeObject<AppData>(json);
			AppData.LoadData(appData);

			OnProgressLoaded?.Invoke();

			Debug.Log("Loaded");
			CheckCorruption(json, false);
		}
	}

	static void CreateSaveDirectory()
	{
		Directory.CreateDirectory(SaveDirectory);
	}

	static void CheckCorruption(string json, bool save)
	{
		string corruptedFile = File.ReadAllText(Application.dataPath + "//Save//" + "corruption.json");

		if (corruptedFile == json)
		{
			if (save)
			{
				Debug.LogError("Save was corrupted");
			}
			else
			{
				Debug.LogError("Load was corrupted");
			}
		}
	}
}