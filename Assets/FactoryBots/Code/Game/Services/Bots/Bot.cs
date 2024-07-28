using FactoryBots.Game.Services.Buildings;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace FactoryBots.Game.Services.Bots
{
    public class Bot : MonoBehaviour, IBot, IDelivery
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _cargoPoint;

        private string _id;
        private Transform _basePoint;
        private IBuilding _targetBuilding;
        private Vector3 _targetPosition;
        private bool _hasTarget;
        private bool _isOnDelivery;

        private Box _box;

        public string Status => GetStatus();

        public bool IsCloseToBase => Vector3.Distance(transform.position, _basePoint.position) < 3.0f;

        public event Action TargetReachedAction;

        public void Initialize(string botId, GameObject botBase)
        {
            _id = botId;
            _basePoint = botBase.transform;
            _targetPosition = botBase.transform.position;

            AnimatorClipInfo[] clipInfo = _animator.GetCurrentAnimatorClipInfo(0);

            if (clipInfo.Length > 0)
            {
                float clipLength = clipInfo[0].clip.length;
                float randomStartTime = Random.Range(0f, clipLength);

                _animator.Play(clipInfo[0].clip.name, 0, randomStartTime / clipLength);
            }
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
                _navMeshAgent.isStopped = true;
                TargetReachedAction?.Invoke();

                if (_targetBuilding != null && _isOnDelivery)
                {
                    _targetBuilding.Interact(this);
                }
            }
        }

        public void MoveToPosition(Vector3 targetPosition)
        {
            _targetBuilding = null;
            _targetPosition = targetPosition;
            _navMeshAgent.destination = _targetPosition;
            _isOnDelivery = false;
            _hasTarget = true;
            _navMeshAgent.isStopped = false;
        }

        public void MoveToBuilding(IBuilding targetBuilding)
        {
            _targetBuilding = targetBuilding;
            _navMeshAgent.destination = _targetBuilding.InteractionPosition;
            _isOnDelivery = true;
            _hasTarget = true;
            _navMeshAgent.isStopped = false;
        }
        
        public void MoveToBase()
        {
            _navMeshAgent.destination = _basePoint.position;
            _isOnDelivery = false;
            _hasTarget = true;
            _navMeshAgent.isStopped = false;
        }
                
        public void ReturnToTarget()
        {
            if (_targetBuilding != null)
            {
                MoveToBuilding(_targetBuilding);
                return;
            }

            MoveToPosition(_targetPosition);
        }

        public bool TrySetBox(Box box, string buildingId)
        {
            if (_hasTarget)
            {
                return false;
            }
            
            if (_isOnDelivery == false)
            {
                return false;
            }
            
            if (_box != null)
            {
                return false;
            }

            if (CheckBuildingId(buildingId) == false)
            {
                return false;
            }

            SetBox(box);
            return true;
        }

        public bool TryRetrieveBox(out Box box)
        {
            box = _box;

            if (box == null)
            {
                return false;
            }

            _box = null;
            return true;
        }

        private void SetBox(Box box)
        {
            _box = box;
            _box.transform.SetParent(_cargoPoint);
            _box.transform.SetPositionAndRotation(_cargoPoint.position, _cargoPoint.rotation);
        }

        private bool CheckBuildingId(string buildingId)
        {
            if (_targetBuilding == null)
            {
                return false;
            }

            return _targetBuilding.Id == buildingId;
        }

        private string GetStatus()
        {
            if (_targetBuilding == null)
            {
                return $"{_id} - (X: {(int)_targetPosition.x}, Y: {(int)_targetPosition.z})";
            }

            return $"{_id} - {_targetBuilding.Id}";
        }
    }
}
