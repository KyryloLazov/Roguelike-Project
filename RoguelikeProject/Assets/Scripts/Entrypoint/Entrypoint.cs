using System;
using Gameflow.Domain;
using Player.Domain.PlayerStateMachine;
using UI.GameOver;
using UI.PlayerHUD;
using UnityEngine;
using Zenject;

namespace Entrypoint
{
    public class Entrypoint : MonoBehaviour
    {
        private InitializationPlayerStateMachine _fsm;
        private GamePhaseService _gamePhaseService;
        private PlayerHUDPresenter _hud;
        private GameOverPresenter _gameOver;
        private AudioHub _audioHub;

        [Inject]
        private void Construct(InitializationPlayerStateMachine fsm, GamePhaseService gamePhaseService,
            PlayerHUDPresenter hud, GameOverPresenter gameOver, AudioHub audioHub)
        {
            _fsm = fsm;
            _gamePhaseService = gamePhaseService;
            _hud = hud;
            _gameOver = gameOver;
            _audioHub = audioHub;
        }

        private void Start()
        {
            _fsm.Initialize();
            _gamePhaseService.Initialize();
            _hud.Initialize();
            _gameOver.Initialize();
            _audioHub.Init();
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