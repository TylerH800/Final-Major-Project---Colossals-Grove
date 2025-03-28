using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //change input settings
    public static UnityAction InputSettingsChanged;
    public static void OnInputSettingsChanged() => InputSettingsChanged?.Invoke();

    //change eli mode
    public static UnityAction ChangeEliAIMode;
    public static void OnChangeEliAIMode() => ChangeEliAIMode?.Invoke();

    //change leda mode
    public static UnityAction ChangeLedaAIMode;
    public static void OnChangeLedaAIMode() => ChangeLedaAIMode?.Invoke();
}
