using UnityEngine;

public class SettingsItemConstructor : Singleton<SettingsItemConstructor>
{
#pragma warning disable 0649

	[SerializeField] SettingsItem settingsItemPrefab;

#pragma warning restore 0649

	public SettingsItem CreateSettingsItem(Upgrade upgrade, RectTransform parent)
	{
		SettingsItem item = Instantiate(settingsItemPrefab, parent);
		item.Init(upgrade);

		return item;
	}
}