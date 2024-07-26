﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Parking
{
    public interface IGameParking : IGameService
    {
        bool IsGateOpen { get; }
        List<Transform> BotBasePoints { get; }

        event Action GateOpenedAction;
        event Action GateClosedAction;

        void CloseGate();
        void OpenGate();
    }
}