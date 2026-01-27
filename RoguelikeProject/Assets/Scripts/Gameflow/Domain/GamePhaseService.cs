using System;
using Gameflow.Presentation;
using WaveControl.Domain;
using WaveControl.Infrastructure.Config;
using Zenject;

namespace Gameflow.Domain
{
    public class GamePhaseService : IDisposable
    {
        private readonly WaveController _waveController;
        private readonly RewardUI _rewardUI;
        private readonly WavesConfig _config;
        
        public GamePhaseService(WaveController waveController, RewardUI rewardUI, WavesConfig config)
        {
            _waveController = waveController;
            _rewardUI = rewardUI;
            _config = config;
        }

        public void Initialize()
        {
            _waveController.OnWaveCompleted += EnableRewardPhase;
            _rewardUI.OnRewardSelected += StartWavePhase;
            
            StartWavePhase();
        }

        private void EnableRewardPhase()
        {
            _rewardUI.Show(_config.ItemRewardCount);
        }

        private void StartWavePhase()
        {
            _rewardUI.Hide();
            _waveController.StartNextWave();
        }

        public void Dispose()
        {
            _waveController.OnWaveCompleted -= EnableRewardPhase;
            _rewardUI.OnRewardSelected -= StartWavePhase;
        }
    }
}