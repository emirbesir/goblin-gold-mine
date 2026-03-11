using System.Collections.Generic;
using _Project.GoblinMine.Game.Bootstrap.Controller;
using _Project.GoblinMine.Game.Inventory.Command;
using _Project.GoblinMine.Game.Inventory.Controller;
using _Project.GoblinMine.Game.Inventory.Repository;
using _Project.GoblinMine.Game.Player.Command;
using _Project.GoblinMine.Game.Player.Configuration;
using _Project.GoblinMine.Game.Player.Controller;
using _Project.GoblinMine.Game.Player.Repository;
using _Project.GoblinMine.Game.Player.View;
using _Project.GoblinMine.Game.MiningResource.Command;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Controller;
using _Project.GoblinMine.Game.MiningResource.Repository;
using _Project.GoblinMine.Game.MiningResource.View;
using UnityEngine;
using Zenject;
using _Project.GoblinMine.Game.Inventory.View;

namespace _Project.GoblinMine.Game.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Configurations")]
        [SerializeField] private PlayerConfiguration playerConfiguration;
        [SerializeField] private MiningResourceConfigurationCollection resourceConfigurationCollection;
        [SerializeField] private MiningResourceVisualConfiguration miningResourceVisualConfiguration;
        [SerializeField] private ResourceChunkVisualConfiguration resourceChunkVisualConfiguration;

        [Header("Views")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private ResourceChunkView resourceChunkView;
        [SerializeField] private ResourceView resourceView;
        [SerializeField] private List<MiningResourceView> miningResourceViews;

        [Header("Scene References")]
        [SerializeField] private Transform resourceChunkViewContainer;
        [SerializeField] private Transform resourceViewContainer;

        [Header("External References")]
        [SerializeField] private Joystick joystick;

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
            Container.Bind<MiningResourceConfigurationCollection>().FromInstance(resourceConfigurationCollection).AsSingle().NonLazy();
            Container.Bind<MiningResourceVisualConfiguration>().FromInstance(miningResourceVisualConfiguration).AsSingle().NonLazy();
            Container.Bind<ResourceChunkVisualConfiguration>().FromInstance(resourceChunkVisualConfiguration).AsSingle().NonLazy();
        }

        private void BindRepositories()
        {
            Container.Bind<PlayerRepository>().AsSingle().NonLazy();
            Container.Bind<MiningResourceRepository>().AsSingle().NonLazy();
            Container.Bind<MiningResourceViewRepository>().AsSingle().NonLazy();
            Container.Bind<InventoryRepository>().AsSingle().NonLazy();
            Container.Bind<InventoryViewRepository>().AsSingle().NonLazy();
        }

        private void BindCommands()
        {
            Container.Bind<CreatePlayerModelCommand>().AsSingle().NonLazy();
            Container.Bind<MovePlayerCommand>().AsSingle().NonLazy();
            Container.Bind<GetMoveDirectionCommand>().AsSingle().NonLazy();
            Container.Bind<CreateResourceModelCommand>().AsSingle().NonLazy();
            Container.Bind<CreateMiningResourceModelCommand>().AsSingle().NonLazy();
            Container.Bind<SpawnResourceChunksCommand>().AsSingle().NonLazy();
            Container.Bind<InitializeMiningResourcesCommand>().AsSingle().NonLazy();
            Container.Bind<CollectMiningResourceCommand>().AsSingle().NonLazy();
            Container.Bind<RespawnMiningResourceCommand>().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<PlayerView>().FromInstance(playerView).AsSingle().NonLazy();
            Container.Bind<List<MiningResourceView>>().FromInstance(miningResourceViews).AsSingle().NonLazy();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InventoryController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MiningResourceController>().AsSingle().NonLazy();
        }

        private void BindExternalReferences()
        {
            Container.Bind<Joystick>().FromInstance(joystick).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.BindFactory<ResourceView, ResourceView.Factory>()
                .FromComponentInNewPrefab(resourceView)
                .UnderTransform(resourceViewContainer);
        }

        private void BindMemoryPools()
        {
            Container.BindMemoryPool<ResourceChunkView, ResourceChunkView.Pool>()
                .WithInitialSize(50)
                .FromComponentInNewPrefab(resourceChunkView)
                .UnderTransform(resourceChunkViewContainer);
        }
    }
}
