using System;
using System.Collections.Generic;

namespace Gameplay.HeroBased
{
    public interface IHeroesService
    {
        event Action<string> OnAvailable;
        IReadOnlyList<string> AvailableHeroes { get; }
        void MarkAsAvailable(string heroId);
    }
}