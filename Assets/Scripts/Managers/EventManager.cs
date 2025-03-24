using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction InputSettingsChanged;
    public static void OnInputSettingsChanged() => InputSettingsChanged?.Invoke();
}
