using UnityEngine;

namespace FactoryBots.App.Services.Assets
{
    public interface IAppAssetProvider : IAppService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path, Transform parent);
        GameObject Instantiate(string path, Vector3 at, Transform parent);
    }
}
