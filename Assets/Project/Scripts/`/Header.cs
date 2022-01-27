using UnityEngine;
using UnityEngine.UI;

public class Header : Singleton<Header>
{
#pragma warning disable 0649

	[SerializeField] Text headerLabelMain;
	[SerializeField] Text headerLabelSecond;

#pragma warning restore 0649

	public void ChangeHeader(string main, string second)
	{
		headerLabelMain.text = main;
		headerLabelSecond.text = second;

		bool secondHeaderHaveContent = headerLabelSecond.text != "";
		headerLabelSecond.gameObject.SetActive(secondHeaderHaveContent);
	}
}