using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Components
{
    public class BotRegistry : MonoBehaviour
    {
        public string Id {  get; private set; }

        public void Initialize(string botId) => 
            Id = botId;
    }
}
