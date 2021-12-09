using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button open;
	[SerializeField] RectTransform content;
	[SerializeField] bool srartingMenu;

#pragma warning restore 0649

	static Menu activeMenu;
	static List<Menu> allMenus = new List<Menu>();

	public void OpenMenu()
	{
		if (activeMenu != this)
		{
			if (activeMenu != null)
			{
				activeMenu.CloseMenu();
			}

			activeMenu = this;
			content.gameObject.SetActive(true);
		}
	}

	void CloseMenu()
	{
		content.gameObject.SetActive(false);
	}

	public static Menu GetMenu(string name)
	{
		return allMenus.Find(obj => obj.gameObject.name == name);
	}

	// Unity
	private void Start()
	{
		allMenus.Add(this);

		if (srartingMenu)
		{
			OpenMenu();
		}
		else
		{
			CloseMenu();
		}
	}

	private void OnEnable()
	{
		open.onClick.AddListener(OpenMenu);
	}

	private void OnDisable()
	{
		open.onClick.RemoveListener(OpenMenu);
	}
}