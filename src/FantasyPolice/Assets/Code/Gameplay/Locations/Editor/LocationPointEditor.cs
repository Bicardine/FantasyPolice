using Gameplay.Locations;
using UnityEditor;
using UnityEngine;

namespace Code.Gameplay.Locations.Editor
{
    [CustomEditor(typeof(LocationPoint))]
    public class LocationPointEditor : UnityEditor.Editor
    {
        private const float Radius = 0.5f;
        
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(LocationPoint point, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(point.transform.position, Radius);
        }
    }
}
