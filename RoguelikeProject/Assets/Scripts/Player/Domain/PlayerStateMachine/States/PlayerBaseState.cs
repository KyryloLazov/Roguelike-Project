using Player.Domain.Events;
using Player.Domain.PlayerStateMachine.Handlers;
using Player.Presentation;

namespace Player.Domain.PlayerStateMachine.States
{
    public abstract class PlayerBaseState : IState
    {
        protected readonly InitializationPlayerStateMachine _stateMachine;
        protected readonly PlayerStateMachineData _stateData;
        protected readonly PlayerMover PlayerMover;
        protected readonly PlayerEvents PlayerEvents;

        protected PlayerBaseState(InitializationPlayerStateMachine stateMachine, PlayerStateMachineData stateData, 
            PlayerMover mover, PlayerEvents playerEvents)
        {
            _stateMachine = stateMachine;
            _stateData = stateData;
            PlayerMover = mover;
            PlayerEvents = playerEvents;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }

        public virtual void Update()
        {
            PlayerMover.StateHandleChain.HandleState<DashStateHandler>();
            PlayerMover.StateHandleChain.HandleState<IdleStateHandler>();
            PlayerMover.StateHandleChain.HandleState<MovementStateHandler>();
        }
        public virtual void FixedUpdate() { }
    }
}