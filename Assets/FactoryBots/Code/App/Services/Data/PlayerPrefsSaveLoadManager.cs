using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace FactoryBots.App.Services.Progress
{
    public class PlayerPrefsSaveLoadManager : IAppData
    {
        private const string SETTINGS_KEY = "SettingsData";

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task SaveSettingsAsync(SettingsData settingsData)
        {
            await _semaphore.WaitAsync();
            try
            {
                string json = JsonUtility.ToJson(settingsData);
                PlayerPrefs.SetString(SETTINGS_KEY, json);
                PlayerPrefs.Save();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save settings data. Exception: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<SettingsData> LoadSettingsAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (PlayerPrefs.HasKey(SETTINGS_KEY))
                {
                    string json = PlayerPrefs.GetString(SETTINGS_KEY);
                    SettingsData settingsData = JsonUtility.FromJson<SettingsData>(json);
                    return await Task.FromResult(settingsData);
                }
                return await Task.FromResult(new SettingsData(1));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load settings data. Exception: {ex.Message}");
                return await Task.FromResult(new SettingsData(1));
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public void Cleanup()
        {
            _semaphore.Dispose();
        }
    }
}
