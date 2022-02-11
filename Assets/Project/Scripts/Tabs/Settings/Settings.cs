using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button newUpgrade;
	[SerializeField] RectTransform content;
	[SerializeField] ToggleGroup toggleGroup;

#pragma warning restore 0649

	public static event Action<SettingsItem> OnItemDisplayed;

	void UpdateContent()
	{
		ClearContent();
		FillContent();
	}

	void FillContent()
	{
		foreach (Upgrade upgrade in Upgrade.AllUpgrades)
		{
			SettingsItem item = SettingsItemConstructor.Instance.CreateSettingsItem(upgrade, content);
			item.GetComponentInChildren<Toggle>().group = toggleGroup;
			OnItemDisplayed?.Invoke(item);
		}
	}

	void ClearContent()
	{
		foreach (Transform item in content)
		{
			Destroy(item.gameObject);
		}
	}

	// Events
	void NewUpgrade()
	{
		WindowManager.Instance.CreateNewUpgradeWindow();
	}

	void NewUpgradeCreated(Upgrade upgrade)
	{
		UpdateContent();
	}

	void UpgradeDeleted(string name)
	{
		UpdateContent();
	}

	// Unity
	private void OnEnable()
	{
		UpdateContent();

		Upgrade.OnNewUpgradeCreated += NewUpgradeCreated;
		Upgrade.OnUpgradeDeleted += UpgradeDeleted;

		newUpgrade.onClick.AddListener(NewUpgrade);
	}

	private void OnDisable()
	{
		Upgrade.OnNewUpgradeCreated -= NewUpgradeCreated;
		Upgrade.OnUpgradeDeleted -= UpgradeDeleted;

		newUpgrade.onClick.RemoveListener(NewUpgrade);
	}
}