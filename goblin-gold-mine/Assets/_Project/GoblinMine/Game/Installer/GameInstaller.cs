using _Project.GoblinMine.Game.Bootstrap.Controller;
using _Project.GoblinMine.Game.Player.Command;
using _Project.GoblinMine.Game.Player.Configuration;
using _Project.GoblinMine.Game.Player.Controller;
using _Project.GoblinMine.Game.Player.Repository;
using _Project.GoblinMine.Game.Player.View;
using _Project.GoblinMine.Game.Resource.Command;
using _Project.GoblinMine.Game.Resource.Configuration;
using _Project.GoblinMine.Game.Resource.Controller;
using _Project.GoblinMine.Game.Resource.Repository;
using _Project.GoblinMine.Game.Resource.View;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Configurations")]
        [SerializeField] private PlayerConfiguration playerConfiguration;
        [SerializeField] private ResourceConfigurationCollection resourceConfigurationCollection;

        [Header("Views")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private ResourceView resourceView;

        [Header("Scene References")]
        [SerializeField] private Transform resourceViewContainer;

        [Header("External References")]
        [SerializeField] private DynamicJoystick dynamicJoystick;

        public override void InstallBindings()
        {
            BindConfigurations();
            BindRepositories();
            BindCommands();
            BindControllers();
            BindViews();
            BindExternalReferences();
            BindFactories();
            BindMemoryPools();               
        }

        private void BindConfigurations()
        {
            Container.Bind<PlayerConfiguration>().FromInstance(playerConfiguration).AsSingle().NonLazy();
            Container.Bind<ResourceConfigurationCollection>().FromInstance(resourceConfigurationCollection).AsSingle().NonLazy();
        }

        private void BindRepositories()
        {
            Container.Bind<PlayerRepository>().AsSingle().NonLazy();
            Container.Bind<ResourceRepository>().AsSingle().NonLazy();
            Container.Bind<ResourceViewRepository>().AsSingle().NonLazy();
        }

        private void BindCommands()
        {
            Container.Bind<CreatePlayerModelCommand>().AsSingle().NonLazy();
            Container.Bind<MovePlayerCommand>().AsSingle().NonLazy();
            Container.Bind<GetMoveDirectionCommand>().AsSingle().NonLazy();
            Container.Bind<CollectResourceCommand>().AsSingle().NonLazy();
            Container.Bind<CreateResourceModelCommand>().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<PlayerView>().FromInstance(playerView).AsSingle().NonLazy();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ResourceController>().AsSingle().NonLazy();
        }

        private void BindExternalReferences()
        {
            Container.Bind<DynamicJoystick>().FromInstance(dynamicJoystick).AsSingle().NonLazy();
        }
        
        private void BindFactories()
        {
            Container.BindFactory<ResourceView, ResourceView.Factory>()
                .FromComponentInNewPrefab(resourceView)
                .UnderTransform(resourceViewContainer);
        }

        private void BindMemoryPools()
        {
            // Container.BindMemoryPool<ResourceView, ResourceView.Pool>()
            //     .WithInitialSize(10)
            //     .FromComponentInNewPrefab(resourceView)
            //     .UnderTransform(resourceViewContainer);
            //     .NonLazy();
        }
    }
}