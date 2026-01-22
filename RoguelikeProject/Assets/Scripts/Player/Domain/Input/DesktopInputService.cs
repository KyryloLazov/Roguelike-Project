using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Domain.Input
{
    public class DesktopInputService : IInputService, IDisposable
    {
        private readonly Subject<Unit> _dashSubject = new();
        private readonly Subject<Unit> _fireSubject = new();
        
        private readonly InputAction _move;
        private readonly InputAction _dash;
        private readonly InputAction _fire;
        private readonly InputAction _mousePos;

        private readonly PlayerControls _inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MousePosition { get; private set; }
        public bool IsFireHeld => _fire.IsPressed();

        public IObservable<Unit> OnDashPressed => _dashSubject;
        public IObservable<Unit> OnFirePressed => _fireSubject;

        public DesktopInputService()
        {
            _inputActions = new PlayerControls();

            _move = _inputActions.Player.Move;
            _dash = _inputActions.Player.Dash;
            _fire = _inputActions.Player.Fire; 
            _mousePos = _inputActions.Player.Look; 

            _dash.performed += _ => _dashSubject.OnNext(Unit.Default);
            _fire.performed += _ => _fireSubject.OnNext(Unit.Default);

            _move.performed += ctx =>
            {
                var raw = ctx.ReadValue<Vector2>();
                MovementInput = raw.normalized;
            };
            _move.canceled += _ => MovementInput = Vector2.zero;

            _mousePos.performed += ctx => MousePosition = ctx.ReadValue<Vector2>();

            _inputActions.Enable();
        }

        public void Dispose()
        {
            _inputActions.Dispose();
        }
    }
}