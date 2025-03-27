using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //change input settings
    public static UnityEvent InputSettingsChanged;
    public static void OnInputSettingsChanged() => InputSettingsChanged?.Invoke();

    //change to follow
    public static UnityEvent<int> ChangeAIToFollow;
    public static void OnChangeAIToFollow(int character) => ChangeAIToFollow?.Invoke(character);

    //change to idle
    public static UnityEvent<int> ChangeAIToIdle;
    public static void OnChangeAIToIdle(int character) => ChangeAIToFollow?.Invoke(character);


}
