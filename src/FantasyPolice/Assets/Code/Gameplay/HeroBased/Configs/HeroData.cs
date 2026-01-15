using System;
using Gameplay.HeroBased.StatsBased.Configs;
using Model.Data;
using UnityEngine;

namespace Gameplay.HeroBased.Configs
{
    [Serializable]
    public class HeroData : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private HeroViewData _viewData;
        [SerializeField] private HeroStatsConfigData _statsConfigData;

        public string Id => _id;
        public HeroViewData ViewData => _viewData;
        public HeroStatsConfigData StatsConfigData => _statsConfigData;
    }
}