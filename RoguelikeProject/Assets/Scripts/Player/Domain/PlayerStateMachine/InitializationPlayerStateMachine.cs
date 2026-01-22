using Player.Domain.Events;
using Player.Domain.PlayerStateMachine.States;
using Player.Presentation;

namespace Player.Domain.PlayerStateMachine
{
    public class InitializationPlayerStateMachine
    {
        public StateMachine PlayerStateMachine { get; private set; }
        public PlayerStateMachineData PlayerStateMachineData { get; private set; }
        public PlayerMover PlayerMover { get; private set; }
        public PlayerEvents PlayerEvents { get; private set; }

        private IdleState _idleState;
        private MovementState _movementState;
        private DashState _dashState;

        public InitializationPlayerStateMachine(PlayerMover mover, PlayerStateMachineData playerStateMachineData, 
            PlayerEvents playerEvents)
        {
            PlayerMover = mover;
            PlayerStateMachineData = playerStateMachineData;
            PlayerEvents = playerEvents;
        }

        public void Initialize()
        {
            _idleState = new IdleState(this, PlayerStateMachineData, PlayerMover, PlayerEvents);
            _movementState = new MovementState(this, PlayerStateMachineData, PlayerMover, PlayerEvents);
            _dashState = new DashState(this, PlayerStateMachineData, PlayerMover, PlayerEvents);
            PlayerStateMachine = new StateMachine(_idleState, _movementState, _dashState);
            PlayerStateMachine.SwitchStates<IdleState>();
        }
    }
}