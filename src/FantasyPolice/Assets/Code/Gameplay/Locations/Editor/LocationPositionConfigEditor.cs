using System.Linq;
using Extensions;
using Gameplay.Locations;
using Gameplay.Locations.Config;
using UnityEditor;
using UnityEngine;

namespace Code.Gameplay.Locations.Editor
{
    [CustomEditor(typeof(LocationPositionsConfig))]
    public class LocationPositionConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LocationPositionsConfig levelData = (LocationPositionsConfig) target;

            if (GUILayout.Button("Collect"))
            {
                // !!! FindObjectOfType is using only this place !!! IT'S NOT bad practice if it's using for editor only.
                // It's just speed up setting SO editor data.
                var collection = FindObjectsOfType<LocationPoint>()
                    .Select(x => new LocationPositionData(x.LocationType, x.transform.position.ToVector2()));
                levelData.Set(collection);
            }
      
            EditorUtility.SetDirty(target);
        }
    }
}