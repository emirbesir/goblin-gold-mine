using _Project.GoblinMine.Menu.Bootstrap;
using Zenject;

namespace _Project.GoblinMine.Menu.Installer
{
    public class MenuInstaller : MonoInstaller
    {
        // Configurations
        // [SerializeField] private AbilityCollectionConfiguration abilityCollectionConfiguration;

        // Views
        // [SerializeField] private CombatHudView combatHudView;

        public override void InstallBindings()
        {
            BindConfigurations();
            BindRepositories();
            BindCommands();
            BindViews();
            BindControllers();
        }

        private void BindConfigurations()
        {
            // Container.Bind<AbilityCollectionConfiguration>().FromInstance(abilityCollectionConfiguration).AsSingle().NonLazy();
        
        }

        private void BindRepositories()
        {
            // Container.Bind<AbilityRepository>().AsSingle().NonLazy();
        
        }

        private void BindCommands()
        {
            // Container.Bind<SelectAbilityCommand>().AsSingle().NonLazy();

        }

        private void BindViews()
        {
            // Container.Bind<CombatHudView>().FromInstance(combatHudView).AsSingle().NonLazy();

        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<MenuBootstrapController>().AsSingle().NonLazy();
        }
    }
}