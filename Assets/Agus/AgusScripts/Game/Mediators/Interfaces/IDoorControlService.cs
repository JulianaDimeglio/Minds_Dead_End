namespace Game.Mediators.Interfaces
{
    public interface IDoorControlService
    {
        void OpenDoor(string doorId);
        void CloseDoor(string doorId);
        void ToggleDoor(string doorId);
    }
}