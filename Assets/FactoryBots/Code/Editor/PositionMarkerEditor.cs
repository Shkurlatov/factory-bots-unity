using FactoryBots.Game;
using UnityEditor;
using UnityEngine;

namespace FactoryBots.Editor
{
    [CustomEditor(typeof(PositionMarker))]
    public class PositionMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGismo(PositionMarker marker, GizmoType gizmo)
        {
            Gizmos.color = marker.Color;
            Gizmos.DrawSphere(marker.transform.position, 1.0f);
        }
    }
}