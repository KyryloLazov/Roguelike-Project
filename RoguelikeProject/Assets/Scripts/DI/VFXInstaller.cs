using UnityEngine;

namespace DI
{
    public class VFXInstaller : BaseBindings
    {
        [SerializeField] private VFXManager _vfxManager;
        [SerializeField] private VFXConfig _vfxConfig;

        public override void InstallBindings()
        {
            BindInstance(_vfxManager);
            BindInstance(_vfxConfig);
        }
    }
}