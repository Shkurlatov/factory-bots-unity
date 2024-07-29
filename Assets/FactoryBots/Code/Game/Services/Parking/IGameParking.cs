using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Parking
{
    public interface IGameParking : IGameService
    {
        bool IsGateOpen { get; }

        event Action GateOpenedAction;

        List<Transform> GetBotBasePoints();
        void OpenGate();
        void CloseGate();
    }
}
