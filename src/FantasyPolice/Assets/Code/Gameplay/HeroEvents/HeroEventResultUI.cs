using System;
using Model.Data;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.HeroEvents
{
    public class HeroEventResultUI : CustomUI, IItemRenderer<HeroEventResultData>
    {
        [SerializeField] private TMP_Text _missionCompletedLabel;
        [SerializeField] private TMP_Text _targetPower;
        [SerializeField] private TMP_Text _heroPower;
        [SerializeField] private TMP_Text _successLabel;
        [SerializeField] private Button _continueButton;

        private IHeroEventsService _heroEventsService;

        private HeroEventResultData _data { get; set; }

        [Inject]
        private void Construct(IHeroEventsService heroEventsService)
        {
            _heroEventsService = heroEventsService;
        }

        private const string MissionCompletedKey = "Mission completed";
        private const string OnSuccessKey = "Success!";
        private const string OnFailedKey = "Failed!";
        public event Action<string> OnContinueClicked;


        private void OnEnable()
        {
            _continueButton.onClick.AddListener(HandleOnContinueClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(HandleOnContinueClicked);
        }

        private void HandleOnContinueClicked()
        {
            Hide();
            OnContinueClicked?.Invoke(_data.Id);
        
            _heroEventsService.FinishEvent(_data.Id);
        }

        public void Render(HeroEventResultData data)
        {
            Show();
            _data = data;
            _missionCompletedLabel.SetText(MissionCompletedKey);
            _targetPower.SetText($"Need power: {data.TargetPower.ToString()}");
            _heroPower.SetText($"Hero power: {data.HeroPower.ToString()}");
            _successLabel.SetText(data.IsHeroWin ? OnSuccessKey : OnFailedKey);
        }
    }

    public class HeroEventResultData
    {
        public string Id { get; private set; }
        public int TargetPower { get; private set; }
        public int HeroPower { get; private set; }
        public bool IsHeroWin { get; private set; }
    

        // Is hero win can be calculated based on power, but isHeroWin conditions can be changed in feature from service.

        public HeroEventResultData(string id, int targetPower, int heroPower, bool isHeroWin)
        {
            Id = id;
            TargetPower = targetPower;
            HeroPower = heroPower;
            IsHeroWin = isHeroWin;
        }
    }
}