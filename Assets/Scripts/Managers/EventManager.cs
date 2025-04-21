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

    //eli open gate
    public static Action<Vector3> EliGate;
    public static void OnEliGate(Vector3 pos) => EliGate?.Invoke(pos);

    //eli ignite barrel
    public static Action<Vector3> EliIgnite;
    public static void OnEliIgnite(Vector3 pos) => EliIgnite?.Invoke(pos);

    //leda neutral colossal
    public static Action<Vector3> LedaBait;
    public static void OnLedaBait(Vector3 pos) => LedaBait?.Invoke(pos);

    //game over
    public static Action GameOver;
    public static void OnGameOver() => GameOver?.Invoke();

}
