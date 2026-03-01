using _Project.GoblinMine.Bootstrap.Bootstrap.Controller;
using Zenject;

namespace _Project.GoblinMine.Bootstrap.Installer
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
    }
}