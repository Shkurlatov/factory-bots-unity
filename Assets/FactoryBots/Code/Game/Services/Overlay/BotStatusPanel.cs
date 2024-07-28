using TMPro;
using UnityEngine;

namespace FactoryBots.Game.Services.Overlay
{
    public class BotStatusPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _botStatusText;

        public void Initialize() => 
            _botStatusText.text = string.Empty;

        public void UpdateStatusText(string botStatusText) => 
            _botStatusText.text = botStatusText;
    }
}
