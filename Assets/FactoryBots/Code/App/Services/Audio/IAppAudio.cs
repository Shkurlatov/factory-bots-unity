namespace FactoryBots.App.Services.Audio
{
    public interface IAppAudio : IAppService
    {
        void PlayCardFlipSound();
        void PlayMatchSound();
        void PlayMismatchSound();
        void PlayGameCompleteSound();
    }
}
