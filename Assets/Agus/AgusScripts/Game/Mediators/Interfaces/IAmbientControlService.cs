namespace Game.Mediators.Interfaces
{
    public interface IAmbientAudioService
    {
        void ToggleRadio(string radioId, bool on);
        void PlayAmbientSound(string clipName);
        void StopAmbientSound(string clipName);
    }
}