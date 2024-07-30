using System;
using UnityEngine;

namespace FactoryBots.Game.Services.Input
{
    public interface IGameInput : IGameService
    {
        event Action<GameObject> SelectPerformedAction;
        event Action<GameObject, Vector3, bool> ExecutePerformedAction;
    }
}
