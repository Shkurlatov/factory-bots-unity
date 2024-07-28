using FactoryBots.Game.Services.Buildings;
using FactoryBots.Game.Services.Input;
using FactoryBots.Game.Services.Overlay;
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

        private readonly IGameOverlay _overlay;
        private readonly IGameInput _input;
        private readonly IGameParking _parking;
        private readonly IGameBuildings _buildings;
        private readonly BotFactory _botFactory;

        private List<IBot> _bots;
        private IBot _selectedBot;
        private bool _isAlarm;

        public BotManager(IGameOverlay overlay, IGameInput input, IGameParking parking, BotFactory botFactory)
        {
            _overlay = overlay;
            _botFactory = botFactory;
            _input = input;
            _parking = parking;
        }

        public void Initialize()
        {
            _bots = _botFactory.CreateBots(_parking.BotBasePoints);
            _selectedBot = null;
            _isAlarm = false;

            _overlay.AlarmPanel.StartAlarmAction += OnAlarmStarted;
            _overlay.AlarmPanel.CancelAlarmAction += OnAlarmCanceled;

            _input.SelectPerformedAction += OnSelectPerformed;
            _input.ExecutePerformedAction += OnExecutePerformed;
        }

        private void OnAlarmStarted()
        {
            Debug.Log("Alarm Started");
        }

        private void OnAlarmCanceled()
        {
            Debug.Log("Alarm Canceled");
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
                _selectedBot = targetObject.GetComponent<IBot>();
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
                _selectedBot.MoveToPosition(targetPosition);

                return;
            }

            if (targetObject.CompareTag(BUILDING_TAG))
            {
                _selectedBot.MoveToBuilding(targetObject.GetComponent<IBuilding>());
            }
        }

        public void Cleanup()
        {
            _overlay.AlarmPanel.StartAlarmAction -= OnAlarmStarted;
            _overlay.AlarmPanel.CancelAlarmAction -= OnAlarmCanceled;

            _input.SelectPerformedAction -= OnSelectPerformed;
            _input.ExecutePerformedAction -= OnExecutePerformed;
        }
    }
}
