using System;
using Player.Domain.PlayerStats;
using Player.Domain.PlayerStatus;
using Player.Infrastructure.Config;
using UniRx;

namespace UI.PlayerHUD
{
    public class PlayerHUDPresenter : IDisposable
    {
        private readonly PlayerHUDView _view;
        private readonly PlayerStatusModel _statusModel;
        private readonly IPlayerStatsProvider _statsProvider;
        private readonly CompositeDisposable _disposables = new();

        public PlayerHUDPresenter(
            PlayerHUDView view, 
            PlayerStatusModel statusModel, 
            IPlayerStatsProvider statsProvider)
        {
            _view = view;
            _statusModel = statusModel;
            _statsProvider = statsProvider;
        }

        public void Initialize()
        {
            _statusModel.CurrentHealth
                .CombineLatest(_statsProvider.GetStat(StatType.MaxHealth), (current, max) => new { current, max })
                .Subscribe(x => _view.SetHealth(x.current, x.max))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}