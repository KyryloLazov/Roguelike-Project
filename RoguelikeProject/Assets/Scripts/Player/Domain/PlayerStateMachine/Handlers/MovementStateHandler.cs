using Player.Domain.PlayerStateMachine.States;
using UnityEngine;

namespace Player.Domain.PlayerStateMachine.Handlers
{
    public class MovementStateHandler : IStateHandle
    {
        private readonly InitializationPlayerStateMachine _stateMachine;

        public InitializationPlayerStateMachine StateMachine => _stateMachine;

        public MovementStateHandler(InitializationPlayerStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public bool CanHandle()
        {
            return !_stateMachine.PlayerStateMachineData.IsDashing.Value 
                   && !_stateMachine.PlayerStateMachineData.IsDead.Value 
                   && !_stateMachine.PlayerStateMachineData.IsStunned.Value
                   && _stateMachine.PlayerStateMachineData.MovementInput.Value != Vector2.zero;
        }

        public void Handle()
        {
            _stateMachine.PlayerStateMachine.SwitchStates<MovementState>();
        }
    }
}