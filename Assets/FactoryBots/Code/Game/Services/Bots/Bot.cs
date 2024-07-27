using FactoryBots.Game.Services.Buildings;
using UnityEngine;
using UnityEngine.AI;

namespace FactoryBots.Game.Services.Bots
{
    public class Bot : MonoBehaviour, IBot, IDelivery
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _boxHolderPoint;

        private string _id;
        private Transform _basePoint;
        private IBuilding _targetBuilding;
        private Vector3 _targetPosition;
        private bool _hasTarget;

        private Box _box;

        public string Status => GetStatus();

        public string TargetId => GetTargetId();

        public void Initialize(string botId, GameObject botBase)
        {
            _id = botId;
            _basePoint = botBase.transform;
            _targetPosition = botBase.transform.position;
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
                _hasTarget = false;
                Debug.Log("Reached destination.");
            }
        }

        public void MoveToPosition(Vector3 targetPosition)
        {
            _navMeshAgent.destination = targetPosition;
            _hasTarget = true;
        }

        public void MoveToBuilding(IBuilding targetBuilding)
        {
            _navMeshAgent.destination = targetBuilding.InteractionPosition;
            _hasTarget = true;
        }

        public bool TrySetBox(Box box)
        {
            return false;
        }

        private string GetStatus()
        {
            return _targetBuilding == null
                ? $"{_id} => (X: {(int)_targetPosition.x}, Y: {(int)_targetPosition.z})"
                : $"{_id} => {_targetBuilding.Id}";
        }

        private string GetTargetId()
        {
            return _targetBuilding == null
                ? string.Empty
                : _targetBuilding.Id;
        }
    }
}
