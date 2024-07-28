using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Components
{
    public class BotEffects : MonoBehaviour
    {
        [SerializeField] private GameObject _highlight;

        public void Initialize() => 
            _highlight.SetActive(false);

        public void ToggleHighlight(bool isActive) =>
            _highlight.SetActive(isActive);
    }
}
