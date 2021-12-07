using System;
using System.IO;
using UnityEngine;

public static class PathFinder
{
	// Directories
	public static string SaveDirectory()
	{
		string path = Application.dataPath + "/Save";
		Directory.CreateDirectory(path);
		return path;
	}

	public static string BackupDirectory()
	{
		string path = SaveDirectory() + "/Backup";
		Directory.CreateDirectory(path);
		return path;
	}

	// Files
	public static string SaveFile()
	{
		return SaveDirectory() + "/save.json";
	}

	public static string BackupFile(DateTime date)
	{
		return BackupDirectory() + "/" + date.Year + "." + date.Month + "." + date.Day + ".json";
	}
}