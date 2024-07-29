using FactoryBots.Game.Services.Bots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public class StorageBuilding : BaseBuilding
    {
        private readonly Queue<IDelivery> _deliveryQueue = new Queue<IDelivery>();

        private Box _box;
        private bool _isBoxReady;

        public override void Initialize(string buildingId, BoxFactory boxFactory)
        {
            base.Initialize(buildingId, boxFactory);

            _isBoxReady = false;
            ProvideNextBox();
        }

        public override void Interact(IDelivery delivery)
        {
            if (_isBoxReady && delivery.TrySetBox(_box, Id))
            {
                _isBoxReady = false;
                _box = null;
                ProvideNextBox();
                return;
            }

            _deliveryQueue.Enqueue(delivery);
        }

        private void ProvideNextBox()
        {
            _box = BoxFactory.GetBox(_insidePoint.position);
            StartCoroutine(MoveBoxToDelivery(_deliveryPoint.position));
        }

        private IEnumerator MoveBoxToDelivery(Vector3 deliveryPosition)
        {
            while (Vector3.Distance(_box.transform.position, deliveryPosition) > 0.01f)
            {
                _box.transform.position = Vector3.MoveTowards(_box.transform.position, deliveryPosition, _conveyorSpeed * Time.deltaTime);
                yield return null;
            }

            _box.transform.position = deliveryPosition;
            OnBoxReachedDelivery();
        }

        private void OnBoxReachedDelivery()
        {
            while (_deliveryQueue.Count > 0)
            {
                IDelivery delivery = _deliveryQueue.Dequeue();

                if (delivery.TrySetBox(_box, Id))
                {
                    ProvideNextBox();
                    return;
                }
            }

            _isBoxReady = true;
        }
    }
}
