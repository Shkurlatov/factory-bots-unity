using System;
using System.Collections;
using System.Collections.Generic;
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

        private int _botAmount;
        private Coroutine _currentGateCoroutine;

        public bool IsGateOpen { get; private set; }

        public event Action GateOpenedAction;

        public void Initialize(int botAmount)
        {
            IsGateOpen = true;
            _botAmount = ValidateBotAmount(botAmount);
        }

        public List<Transform> GetBotBasePoints()
        {
            List<Transform> botBasePoints = new List<Transform>();

            for (int i = 0; i < _botAmount; i++)
            {
                botBasePoints.Add(_botBasePoints[i]);
            }

            return botBasePoints;
        }

        public void OpenGate()
        {
            if (_currentGateCoroutine != null)
            {
                StopCoroutine(_currentGateCoroutine);
            }

            _currentGateCoroutine = StartCoroutine(RotateGateTo(_gateMaxAngle, isOpening: true));
        }

        public void CloseGate()
        {
            IsGateOpen = false;

            if (_currentGateCoroutine != null)
            {
                StopCoroutine(_currentGateCoroutine);
            }

            _currentGateCoroutine = StartCoroutine(RotateGateTo(_gateMinAngle));
        }

        private IEnumerator RotateGateTo(float targetAngle, bool isOpening = false)
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

            if (isOpening)
            {
                IsGateOpen = true;
                GateOpenedAction?.Invoke();
            }
        }

        private int ValidateBotAmount(int botAmount)
        {
            int botBasePointsCount = _botBasePoints.Count;

            if (botBasePointsCount == 0)
            {
                Debug.LogError($"References to bot base points was lost.");
                return 0;
            }

            if (botBasePointsCount < botAmount)
            {
                Debug.LogError($"Not enough bot base points to place all bots.");
                return botBasePointsCount;
            }

            if (botAmount < 1)
            {
                Debug.LogError($"At least 1 bot should be placed, but requested bot amount is {botAmount}.");
                return 1;
            }

            return botAmount;
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
