using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.HeroBased;
using Gameplay.HeroBased.StatsBased.Configs;
using Model.Data;
using UnityEngine;

namespace Gameplay.HeroEvents
{
    public class HeroEvent : IHaveId
    {
        public HeroCard CurrentHeroCard { get; private set; }
        public HeroEventData Data { get; private set; }

        public bool IsEventStarted { get; private set; }
    
        // I mean it's ez to undestand, but IsInProcess coz finish step.
        public bool IsTimerActive { get; private set; }
        public bool IsHeroWin { get; private set; }
        public Vector3 At { get; private set; }

        private CancellationTokenSource _cancellationTokenSource;

        public event Action<float, float> OnTimeUpdated;
        public event Action<string> OnTimesUp;
        public event Action<HeroEvent> OnCompleted;
        public event Action<HeroEvent> OnFailed;
        public event Action<HeroEvent, bool> OnFinished;

        private const float MinTime = 0f;
        public string Id => Data.Id;


        public HeroEvent(HeroEventData data, Vector3 at = default)
        {
            Data = data;
            At = at;
        }

        public void Send(HeroCard heroCard)
        {
            if (IsTimerActive)
            {
                Debug.LogError($"Already have a {heroCard.Id} on this event.");
                return;
            }

            IsEventStarted = true;
            IsTimerActive = true;

            CurrentHeroCard = heroCard;

            heroCard.GoToEvent();

            StartTimer().Forget();
        }

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
            Cleanup();
        }

        private async UniTask StartTimer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var elapsedSeconds = Data.TimeInSeconds;

            try
            {
                while (elapsedSeconds > 0)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                    elapsedSeconds -= Time.deltaTime;
                    elapsedSeconds = Mathf.Max(MinTime, elapsedSeconds);
                    var normalizedValue = elapsedSeconds / Data.TimeInSeconds;
                    CurrentHeroCard.ProgressBar.Render(normalizedValue);
                    
                    OnTimeUpdated?.Invoke(elapsedSeconds, Data.TimeInSeconds);
                }

                if (_cancellationTokenSource.IsCancellationRequested == false)
                    HandleOnTimesUp();
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning($"The event with {Data.Id} was cancelled.");
            }
            finally
            {
            }
        }

        private void HandleOnTimesUp()
        {
            IsTimerActive = false;
        
            IsHeroWin = CurrentHeroCard.Stats.Final(StatType.Power) >= Data.PowerToComplete;
        
            if (IsHeroWin)
                CompleteEvent();
            else
                FailEvent();
        
            OnTimesUp?.Invoke(Id);
        }

        private void CompleteEvent()
        {
            OnCompleted?.Invoke(this);
        }

        private void FailEvent()
        {
            OnFailed?.Invoke(this);
        }

        public void FinishEvent()
        {
            CurrentHeroCard.ExitFromEvent();
            OnFinished?.Invoke(this, IsHeroWin);
        }

        private void Cleanup()
        {
            IsTimerActive = false;
            CurrentHeroCard = null;

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}