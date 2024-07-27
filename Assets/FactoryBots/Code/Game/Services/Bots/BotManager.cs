using FactoryBots.Game.Services.Buildings;
using FactoryBots.Game.Services.Input;
using FactoryBots.Game.Services.Parking;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots
{
    public class BotManager : IGameBots
    {
        private const string BOT_TAG = "Bot";
        private const string WALKABLE_TAG = "Walkable";
        private const string BUILDING_TAG = "Building";
        private const string STORAGE_TAG = "Storage";
        private const string FACTORY_TAG = "Factory";

        private readonly IGameInput _input;
        private readonly IGameParking _parking;
        private readonly IGameBuildings _buildings;
        private readonly BotFactory _botFactory;

        private List<Bot> _bots;
        private Bot _selectedBot;
        private bool _isAlarm;

        public BotManager(IGameInput input, IGameParking parking, IGameBuildings buildings, BotFactory botFactory)
        {
            _botFactory = botFactory;
            _input = input;
            _parking = parking;
            _buildings = buildings;
        }

        public void Initialize()
        {
            _bots = _botFactory.CreateBots(_parking.BotBasePoints);
            _selectedBot = null;
            _isAlarm = false;

            _input.SelectPerformedAction += OnSelectPerformed;
            _input.ExecutePerformedAction += OnExecutePerformed;
        }

        private void OnSelectPerformed(GameObject targetObject)
        {
            if (_selectedBot != null)
            {
                _selectedBot = null;
            }

            if (targetObject == null)
            {
                return;
            }

            if (targetObject.CompareTag(BOT_TAG))
            {
                _selectedBot = targetObject.GetComponent<Bot>();
            }
        }

        private void OnExecutePerformed(GameObject targetObject, Vector3 targetPosition)
        {
            if (_isAlarm)
            {
                return;
            }

            if (_selectedBot == null)
            {
                return;
            }

            if (targetObject.CompareTag(WALKABLE_TAG))
            {
                _selectedBot.SetTargetPosition(targetPosition);

                return;
            }

            if (targetObject.CompareTag(BUILDING_TAG))
            {
                Debug.Log("Execute Building interaction.");
            }
        }

        public void Cleanup()
        {
            _input.SelectPerformedAction -= OnSelectPerformed;
            _input.ExecutePerformedAction -= OnExecutePerformed;
        }
    }
}
