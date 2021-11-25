using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateUpgrade : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] new InputField name;
	[SerializeField] Dropdown dropdownTypes;
	[SerializeField] Button create;

#pragma warning restore 0649

	void Create()
	{
		Upgrade.CreateUpgrade(name.text, GetSelectedType());
	}

	UpgradeType GetSelectedType()
	{
		foreach (UpgradeType type in Enum.GetValues(typeof(UpgradeType)))
		{
			if (type.ToString() == dropdownTypes.options[dropdownTypes.value].text)
			{
				return type;
			}
		}

		return UpgradeType.Checker;
	}

	void UpdateTypesDropdownVisual()
	{
		List<Dropdown.OptionData> newOptions = new List<Dropdown.OptionData>();

		foreach (UpgradeType type in Enum.GetValues(typeof(UpgradeType)))
		{
			Dropdown.OptionData option = new Dropdown.OptionData(type.ToString());
			newOptions.Add(option);
		}

		dropdownTypes.options = newOptions;
	}

	private void Start()
	{
		UpdateTypesDropdownVisual();
	}

	private void OnEnable()
	{
		create.onClick.AddListener(Create);
	}

	private void OnDisable()
	{
		create.onClick.RemoveListener(Create);
	}
}