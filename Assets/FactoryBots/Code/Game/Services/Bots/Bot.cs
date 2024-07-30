using FactoryBots.Game.Services.Bots.Commands;
using FactoryBots.Game.Services.Bots.Components;
using FactoryBots.Game.Services.Buildings;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FactoryBots.Game.Services.Bots
{
    public class Bot : IBot, IDelivery
    {
        private readonly BotComponents _components;
        private readonly List<IBotCommand> _commands;
        private readonly float _closeToBaseDistance;

        private int _currentCommandIndex;
        private bool _isFollowBasePoint;

        public string Status => GetStatus();

        private Action TargetReachedAction;

        public Bot(BotComponents components, Action onTargetReached)
        {
            _components = components;
            _commands = new List<IBotCommand>();
            _closeToBaseDistance = 3.0f;

            _currentCommandIndex = -1;
            _isFollowBasePoint = false;

            TargetReachedAction = onTargetReached;
            _components.Mover.TargetReachedAction += OnTargetReached;
        }

        public bool IsCloseToBase()
        {
            float distanceToBase = Vector3.Distance(_components.BotObject.transform.position, _components.BasePoint.position);
            return distanceToBase < _closeToBaseDistance;
        }

        public void Select() =>
            _components.Effects.ToggleHighlight(true);

        public void Unselect() =>
            _components.Effects.ToggleHighlight(false);

        public void ExecuteBaseCommand()
        {
            _isFollowBasePoint = true;

            if (_currentCommandIndex >= 0)
            {
                _commands[_currentCommandIndex].SetTargetReached(false);
            }

            MoveToTargetPosition(_components.BasePoint.position);
        }

        public void ClearAllAndExecuteCommand(IBotCommand command)
        {
            _commands.Clear();
            _commands.Add(command);
            _currentCommandIndex = 0;

            MoveToTargetPosition(command.TargetPosition);
        }

        public void AddCommand(IBotCommand command)
        {
            _commands.Add(command);

            if (_currentCommandIndex < 0)
            {
                _currentCommandIndex++;
                MoveToTargetPosition(_commands[_currentCommandIndex].TargetPosition);
                return;
            }

            if (_currentCommandIndex == _commands.Count - 2 && _commands[_currentCommandIndex].IsTargetReached)
            {
                ExecuteNextCommand();
            }
        }

        public void ExecutePreviousCommand()
        {
            _isFollowBasePoint = false;

            if (_currentCommandIndex >= 0)
            {
                MoveToTargetPosition(_commands[_currentCommandIndex].TargetPosition);
            }
        }

        public bool TrySetBox(Box box, string buildingId)
        {
            if (_currentCommandIndex < 0)
            {
                return false;
            }

            if (_commands[_currentCommandIndex].TargetId != buildingId || _commands[_currentCommandIndex].IsTargetReached == false)
            {
                return false;
            }

            ExecuteNextCommand();

            if (_components.Cargo.IsLoaded)
            {
                return false;
            }

            _components.Cargo.SetBox(box);
            return true;
        }

        public bool TryRetrieveBox(out Box box)
        {
            ExecuteNextCommand();
            return _components.Cargo.TryRetrieveBox(out box);
        }

        private void OnTargetReached()
        {
            _components.Animator.PlayIdle();
            TargetReachedAction?.Invoke();

            if (_isFollowBasePoint || _currentCommandIndex < 0)
            {
                return;
            }

            IBotCommand command = _commands[_currentCommandIndex];
            command.SetTargetReached(true);

            if (command is BotDeliveryCommand deliveryCommand)
            {
                deliveryCommand.TargetBuilding.Interact(this);
                return;
            }

            ExecuteNextCommand();
        }

        private void ExecuteNextCommand()
        {
            if (_commands.Count > _currentCommandIndex + 1)
            {
                _commands[_currentCommandIndex].SetTargetReached(false);
                _currentCommandIndex++;
                MoveToTargetPosition(_commands[_currentCommandIndex].TargetPosition);
                return;
            }

            if (_currentCommandIndex > 0)
            {
                _commands[_currentCommandIndex].SetTargetReached(false);
                _currentCommandIndex = 0;
                MoveToTargetPosition(_commands[_currentCommandIndex].TargetPosition);
                return;
            }

            _components.Animator.PlayIdle();
        }

        private void MoveToTargetPosition(Vector3 targetPosition)
        {
            _components.Animator.PlayMove();
            _components.Mover.MoveToTargetPosition(targetPosition);
        }

        private string GetStatus()
        {
            string id = _components.Registry.Id;

            if (_isFollowBasePoint)
            {
                return $"{id} - Base";
            }

            if (_currentCommandIndex >= 0)
            {
                return $"{id} - {_commands[_currentCommandIndex].TargetId}";
            }

            return id;
        }

        public void Cleanup()
        {
            _components.Mover.TargetReachedAction -= OnTargetReached;
            Object.Destroy(_components.BotObject);
            TargetReachedAction = null;
        }
    }
}
