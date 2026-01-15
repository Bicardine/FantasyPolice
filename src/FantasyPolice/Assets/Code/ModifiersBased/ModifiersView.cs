using Gameplay.HeroBased.StatsBased;
using Gameplay.HeroBased.StatsBased.Configs;
using Infrastructure.Factory;
using Model.Data;
using UnityEngine;

namespace ModifiersBased
{
    public class ModifiersView : MonoBehaviour, IItemRenderer<StatModifierType>
    {
        [SerializeField] private Transform _container;
    
        private IGameFactory _gameFactory;
        private HeroStats _stats;

        public void Construct(IGameFactory gameFactory, HeroStats stats)
        {
            _gameFactory = gameFactory;

            _stats = stats;
            _stats.OnModifierAdded += Render;
        }

        private void OnDestroy()
        {
            _stats.OnModifierAdded -= Render;
        }

        public void Render(StatModifierType data)
        {
            _gameFactory.ModifierView(data, _container);
        }
    }
}