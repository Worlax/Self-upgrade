using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] Button execute;
	[SerializeField] Button cancel;

#pragma warning restore 0649

	public event Action OnDestroy;

	// Events
	protected abstract bool Execute();

	void TryExecute()
	{
		if (Execute())
		{
			DestroyWindow();
		}
	}

	protected void DestroyWindow()
	{
		OnDestroy?.Invoke();
		Destroy(gameObject);
	}

	// Unity
	protected virtual void OnEnable()
	{
		cancel.onClick.AddListener(DestroyWindow);
		execute.onClick.AddListener(TryExecute);
	}

	protected virtual void OnDisable()
	{
		cancel.onClick.RemoveListener(DestroyWindow);
		execute.onClick.RemoveListener(TryExecute);
	}
}