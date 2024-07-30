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
        private readonly Stack<IBotCommand> _commands;
        private readonly float _closeToBaseDistance;

        public string Status => GetStatus();

        private Action TargetReachedAction;

        public Bot(BotComponents components, Action onTargetReached)
        {
            _components = components;
            _commands = new Stack<IBotCommand>();
            _closeToBaseDistance = 3.0f;
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
            if (_commands.Count > 0 && _commands.Peek() is BotBaseCommand)
            {
                return;
            }

            IBotCommand command = new BotBaseCommand();
            _commands.Push(command);

            MoveToTargetPosition(_components.BasePoint.position);
        }

        public void ExecutePositionCommand(Vector3 targetPosition, bool isModifiedCommand)
        {
            if (_commands.Count > 0)
            {
                _commands.Pop();
            }

            IBotCommand command = new BotPositionCommand(targetPosition);
            _commands.Push(command);

            MoveToTargetPosition(targetPosition);
        }

        public void ExecuteDeliveryCommand(IBuilding targetBuilding, bool isModifiedCommand)
        {
            if (_commands.Count > 0)
            {
                _commands.Pop();
            }

            IBotCommand command = new BotDeliveryCommand(targetBuilding);
            _commands.Push(command);

            MoveToTargetPosition(targetBuilding.InteractionPosition);
        }

        public void ExecutePreviousCommand()
        {
            if (_commands.Count == 0)
            {
                return;
            }

            _commands.Pop();

            if (_commands.Count == 0)
            {
                return;
            }

            IBotCommand command = _commands.Peek();
            command.SetTargetReached(false);

            switch (command)
            {
                case BotBaseCommand:
                    MoveToTargetPosition(_components.BasePoint.position);
                    break;
                case BotPositionCommand positionCommand:
                    MoveToTargetPosition(positionCommand.TargetPosition);
                    break;
                case BotDeliveryCommand deliveryCommand:
                    MoveToTargetPosition(deliveryCommand.TargetBuilding.InteractionPosition);
                    break;
            }
        }

        public bool TrySetBox(Box box, string buildingId)
        {
            if (_commands.Count == 0 || _commands.Peek().TargetId != buildingId || _commands.Peek().IsTargetReached == false)
            {
                return false;
            }

            if (_components.Cargo.IsLoaded)
            {
                return false;
            }

            _components.Cargo.SetBox(box);
            return true;
        }

        public bool TryRetrieveBox(out Box box) =>
            _components.Cargo.TryRetrieveBox(out box);

        private void MoveToTargetPosition(Vector3 targetPosition)
        {
            _components.Animator.PlayMove();
            _components.Mover.MoveToTargetPosition(targetPosition);
        }

        private string GetStatus()
        {
            string target = string.Empty;

            if (_commands.Count > 0)
            {
                target = $" - {_commands.Peek().TargetId}";
            }

            return $"{_components.Registry.Id}{target}";
        }

        private void OnTargetReached()
        {
            _components.Animator.PlayIdle();

            IBotCommand command = _commands.Peek();
            command.SetTargetReached(true);

            if (command is BotDeliveryCommand deliveryCommand)
            {
                deliveryCommand.TargetBuilding.Interact(this);
            }

            TargetReachedAction?.Invoke();
        }

        public void Cleanup()
        {
            _components.Mover.TargetReachedAction -= OnTargetReached;
            Object.Destroy(_components.BotObject);
            TargetReachedAction = null;
        }
    }
}
