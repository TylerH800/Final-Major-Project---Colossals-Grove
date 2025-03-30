using UnityEngine;
using UnityEngine.Events;
using System;

public static class EventManager
{
    //change input settings
    public static Action InputSettingsChanged;
    public static void OnInputSettingsChanged() => InputSettingsChanged?.Invoke();

    //change eli mode
    public static Action ChangeEliAIMode;
    public static void OnChangeEliAIMode() => ChangeEliAIMode?.Invoke();

    //change leda mode
    public static Action ChangeLedaAIMode;
    public static void OnChangeLedaAIMode() => ChangeLedaAIMode?.Invoke();
}
