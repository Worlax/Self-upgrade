using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] string headerName;
	[SerializeField] string headerNameSecond;

	[SerializeField] bool allGlobal;
	[SerializeField] bool allTimers;
	[SerializeField] bool timers;
	[SerializeField] bool allCheckers;
	[SerializeField] bool checkers;
	[SerializeField] bool allMultiCheckers;
	[SerializeField] bool multiCheckers;

#pragma warning restore 0649

	HashSet<UpgradeType> GetDefinedTypes()
	{
		HashSet<UpgradeType> definedTypes = new HashSet<UpgradeType>();

		if (timers)
		{
			definedTypes.Add(UpgradeType.Timer);
		}
		if (checkers)
		{
			definedTypes.Add(UpgradeType.Checker);
		}
		if (multiCheckers)
		{
			definedTypes.Add(UpgradeType.MultiChecker);
		}

		return definedTypes;
	}

	HashSet<UpgradeType> GetDefinedTypesAsAll()
	{
		HashSet<UpgradeType> definedTypesAsAll = new HashSet<UpgradeType>();

		if (allTimers)
		{
			definedTypesAsAll.Add(UpgradeType.Timer);
		}
		if (allCheckers)
		{
			definedTypesAsAll.Add(UpgradeType.Checker);
		}
		if (allMultiCheckers)
		{
			definedTypesAsAll.Add(UpgradeType.MultiChecker);
		}

		return definedTypesAsAll;
	}

	void UpdateHeader()
	{
		if (headerName != "")
		{
			Header.Instance.ChangeHeader(headerName, headerNameSecond);
		}
	}

	// Unity
	private void OnEnable()
	{
		UpgradeList.Instance.SetAvaibleTypes(GetDefinedTypes(), GetDefinedTypesAsAll(), allGlobal);
		UpdateHeader();
	}
}