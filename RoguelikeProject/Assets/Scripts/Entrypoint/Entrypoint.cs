using System;
using Player.Domain.PlayerStateMachine;
using UnityEngine;
using Zenject;

namespace Entrypoint
{
    public class Entrypoint : MonoBehaviour
    {
        private InitializationPlayerStateMachine _fsm;

        [Inject]
        private void Construct(InitializationPlayerStateMachine fsm)
        {
            _fsm = fsm;
        }

        private void Start()
        {
            _fsm.Initialize();
        }

        private void Update()
        {
            _fsm.PlayerStateMachine.currentStates?.Update();
            Debug.Log($"State: {_fsm.PlayerStateMachine.currentStates}"); 
        }
        
        public void FixedUpdate()
        {
            _fsm.PlayerStateMachine.currentStates?.FixedUpdate();
        }
    }
}