using Player.Domain.PlayerStateMachine.States;
using UnityEngine;

namespace Player.Domain.PlayerStateMachine.Handlers
{
    public class DashStateHandler : IStateHandle
    {
        private readonly InitializationPlayerStateMachine _stateMachine;

        public InitializationPlayerStateMachine StateMachine => _stateMachine;

        public DashStateHandler(InitializationPlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public bool CanHandle()
        {
            return _stateMachine.PlayerStateMachineData.DashRequested.Value 
                   && _stateMachine.PlayerStateMachineData.DashCooldownTimer <= 0 
                   && _stateMachine.PlayerStateMachineData.MovementInput != Vector2.zero;
        }

        public void Handle()
        {
            _stateMachine.PlayerStateMachineData.DashRequested.Value = false;
            _stateMachine.PlayerStateMachine.SwitchStates<DashState>();
        }
    }
}