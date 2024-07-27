﻿using FactoryBots.Game.Services.Bots;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour, IBuilding
    {
        [SerializeField] private Transform _interactionPoint;
        [SerializeField] protected Transform _deliveryPoint;

        public Vector3 InteractionPosition => _interactionPoint.position;

        public string Id { get; private set; }
        protected BoxFactory BoxFactory { get; private set; }

        public virtual void Initialize(string buildingId, BoxFactory boxFactory)
        {
            Id = buildingId;
            BoxFactory = boxFactory;
        }

        public abstract void Interact(Bot bot);
    }
}
