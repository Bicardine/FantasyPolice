using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Model.Data;
using Model.Scriptable;
using UnityEngine;

namespace Components.NotMono.TransformBased.Movement
{
    public class OutExpoMovementStrategy : IMovementStrategy
    {
        private EaseDurationConfig _easeDurationConfig;
        private Transform _transform;
        private CancellationTokenSource _cts;
        private bool _isMoving;
    
        public bool IsWorking => _isMoving;

        public OutExpoMovementStrategy(EaseDurationConfig easeDurationConfig, Transform transform)
        {
            _easeDurationConfig = easeDurationConfig;
            _transform = transform;
        }

        public async UniTask Move(Vector3 at, CancellationToken token = default)
        {
            Cancel();
            
            _cts = CancellationTokenSource.CreateLinkedTokenSource(token);
            
            _isMoving = true;
            
            try
            {
                await _transform
                    .DOMove(at, _easeDurationConfig.Duration)
                    .SetEase(Ease.OutExpo)
                    .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, _cts.Token);
            }
            finally
            {
                _isMoving = false; _cts?.Dispose();
                _cts = null;
            }
        }

        public void Cancel()
        {
            if (_isMoving == false) return;
            
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            _isMoving = false;
        }
    }
}