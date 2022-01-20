using UnityEngine;
using UnityEngine.UI;

public class Preload : Singleton<Preload>
{
	protected void Start()
	{
        // Load data
        //GeneralFileSystem.LoadData();

        //// Create empty Upgrade if there is no upgrades
        //if (Upgrade.AllUpgrades.Count == 0)
        //{
        //	Upgrade.CreateUpgrade("Autocreated", UpgradeType.Timer);
        //}

        //// Update dropdown display
        //UpgradesList.Instance.UpdateDisplay();


        //Color color = new Color(0.1098039f, 0.1019608f, 0.1254902f);
        //Screen.fullScreen = false;

        //
        //ApplicationChrome.statusBarColor = ApplicationChrome.navigationBarColor = 0xffff3300;
        //Screen.fullScreen = false;
    }
}