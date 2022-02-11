using System;
using System.IO;
using UnityEngine;

public static class PathFinder
{
	// Directories
	public static string SaveDirectory()
	{
		Log.Instance.Print(typeof(PathFinder), "SaveDirectory");
		string path = Application.dataPath + "/Save";
		string path2 = Application.persistentDataPath + "/Save";
		Log.Instance.Print(typeof(PathFinder), "Path: " + path);
		Log.Instance.Print(typeof(PathFinder), "Path2: " + path2);
		if (!Directory.Exists(path2)) Directory.CreateDirectory(path2);
		Log.Instance.Print(typeof(PathFinder), "After creating directory");
		return path2;
	}

	public static string BackupDirectory()
	{
		string path = SaveDirectory() + "/Backup";
		if (!Directory.Exists(path)) Directory.CreateDirectory(path);
		return path;
	}

	// Files
	public static string SaveFile()
	{
		Log.Instance.Print(typeof(PathFinder), "SaveFile");
		return SaveDirectory() + "/save.json";
	}

	public static string BackupFile(DateTime date)
	{
		return BackupDirectory() + "/" + date.Year + "." + date.Month + "." + date.Day + ".json";
	}
}