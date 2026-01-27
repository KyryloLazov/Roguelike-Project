using Player.Domain.Events;
using Player.Infrastructure.Config;
using Player.Presentation;

namespace Player.Domain.PlayerStateMachine.States
{
    public class MovementState : PlayerBaseState
    {
        public MovementState(
            InitializationPlayerStateMachine stateMachine,
            PlayerStateMachineData stateData,
            PlayerMover mover,
            PlayerEvents playerEvents)
            : base(stateMachine, stateData, mover, playerEvents) { }

        public override void FixedUpdate()
        {
            float speed = StateData.Stats.GetStat(StatType.Speed).Value;

            var input = StateData.MovementInput.Value;
            var dir = PlayerMover.GetWorldMovementDirection(input);

            PlayerMover.Move(dir * speed);
        }
    }
}