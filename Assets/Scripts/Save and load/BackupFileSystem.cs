using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class BackupFileSystem
{
	// Should not be less than 1
	const int MAX_BACKUPS = 5;

	public static List<AppData> GetAllBackups()
	{
		List<AppData> backups = new List<AppData>();

		foreach (string fileName in GetAllBackupFileNames())
		{
			string json = File.ReadAllText(fileName);
			AppData data = JsonConvert.DeserializeObject<AppData>(json);

			backups.Add(data);
		}

		return backups;
	}

	static void SaveData()
	{
		if (Upgrade.AllUpgrades != null && Upgrade.AllUpgrades.Count > 0)
		{
			FreeSpaceForBackupIfNeeded();

			File.WriteAllText(PathFinder.BackupFile(DateTime.Today), AppData.GetDataAsJson());
		}
	}
	static bool TodayBackupExists()
	{
		return File.Exists(PathFinder.BackupFile(DateTime.Today));
	}

	static void FreeSpaceForBackupIfNeeded()
	{
		List<AppData> backups = GetAllBackups();
		backups.Sort((obj1, obj2) => obj1.dateCreated.CompareTo(obj2.dateCreated));
		int backupsToDelete = (backups.Count + 1) - MAX_BACKUPS;

		for (int i = 0; i < backupsToDelete; ++i)
		{
			DateTime date = backups[i].dateCreated;
			File.Delete(PathFinder.BackupFile(date));
			File.Delete(PathFinder.BackupFile(date) + ".meta");
		}
	}

	static List<string> GetAllBackupFileNames()
	{
		List<string> allFiles = Directory.GetFiles(PathFinder.BackupDirectory()).ToList();
		List<string> jsonFiles = allFiles.Where(obj => Path.GetExtension(obj) == ".json").ToList();

		return jsonFiles;
	}

	// Events
	public static void AppLoaded()
	{
		if (!TodayBackupExists())
		{
			SaveData();
		}
	}
}