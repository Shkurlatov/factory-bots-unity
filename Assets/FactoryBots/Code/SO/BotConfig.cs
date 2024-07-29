using UnityEngine;

namespace FactoryBots.SO
{
    [CreateAssetMenu(menuName = "Configs/BotConfig", fileName = "BotConfig")]
    public class BotConfig : ScriptableObject
    {
        [Range(2.0f, 5.0f)]
        public float MovementSpeed = 3.5f;

        [Range(60.0f, 200.0f)]
        public float RotationSpeed = 120.0f;

        [Range(2.0f, 15.0f)]
        public float Acceleration = 8.0f;
    }
}