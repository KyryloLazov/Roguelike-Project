using Player.Domain.Events;
using Player.Infrastructure.Config;
using Player.Presentation;
using UnityEngine;

namespace Player.Domain.PlayerStateMachine.States
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(
            InitializationPlayerStateMachine stateMachine,
            PlayerStateMachineData stateData,
            PlayerMover mover,
            PlayerEvents playerEvents)
            : base(stateMachine, stateData, mover, playerEvents) { }

        public override void OnEnter()
        {
            base.OnEnter();
            PlayerMover.Stop();
        }
        
        public override void Update()
        {
            base.Update();
            PlayerEvents.OnIdleTick.OnNext(Time.deltaTime);
        }
    }
}