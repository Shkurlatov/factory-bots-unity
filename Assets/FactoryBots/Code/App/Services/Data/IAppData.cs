using System.Threading.Tasks;

namespace FactoryBots.App.Services.Progress

{
    public interface IAppData : IAppService
    {
        Task SaveSettingsAsync(SettingsData settingsData);
        Task<SettingsData> LoadSettingsAsync();
    }
}
