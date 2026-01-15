using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Model.Data
{
    public interface IMoveable
    {
        bool IsMoving { get; }
        UniTask MoveTo(Vector3 at);
        event Action<IMoveable> OnMoveStarted;
        event Action<IMoveable> OnMoveEnded;
    }

    public interface IMovementStrategy : IWorkable
    {
        UniTask Move(Vector3 at, CancellationToken token = default);
    }

    public interface IWorkable
    {
        bool IsWorking { get; }
        void Cancel();
    }
}