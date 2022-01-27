using UnityEngine;
using UnityEngine.UI;

public class WindowManager : Singleton<WindowManager>
{
#pragma warning disable 0649

	[SerializeField] Canvas windowCanvas;
	[SerializeField] Image screenLock;

	[SerializeField] ChangeMinutesWindow addMinutesWindow;
	[SerializeField] ChangeMinutesWindow subtructMinutesWindow;
	[SerializeField] NewScheduleItemWindow newScheduleItemWindow;

#pragma warning restore 0649

	public ChangeMinutesWindow CreateAddMinutesWindow()
	{
		return InitWindow(addMinutesWindow) as ChangeMinutesWindow;
	}

	public ChangeMinutesWindow CreateSubtructMinutesWindow()
	{
		return InitWindow(subtructMinutesWindow) as ChangeMinutesWindow;
	}

	public NewScheduleItemWindow CreateNewScheduleItemWindow()
	{
		return InitWindow(newScheduleItemWindow) as NewScheduleItemWindow;
	}

	Window InitWindow(Window prefab)
	{
		Window window = Instantiate(prefab, windowCanvas.transform, false);
		window.transform.localPosition = Vector3.zero;
		LockScreen();
		window.OnDestroy += UnlockScreen;

		return window;
	}

	void LockScreen()
	{
		screenLock.gameObject.SetActive(true);
	}

	void UnlockScreen()
	{
		screenLock.gameObject.SetActive(false);
	}
}