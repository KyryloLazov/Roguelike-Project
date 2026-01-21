using Player.Infrastructure.Config;
using Player.Presentation;

namespace Player.Domain.PlayerStateMachine.States
{
    public class MovementState : PlayerBaseState
    {
        public MovementState(
            InitializationPlayerStateMachine stateMachine,
            PlayerStateMachineData stateData,
            PlayerMover mover)
            : base(stateMachine, stateData, mover) { }

        public override void FixedUpdate()
        {
            float speed = _stateData.Stats.GetStat(StatType.Speed).Value;

            var input = _stateData.MovementInput;
            var dir = PlayerMover.GetWorldMovementDirection(input);

            PlayerMover.Move(dir * speed);
        }
    }
}