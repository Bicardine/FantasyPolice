using Extensions;
using Gameplay.HeroBased.HeroProgressBased.Configs;
using Gameplay.StaticData;
using Infrastructure.Factory;
using Model.Data;
using UnityEngine;

namespace Gameplay.HeroBased.HeroProgressBased
{
    public class LevelUpService : ILevelUpService, INonLazyResolveable
    {
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;

        private const int NormalizedValueOnMaxLevel = 1;
        private const string MaxLevelKey = "Max Level";
        private const string ExpKey = "exp";

        public LevelUpService(IStaticDataService staticData, IGameFactory gameFactory)
        {
            _staticData = staticData;
            _gameFactory = gameFactory;

            _gameFactory.OnHeroCardCreated += OnHeroCardCreated;
        }

        private void OnHeroCardCreated(HeroCard heroCard)
        {
            heroCard.Progress.OnExpChanged += OnExpChanged;
            heroCard.OnDestroyed += OnHeroDestroyed;
            OnExpChanged(heroCard);
        }

        private void OnHeroDestroyed(HeroCard heroCard)
        {
            heroCard.OnDestroyed -= OnHeroDestroyed;
            heroCard.Progress.OnExpChanged -= OnExpChanged;
        }

        private void OnExpChanged(HeroCard heroCard)
        {
            UpdateLevel(heroCard);

            var data = IsMaxLevel(heroCard)
                ? new HeroExpProgressData(NormalizedValueOnMaxLevel, MaxLevelKey)
                : new HeroExpProgressData(NormalizedExpProgress(heroCard),$"{heroCard.Progress.Exp}/{ExpForNextLevel(heroCard)} {ExpKey}");
            heroCard.ExpBar.Render(data);
        }

        private static float NormalizedExpProgress(HeroCard heroCard)
        {
            return heroCard.Progress.Exp / (float)heroCard.Progress.TotalExpToNextLevel;
        }

        private void UpdateLevel(HeroCard heroCard)
        {
            if (IsMaxLevel(heroCard))
                return;

            var forNextLevel = heroCard.Progress.TotalExpToNextLevel = ExpForNextLevel(heroCard);

            if (heroCard.Progress.Exp < forNextLevel)
                return;

            heroCard.Progress.LevelUp();
            heroCard.Progress.ChangeExp(heroCard.Progress.Exp - forNextLevel);
        }

        private int ExpForNextLevel(HeroCard heroCard) =>
            _staticData.ExperienceForLevel(heroCard.Progress.Level.Next());

        private bool IsMaxLevel(HeroCard heroCard) =>
            heroCard.Progress.Level >= _staticData.MaxHeroLevel();
    }

    public interface ILevelUpService
    {
    }
}