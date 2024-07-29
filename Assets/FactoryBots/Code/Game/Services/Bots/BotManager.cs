using FactoryBots.Game.Services.Bots.Components;
using FactoryBots.Game.Services.Buildings;
using FactoryBots.Game.Services.Input;
using FactoryBots.Game.Services.Overlay;
using FactoryBots.Game.Services.Parking;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Bots
{
    public class BotManager : IGameBots
    {
        private const string BOT_TAG = "Bot";
        private const string WALKABLE_TAG = "Walkable";
        private const string BUILDING_TAG = "Building";

        private readonly IGameOverlay _overlay;
        private readonly IGameInput _input;
        private readonly IGameParking _parking;
        private readonly BotFactory _botFactory;

        private Dictionary<string, IBot> _bots;
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
            _bots = _botFactory.CreateBots(_parking.GetBotBasePoints(), OnBotReachedTarget);
            _selectedBot = null;
            _isAlarm = false;

            SubscribeToEvents();
        }

        private void OnSelectPerformed(GameObject targetObject)
        {
            if (_selectedBot != null)
            {
                _overlay.BotStatusPanel.UpdateStatusText(string.Empty);
                _selectedBot.Unselect();
                _selectedBot = null;
            }

            if (targetObject == null)
            {
                return;
            }

            if (targetObject.CompareTag(BOT_TAG))
            {
                try
                {
                    string botId = targetObject.GetComponent<BotRegistry>().Id;
                    _selectedBot = _bots[botId];
                    _overlay.BotStatusPanel.UpdateStatusText(_selectedBot.Status);
                    _selectedBot.Select();
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception.Message);
                }
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
                _selectedBot.ExecutePositionCommand(targetPosition);
                _overlay.BotStatusPanel.UpdateStatusText(_selectedBot.Status);

                return;
            }

            if (targetObject.CompareTag(BUILDING_TAG))
            {
                _selectedBot.ExecuteDeliveryCommand(targetObject.GetComponent<IBuilding>());
                _overlay.BotStatusPanel.UpdateStatusText(_selectedBot.Status);
            }
        }

        private void OnAlarmStarted()
        {
            _isAlarm = true;

            if (IsAllBotsCloseToBase())
            {
                _parking.CloseGate();
                return;
            }

            SendAllBotsToBase();
        }

        private void OnAlarmCanceled()
        {
            _isAlarm = false;

            if (_parking.IsGateOpen)
            {
                ReturnAllBotsToTarget();
                return;
            }

            _parking.OpenGate();
        }

        private void OnBotReachedTarget()
        {
            if (_isAlarm == false)
            {
                return;
            }

            if (IsAllBotsCloseToBase())
            {
                _parking.CloseGate();
            }
        }

        private void OnGateOpened()
        {
            ReturnAllBotsToTarget();
        }

        private void SendAllBotsToBase()
        {
            foreach (IBot bot in _bots.Values)
            {
                bot.ExecuteBaseCommand();
            }

            if (_selectedBot != null)
            {
                _overlay.BotStatusPanel.UpdateStatusText(_selectedBot.Status);
            }
        }

        private void ReturnAllBotsToTarget()
        {
            foreach (IBot bot in _bots.Values)
            {
                bot.ExecutePreviousCommand();
            }

            if (_selectedBot != null)
            {
                _overlay.BotStatusPanel.UpdateStatusText(_selectedBot.Status);
            }
        }

        private bool IsAllBotsCloseToBase()
        {
            foreach (IBot bot in _bots.Values)
            {
                if (bot.IsCloseToBase() == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void SubscribeToEvents()
        {
            _overlay.AlarmPanel.StartAlarmAction += OnAlarmStarted;
            _overlay.AlarmPanel.CancelAlarmAction += OnAlarmCanceled;

            _input.SelectPerformedAction += OnSelectPerformed;
            _input.ExecutePerformedAction += OnExecutePerformed;

            _parking.GateOpenedAction += OnGateOpened;
        }
        
        private void UnsubscribeFromEvents()
        {
            _overlay.AlarmPanel.StartAlarmAction -= OnAlarmStarted;
            _overlay.AlarmPanel.CancelAlarmAction -= OnAlarmCanceled;

            _input.SelectPerformedAction -= OnSelectPerformed;
            _input.ExecutePerformedAction -= OnExecutePerformed;

            _parking.GateOpenedAction -= OnGateOpened;
        }

        public void Cleanup()
        {
            UnsubscribeFromEvents();

            foreach (IBot bot in _bots.Values)
            {
                bot.Cleanup();
            }

            _bots.Clear();
        }
    }
}
