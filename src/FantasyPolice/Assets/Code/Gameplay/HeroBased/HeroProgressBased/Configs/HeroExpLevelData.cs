using System;
using UnityEngine;

namespace Gameplay.HeroBased.HeroProgressBased.Configs
{
    [Serializable]
    public class HeroExpLevelData
    {
        [SerializeField] private int _expToGetLvl;
    
        public int ExpToGetLvl => _expToGetLvl;
    }
}