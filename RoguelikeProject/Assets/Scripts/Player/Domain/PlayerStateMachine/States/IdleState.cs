using Player.Infrastructure.Config;
using Player.Presentation;

namespace Player.Domain.PlayerStateMachine.States
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(
            InitializationPlayerStateMachine stateMachine,
            PlayerStateMachineData stateData,
            PlayerMover mover)
            : base(stateMachine, stateData, mover) { }

        public override void OnEnter()
        {
            base.OnEnter();
            PlayerMover.Stop();
        }
    }
}