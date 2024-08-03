using UnityEngine;

namespace FactoryBots.App.Bootstrap
{
    public class AppRunner : MonoBehaviour
    {
        [SerializeField] private AppBootstrapper _appBootstrapperPrefab;

        private void Awake()
        {
            AppBootstrapper bootstrapper = FindObjectOfType<AppBootstrapper>();

            if (bootstrapper == null)
            {
                Instantiate(_appBootstrapperPrefab);
            }
        }
    }
}
