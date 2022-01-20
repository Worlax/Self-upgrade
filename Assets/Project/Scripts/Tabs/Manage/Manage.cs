using UnityEngine;
using UnityEngine.UI;

public class Manage : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button newUpgrade;
	[SerializeField] Button deleteUpgrade;
	[SerializeField] Button loadBackup;

	[SerializeField] RectTransform mainTab;
	[SerializeField] RectTransform newUpgradeTab;
	[SerializeField] RectTransform deleteUpgradeTab;
	[SerializeField] RectTransform loadBackupTab;

	[SerializeField] Button back;

#pragma warning restore 0649


	void DisableAllTabsButOne(RectTransform tab)
	{
		if (newUpgradeTab != tab) newUpgradeTab.gameObject.SetActive(false);
		if (deleteUpgradeTab != tab) deleteUpgradeTab.gameObject.SetActive(false);
		if (loadBackupTab != tab) loadBackupTab.gameObject.SetActive(false);
		if (mainTab != tab) mainTab.gameObject.SetActive(false);

		tab.gameObject.SetActive(true);
	}

	// Events
	void NewUpgrade()
	{
		back.interactable = true;
		DisableAllTabsButOne(newUpgradeTab);
	}

	void DeleteUpgrade()
	{
		back.interactable = true;
		DisableAllTabsButOne(deleteUpgradeTab);
	}

	void LoadBackup()
	{
		back.interactable = true;
		DisableAllTabsButOne(loadBackupTab);
	}

	void Back()
	{
		DisableAllTabsButOne(mainTab);
		back.interactable = false;
	}

	// Unity
	private void OnEnable()
	{
		newUpgrade.onClick.AddListener(NewUpgrade);
		deleteUpgrade.onClick.AddListener(DeleteUpgrade);
		loadBackup.onClick.AddListener(LoadBackup);

		back.onClick.AddListener(Back);
	}

	private void OnDisable()
	{
		newUpgrade.onClick.RemoveListener(NewUpgrade);
		deleteUpgrade.onClick.RemoveListener(DeleteUpgrade);
		loadBackup.onClick.RemoveListener(LoadBackup);

		back.onClick.RemoveListener(Back);
	}
}