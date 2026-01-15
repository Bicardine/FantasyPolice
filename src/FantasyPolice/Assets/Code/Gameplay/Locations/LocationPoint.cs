using UnityEngine;

namespace Gameplay.Locations
{
    public class LocationPoint : MonoBehaviour
    {
        [SerializeField] private LocationType _locationType;
    
        public LocationType LocationType => _locationType;
    }
}
