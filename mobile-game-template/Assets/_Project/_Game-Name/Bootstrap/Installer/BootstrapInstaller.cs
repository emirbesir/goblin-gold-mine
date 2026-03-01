using _Project._GameName.Bootstrap.Bootstrap.Controller;
using Zenject;

namespace _Project._GameName.Bootstrap.Installer
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<BootstrapController>().AsSingle().NonLazy();
        }

        public override void Start()
        {
            base.Start();
        }
    }
}