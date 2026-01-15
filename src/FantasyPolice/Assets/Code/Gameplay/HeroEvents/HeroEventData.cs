using System;
using Gameplay.Locations;
using UnityEngine;

namespace Gameplay.HeroEvents
{
    [Serializable]
    public class HeroEventDataView
    {

        [SerializeField] private string _nameKey;
        [SerializeField] private string _descriptionKey;
    

        public string NameKey => _nameKey;
        public string DescriptionKey => _descriptionKey;
    }

    [Serializable]
    public class HeroEventData
    {
        [SerializeField] private string _id;
        [SerializeField] private LocationType _locationType;
        [SerializeField] private HeroEventDataView _view;
        [SerializeField] private float _timeInSeconds;
        [SerializeField] [Range(0, 100)] private int _powerToComplete;
        [SerializeField] [Range(0, 10000)] private int _exp;
    
        public string Id => _id;
        public LocationType LocationType => _locationType;
        public HeroEventDataView View => _view;
        public float TimeInSeconds => _timeInSeconds;
        public int PowerToComplete => _powerToComplete;
        public int Exp => _exp;
    }
}