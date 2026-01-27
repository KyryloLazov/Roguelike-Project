using UnityEngine;

namespace DI
{
    public class AudioInstaller : BaseBindings
    {
        [SerializeField] private AudioHub _audioHub;
        [SerializeField] private AudioConfigSO _audioConfig;

        public override void InstallBindings()
        {
            BindInstance(_audioHub);
            BindInstance(_audioConfig);
        }
    }
}