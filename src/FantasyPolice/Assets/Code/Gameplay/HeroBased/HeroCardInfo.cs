using System;
using CodeBase.Components.Animations;
using Gameplay.HeroBased.Configs;
using Gameplay.HeroBased.StatsBased.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay.HeroBased
{
    [RequireComponent(typeof(ShowableAnimatorComponent))]
    public class HeroCardInfo : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private ShowableAnimatorComponent _showable;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _power;
        [SerializeField] private Button _upStat;
        [SerializeField] private GraphicRaycaster _upStatRaycaster;

        private bool _isShowing;

        private HeroViewData _viewData;
        private HeroCard _hero;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _showable = GetComponent<ShowableAnimatorComponent>();
        }
#endif

        public void Construct(HeroViewData viewData, HeroCard hero)
        {
            _viewData = viewData;
            _hero = hero;
            
            _hero.Progress.OnLevelUped += OnLevelUped;
        }

        private void OnEnable()
        {
            _upStat.onClick.AddListener(OnUpStatClicked);
        }

        private void OnDisable()
        {
            _upStat.onClick.RemoveListener(OnUpStatClicked);
        }

        private void OnDestroy()
        {
            _hero.Progress.OnLevelUped -= OnLevelUped;
        }

        public void Toggle()
        {
            if (_isShowing)
                Hide();
            else
                Show();
        }

        public void OnPointerDown(PointerEventData _)
        {
            Toggle();
        }

        private void OnLevelUped(HeroCard _)
        {
            SetActiveUpgradeButtonDependingOnPoints();
        }

        private void OnUpStatClicked()
        {
            _hero.Upgrade();
            Render();
        }

        private void SetActiveUpgradeButtonDependingOnPoints()
        {
            _upStat.gameObject.SetActive(_hero.EnoughPointsToUpgrade());
        }

        private void Show()
        {
            _isShowing = true;
            _showable.Show();
            _upStatRaycaster.enabled = true;
            Render();

            _hero.Stats.OnBaseStatChanged += OnBaseStatsChanged;
            _hero.Stats.OnModifierChanged += OnModifierChanged;
        }

        private void Hide()
        {
            _isShowing = false;
            _showable.Hide();
            _upStatRaycaster.enabled = false;

            _hero.Stats.OnBaseStatChanged -= OnBaseStatsChanged;
            _hero.Stats.OnBaseStatChanged -= OnBaseStatsChanged;
        }

        // To update then hero info opened and hero got new level.


        private void OnBaseStatsChanged(StatType _)
        {
            Render();
        }

        private void OnModifierChanged(StatModifierType _)
        {
            Render();
        }

        private void Render()
        {
            SetActiveUpgradeButtonDependingOnPoints();
            _description.SetText(_viewData.Description);
            _power.SetText(_hero.Stats.Final(StatType.Power).ToString());
        }
    }
}