namespace AudioModule.Contracts
{
    public interface IAudioPlayer
    {
        void PlayOnce(SoundId id);
        void PlayClick();
    }
}