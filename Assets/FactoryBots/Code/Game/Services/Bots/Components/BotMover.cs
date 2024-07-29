using FactoryBots.SO;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace FactoryBots.Game.Services.Bots.Components
{
    public class BotMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private bool _hasTarget;

        public event Action TargetReachedAction;

        public void Initialize(BotConfig botConfig)
        {
            _navMeshAgent.speed = botConfig.MovementSpeed;
            _navMeshAgent.angularSpeed = botConfig.RotationSpeed;
            _navMeshAgent.acceleration = botConfig.Acceleration;
        }

        public void MoveToTargetPosition(Vector3 targetPosition)
        {
            _navMeshAgent.destination = targetPosition;
            _navMeshAgent.isStopped = false;
            _hasTarget = true;
        }

        private void Update()
        {
            if (_hasTarget == false)
            {
                return;
            }

            if (_navMeshAgent.pathPending)
            {
                return;
            }

            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                OnTargetReached();
            }
        }

        private void OnTargetReached()
        {
            _hasTarget = false;
            _navMeshAgent.isStopped = true;

            TargetReachedAction?.Invoke();
        }
    }
}
