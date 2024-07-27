using UnityEngine;
using UnityEngine.AI;

namespace FactoryBots.Game.Services.Bots
{
    public class Bot : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private bool _hasTarget;

        void Update()
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
                _hasTarget = false;
                Debug.Log("Reached destination.");
            }
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            _navMeshAgent.destination = targetPosition;
            _hasTarget = true;
        }
    }
}
