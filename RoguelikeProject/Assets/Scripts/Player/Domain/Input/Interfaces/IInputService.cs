using System;
using UniRx;
using UnityEngine;

namespace Player.Domain.Input
{
    public interface IInputService
    {
        Vector2 MovementInput { get; }
        Vector2 MousePosition { get; }
        IObservable<Unit> OnDashPressed { get; }
        IObservable<Unit> OnFirePressed { get; }
        bool IsFireHeld { get; }
    }
}