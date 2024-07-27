using UnityEngine;

namespace FactoryBots.Game.Services.Input
{
    public class Raycaster
    {
        private readonly float _maxRaycastDistance = 200.0f;
        private readonly Camera _camera;

        public Raycaster(Camera camera)
        {
            _camera = camera;
        }

        public bool TryGetRaycastTarget(Vector3 pointerPosition, out GameObject raycastTarget)
        {
            Ray ray = _camera.ScreenPointToRay(pointerPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance))
            {
                raycastTarget = hit.collider.gameObject;
                return true;
            }

            raycastTarget = null;
            return false;
        }

        public bool TryGetRaycastTargetWithPosition(Vector3 pointerPosition, out GameObject raycastTarget, out Vector3 hitPosition)
        {
            Ray ray = _camera.ScreenPointToRay(pointerPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance))
            {
                raycastTarget = hit.collider.gameObject;
                hitPosition = hit.point;
                return true;
            }

            raycastTarget = null;
            hitPosition = default;
            return false;
        }
    }
}
