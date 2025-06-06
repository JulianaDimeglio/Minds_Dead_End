using System;

public static class IterationRoomEvents
{
    public static event Action OnPlayerEnteredRoom;
    public static event Action OnPlayerExitedRoom;

    public static void PlayerEntered() => OnPlayerEnteredRoom?.Invoke();
    public static void PlayerExited() => OnPlayerExitedRoom?.Invoke();
}