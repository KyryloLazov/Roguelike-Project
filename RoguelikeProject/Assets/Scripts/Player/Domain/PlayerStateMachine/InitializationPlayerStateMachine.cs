using Player.Domain.PlayerStateMachine.States;
using Player.Presentation;

namespace Player.Domain.PlayerStateMachine
{
    public class InitializationPlayerStateMachine
    {
        public StateMachine PlayerStateMachine { get; private set; }
        public PlayerStateMachineData PlayerStateMachineData { get; private set; }
        public PlayerMover PlayerMover { get; private set; }

        private IdleState _idleState;
        private MovementState _movementState;
        private DashState _dashState;

        public InitializationPlayerStateMachine(PlayerMover mover, PlayerStateMachineData playerStateMachineData)
        {
            PlayerMover = mover;
            PlayerStateMachineData = playerStateMachineData;
        }

        public void Initialize()
        {
            _idleState = new IdleState(this, PlayerStateMachineData, PlayerMover);
            _movementState = new MovementState(this, PlayerStateMachineData, PlayerMover);
            _dashState = new DashState(this, PlayerStateMachineData, PlayerMover);
            PlayerStateMachine = new StateMachine(_idleState, _movementState, _dashState);
            PlayerStateMachine.SwitchStates<IdleState>();
        }
    }
}