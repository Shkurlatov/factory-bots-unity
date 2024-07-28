using UnityEngine;

namespace FactoryBots.Game.Services.Bots.Components
{
    public class BotComponents
    {
        public readonly GameObject BotObject;
        public readonly Transform BasePoint;
        public readonly BotRegistry Registry;
        public readonly BotAnimator Animator;
        public readonly BotMover Mover;
        public readonly BotEffects Effects;
        public readonly BotCargo Cargo;

        public BotComponents(
            GameObject botObject,
            Transform basePoint,
            BotRegistry registry,
            BotAnimator animator,
            BotMover mover,
            BotEffects effects,
            BotCargo cargo)
        {
            BotObject = botObject;
            BasePoint = basePoint;
            Registry = registry;
            Animator = animator;
            Mover = mover;
            Effects = effects;
            Cargo = cargo;
        }
    }
}
