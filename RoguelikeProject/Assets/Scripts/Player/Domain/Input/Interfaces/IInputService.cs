using System;
using UniRx;
using UnityEngine;

namespace Player.Domain.Input
{
    public interface IInputService
    {
        Vector2 MovementInput { get; }
        IObservable<Unit> OnDashPressed { get; }
    }
}
