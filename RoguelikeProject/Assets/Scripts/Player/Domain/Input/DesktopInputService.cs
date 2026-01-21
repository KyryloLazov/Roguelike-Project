using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Domain.Input
{
    public class DesktopInputService : IInputService, IDisposable
    {
        private readonly Subject<Unit> _dashSubject = new();
        private readonly InputAction _move;
        private readonly InputAction _dash;

        public Vector2 MovementInput { get; private set; }
        public IObservable<Unit> OnDashPressed => _dashSubject;

        private readonly PlayerControls _inputActions;

        public DesktopInputService()
        {
            _inputActions = new PlayerControls();

            _move = _inputActions.Player.Move;
            _dash = _inputActions.Player.Dash;

            _dash.performed += _ => _dashSubject.OnNext(Unit.Default);

            _move.performed += ctx =>
            {
                var raw = ctx.ReadValue<Vector2>();
                MovementInput = raw.normalized;
            };

            _move.canceled += _ => MovementInput = Vector2.zero;

            _inputActions.Enable();
        }

        public void Dispose()
        {
            _inputActions.Dispose();
        }
    }
}