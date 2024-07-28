using FactoryBots.Game.Services.Bots;
using System.Collections;
using UnityEngine;

namespace FactoryBots.Game.Services.Buildings
{
    public class FactoryBuilding : BaseBuilding
    {
        public override void Interact(IDelivery delivery)
        {
            if (delivery.TryRetrieveBox(out Box box) == false)
            {
                return;
            }

            BoxFactory.ReturnToParentTransform(box);
            box.transform.SetPositionAndRotation(_deliveryPoint.position, _deliveryPoint.rotation);
            StartCoroutine(MoveBoxInside(box, _insidePoint.position));
        }

        private IEnumerator MoveBoxInside(Box box, Vector3 insidePosition)
        {
            while (Vector3.Distance(box.transform.position, insidePosition) > 0.01f)
            {
                box.transform.position = Vector3.MoveTowards(box.transform.position, insidePosition, _conveyorSpeed * Time.deltaTime);
                yield return null;
            }

            BoxFactory.ReturnBox(box);
        }
    }
}
