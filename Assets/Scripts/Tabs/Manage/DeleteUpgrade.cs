using UnityEngine;
using UnityEngine.UI;

public class DeleteUpgrade : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] InputField nameInput;
	[SerializeField] Button delete;

#pragma warning restore 0649

	void Delte()
	{
		Upgrade.DeleteUpgrade(nameInput.text);
		nameInput.text = "";
	}

	private void OnEnable()
	{
		delete.onClick.AddListener(Delte);
	}

	private void OnDisable()
	{
		delete.onClick.RemoveListener(Delte);
	}
}