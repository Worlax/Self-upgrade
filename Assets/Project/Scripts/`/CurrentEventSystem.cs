using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentEventSystem : Singleton<CurrentEventSystem>
{
#pragma warning disable 0649

	[field: SerializeField] public EventSystem EventSystem { get; private set; }

#pragma warning restore 0649
}