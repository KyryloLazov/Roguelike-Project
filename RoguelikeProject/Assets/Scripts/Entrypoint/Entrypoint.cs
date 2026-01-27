using System;
using Gameflow.Domain;
using Player.Domain.PlayerStateMachine;
using UnityEngine;
using Zenject;

namespace Entrypoint
{
    public class Entrypoint : MonoBehaviour
    {
        private InitializationPlayerStateMachine _fsm;
        private GamePhaseService _gamePhaseService;

        [Inject]
        private void Construct(InitializationPlayerStateMachine fsm, GamePhaseService gamePhaseService)
        {
            _fsm = fsm;
            _gamePhaseService = gamePhaseService;
        }

        private void Start()
        {
            _fsm.Initialize();
            _gamePhaseService.Initialize();
        }

        private void Update()
        {
            _fsm.PlayerStateMachine.currentStates?.Update();
        }
        
        public void FixedUpdate()
        {
            _fsm.PlayerStateMachine.currentStates?.FixedUpdate();
        }
    }
}