using Player.Infrastructure.Config;
using Cysharp.Threading.Tasks;
using Player.Domain.Events;
using Player.Presentation;
using UnityEngine;

namespace Player.Domain.PlayerStateMachine.States
{
    public class DashState : PlayerBaseState
    {
        private Vector3 _dashDirection;

        public DashState(
            InitializationPlayerStateMachine stateMachine,
            PlayerStateMachineData stateData,
            PlayerMover mover,
            PlayerEvents playerEvents)
            : base(stateMachine, stateData, mover, playerEvents) { }

        public override void OnEnter()
        {
            _stateData.IsDashing.Value = true;
            PlayerEvents.OnDashStarted.OnNext(UniRx.Unit.Default);
            
            var input = _stateData.MovementInput;
            _dashDirection = PlayerMover.GetWorldMovementDirection(input);

            if (_dashDirection.sqrMagnitude < 0.01f)
                _dashDirection = PlayerMover.transform.forward;

            _dashDirection.Normalize();

            StartDashRoutine().Forget();
        }

        private async UniTaskVoid StartDashRoutine()
        {
            await UniTask.WaitForSeconds(0.2f);

            _stateData.IsDashing.Value = false;
            _stateData.DashCooldownTimer = 1.0f;
        }

        public override void FixedUpdate()
        {
            float baseSpeed = _stateData.Stats.GetStat(StatType.Speed).Value;
            float dashSpeed = baseSpeed * 6f;
            
            var velocity = _dashDirection * dashSpeed;
            PlayerMover.Move(velocity);
        }

        public override void OnExit()
        {
            PlayerMover.Stop();
        }
    }
}