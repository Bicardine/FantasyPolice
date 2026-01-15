using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Gameplay.HeroBased.Configs;
using Gameplay.HeroBased.HeroProgressBased;
using Gameplay.HeroBased.StatsBased;
using Gameplay.HeroBased.StatsBased.Configs;
using Infrastructure.Factory;
using Model.Data;
using ModifiersBased;
using UnityEngine;

namespace Gameplay.HeroBased
{
    public class HeroCard : MonoBehaviour, IHaveId, IChildClickable, IDrop, ISlotContent, IMoveable
    {
        [SerializeField] private HeroCardView _view;
        [SerializeField] private HeroCardProgressEvent _progressBar;
        [SerializeField] private HeroExpProgress _expBar;
        [SerializeField] private ModifiersView _modifiersView;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private HeroCardInfo _info;

        private IMovementStrategy _movementStrategy;
        private Transform _transform;
    
        public HeroCardProgressEvent ProgressBar => _progressBar;
        public HeroExpProgress ExpBar => _expBar;
    
        // It's cached to custom property, coz gameObject.transform invokes GetComponent every time.
        // Not much ms, but habit.
        public Transform Transform => _transform ??= transform;

        public string Id { get; private set; }

        public HeroStats Stats;
        public HeroProgress Progress;
        public bool IsOnEvent { get; private set; }
        public Canvas Canvas => _canvas;
    
        public event Action<HeroCard> OnGoToEvent;
        public event Action<HeroCard> OnExitFromEvent;
        public event Action<HeroCard> OnDestroyed;

        private const int IncreaseOnUpgrade = 1;

        public void Construct(string id, HeroStats stats, IMovementStrategy movementStrategy, HeroViewData viewData, IGameFactory gameFactory)
        {
            Id = id;
            Stats = stats;
            _movementStrategy = movementStrategy;
            Progress = new(this);
            _view.Render(viewData);
            _info.Construct(viewData, this);
            _modifiersView.Construct(gameFactory, Stats);
            _progressBar.Hide();
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }

        public void Upgrade()
        {
            if (EnoughPointsToUpgrade() == false)
            {
                Debug.LogWarning($"Not enough {Progress.UpgradePoints} points to upgrade.");
                return;
            }

            Progress.UpgradePoints--;
            Stats.Upgrade(StatType.Power, IncreaseOnUpgrade);
        }

        public bool EnoughPointsToUpgrade() => Progress.UpgradePoints.IsGreaterThanZero();

        public void GoToEvent()
        {
            if (IsOnEvent)
            {
                Debug.LogError($"{nameof(HeroCard)} with {Id} is already on event.");
                return;
            }
        
            _progressBar.Show();
            IsOnEvent = true;
            OnGoToEvent?.Invoke(this);
        }

        public void ExitFromEvent()
        {
            if (IsOnEvent == false)
            {
                Debug.LogError($"{nameof(HeroCard)} with {Id} is not engage in any event.");
                return;
            }
        
            _progressBar.Hide();
            IsOnEvent = false;
            OnExitFromEvent?.Invoke(this);
        }

        public void TryAcceptClick()
        {
            if (CanAcceptClick())
                _info.Toggle();
        }

        public bool CanAcceptClick()
        {
            return true;
        }

        public void OnUnput()
        {
        }

        public bool IsMoving => _movementStrategy.IsWorking;
        public async UniTask MoveTo(Vector3 at)
        {
            if (_movementStrategy.IsWorking == false)
                OnMoveStarted?.Invoke(this);
        
            _movementStrategy.Cancel();
            await _movementStrategy.Move(at);
            OnMoveEnded?.Invoke(this);
        }

        public event Action<IMoveable> OnMoveStarted;
        public event Action<IMoveable> OnMoveEnded;
    }
}