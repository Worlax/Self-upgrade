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

	private void Start()
	{
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