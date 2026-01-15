using System;
using System.Collections.Generic;

namespace Gameplay.HeroBased
{
    public class HeroesService : IHeroesService
    {
        private readonly List<string> _availables = new();
        public event Action<string> OnAvailable;
    
        public IReadOnlyList<string> AvailableHeroes => _availables;

        public void MarkAsAvailable(string heroId)
        {
            _availables.Add(heroId);
            OnAvailable?.Invoke(heroId);
        }
    }
}