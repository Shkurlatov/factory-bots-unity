using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FactoryBots.Game.Services.Parking
{
    public class ParkingManager : MonoBehaviour, IGameParking
    {
        private readonly float _gateRotationSpeed = 30.0f;
        private readonly float _gateMinAngle = 0.0f;
        private readonly float _gateMaxAngle = 90.0f;

        [SerializeField] private GameObject _gate;
        [SerializeField] private List<Transform> _botBasePoints;

        private Coroutine _currentGateCoroutine;

        public bool IsGateOpen { get; private set; }

        public List<Transform> BotBasePoints => _botBasePoints.ToList();

        public event Action GateOpenedAction;
        public event Action GateClosedAction;

        public void Initialize()
        {
            IsGateOpen = true;
        }

        public void OpenGate()
        {
            if (_currentGateCoroutine != null)
            {
                StopCoroutine(_currentGateCoroutine);
            }

            _currentGateCoroutine = StartCoroutine(RotateGateTo(_gateMaxAngle, OnGateOpenComplete));
        }

        public void CloseGate()
        {
            IsGateOpen = false;

            if (_currentGateCoroutine != null)
            {
                StopCoroutine(_currentGateCoroutine);
            }

            _currentGateCoroutine = StartCoroutine(RotateGateTo(_gateMinAngle, OnGateCloseComplete));
        }

        private IEnumerator RotateGateTo(float targetAngle, Action onComplete)
        {
            Quaternion startRotation = _gate.transform.rotation;
            Quaternion endRotation = Quaternion.Euler(targetAngle, startRotation.eulerAngles.y, startRotation.eulerAngles.z);

            float startAngle = startRotation.eulerAngles.x;
            float totalRotation = Mathf.Abs(targetAngle - startAngle);

            float duration = totalRotation / _gateRotationSpeed;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float interpolationFactor = elapsedTime / duration;
                _gate.transform.rotation = Quaternion.Slerp(startRotation, endRotation, interpolationFactor);
                yield return null;
            }

            _gate.transform.rotation = endRotation;
            onComplete?.Invoke();
        }

        private void OnGateOpenComplete()
        {
            IsGateOpen = true;
            GateOpenedAction?.Invoke();
        }
        
        private void OnGateCloseComplete()
        {
            GateClosedAction?.Invoke();
        }

        public void Cleanup()
        {
            if (_currentGateCoroutine != null)
            {
                StopCoroutine(_currentGateCoroutine);
                _currentGateCoroutine = null;
            }
        }
    }
}
