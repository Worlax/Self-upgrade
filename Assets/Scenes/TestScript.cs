using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
	[SerializeField] Button but;
	[SerializeField] RectTransform objPref;
	[SerializeField] RectTransform TOPDAD;

	void Fun()
	{
		Instantiate(objPref, transform);
		Fun2();

	}

	public void Fun2()
	{
		//LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
		//LayoutRebuilder.ForceRebuildLayoutImmediate(TOPDAD);

		//foreach (LayoutGroup lg in GetComponentsInChildren<LayoutGroup>())
		//{
		//	LayoutRebuilder.ForceRebuildLayoutImmediate(lg.transform as RectTransform);
		//}




		//foreach (LayoutGroup lg in GetComponentsInParent<LayoutGroup>())
		//{
		//	LayoutRebuilder.ForceRebuildLayoutImmediate(lg.transform as RectTransform);
		//}
	}

	private void OnEnable()
	{
		but.onClick.AddListener(Fun);
	}

	private void OnDisable()
	{
		but.onClick.RemoveListener(Fun);
	}
}