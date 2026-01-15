using System;
using System.Collections.Generic;
using System.Linq;
using Model.Definitions;
using UnityEngine;

namespace Gameplay.Locations.Config
{
    [CreateAssetMenu(menuName = "FantasyPolice/Configs/Create LocationPositionConfig", fileName = "LocationPositionConfig")]
    public class LocationPositionsConfig : Definition<LocationPositionData>
    {
#if UNITY_EDITOR
        public void Set(IEnumerable<LocationPositionData> collection)
        {
            _collection = collection.ToArray();
        }
#endif
    }

    [Serializable]
    public class LocationPositionData
    {
        public LocationType LocationType;
        public Vector2 Position;

        public LocationPositionData(LocationType locationType, Vector2 position)
        {
            LocationType = locationType;
            Position = position;
        }
    }
}