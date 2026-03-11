using System.Collections.Generic;
using _Project.GoblinMine.Game.Bootstrap.Controller;
using _Project.GoblinMine.Game.Depot.Command;
using _Project.GoblinMine.Game.Depot.Controller;
using _Project.GoblinMine.Game.Depot.View;
using _Project.GoblinMine.Game.Gold.Command;
using _Project.GoblinMine.Game.Gold.Controller;
using _Project.GoblinMine.Game.Gold.Repository;
using _Project.GoblinMine.Game.Gold.View;
using _Project.GoblinMine.Game.Haptic.Command;
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
using _Project.GoblinMine.Game.Region.Command;
using _Project.GoblinMine.Game.Region.Configuration;
using _Project.GoblinMine.Game.Region.Controller;
using _Project.GoblinMine.Game.Region.Repository;
using _Project.GoblinMine.Game.Region.View;
using _Project.GoblinMine.Game.Upgrade.Command;
using _Project.GoblinMine.Game.Upgrade.Configuration;
using _Project.GoblinMine.Game.Upgrade.Controller;
using _Project.GoblinMine.Game.Upgrade.Repository;
using _Project.GoblinMine.Game.Upgrade.View;
using _Project.GoblinMine.Game.Worker.Command;
using _Project.GoblinMine.Game.Worker.Configuration;
using _Project.GoblinMine.Game.Worker.Controller;
using _Project.GoblinMine.Game.Worker.Repository;
using _Project.GoblinMine.Game.Worker.View;
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
        [SerializeField] private UpgradeConfiguration upgradeConfiguration;
        [SerializeField] private RegionConfiguration regionConfiguration;

        [Header("Worker")]
        [SerializeField] private WorkerConfiguration workerConfiguration;
        [SerializeField] private WorkerVisualConfiguration workerVisualConfiguration;
        [SerializeField] private List<WorkerView> workerViews;
        [SerializeField] private WorkerView workerViewPrefab;

        [Header("Views")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private ResourceChunkView resourceChunkView;
        [SerializeField] private ResourceView resourceView;
        [SerializeField] private List<MiningResourceView> miningResourceViews;
        [SerializeField] private GoldView goldView;
        [SerializeField] private CarryCapacityView carryCapacityView;
        [SerializeField] private DepotView depotView;
        [SerializeField] private UpgradeStationView upgradeStationView;
        [SerializeField] private UpgradePanelView upgradePanelView;
        [SerializeField] private List<RegionGateView> regionGateViews;

        [Header("Scene References")]
        [SerializeField] private Transform resourceChunkViewContainer;
        [SerializeField] private Transform resourceViewContainer;
        [SerializeField] private Transform workerViewContainer;

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
            Container.Bind<WorkerConfiguration>().FromInstance(workerConfiguration).AsSingle().NonLazy();
            Container.Bind<WorkerVisualConfiguration>().FromInstance(workerVisualConfiguration).AsSingle().NonLazy();
            Container.Bind<UpgradeConfiguration>().FromInstance(upgradeConfiguration).AsSingle().NonLazy();
            Container.Bind<RegionConfiguration>().FromInstance(regionConfiguration).AsSingle().NonLazy();
        }

        private void BindRepositories()
        {
            Container.Bind<PlayerRepository>().AsSingle().NonLazy();
            Container.Bind<MiningResourceRepository>().AsSingle().NonLazy();
            Container.Bind<MiningResourceViewRepository>().AsSingle().NonLazy();
            Container.Bind<InventoryRepository>().AsSingle().NonLazy();
            Container.Bind<InventoryViewRepository>().AsSingle().NonLazy();
            Container.Bind<WorkerRepository>().AsSingle().NonLazy();
            Container.Bind<WorkerViewRepository>().AsSingle().NonLazy();
            Container.Bind<GoldRepository>().AsSingle().NonLazy();
            Container.Bind<UpgradeRepository>().AsSingle().NonLazy();
            Container.Bind<RegionRepository>().AsSingle().NonLazy();
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
            Container.Bind<CreateWorkerModelCommand>().AsSingle().NonLazy();
            Container.Bind<InitializeWorkersCommand>().AsSingle().NonLazy();
            Container.Bind<WorkerMineCommand>().AsSingle().NonLazy();
            Container.Bind<SpawnWorkerCommand>().AsSingle().NonLazy();
            Container.Bind<CreateGoldModelCommand>().AsSingle().NonLazy();
            Container.Bind<EarnGoldCommand>().AsSingle().NonLazy();
            Container.Bind<SpendGoldCommand>().AsSingle().NonLazy();
            Container.Bind<DepositResourcesCommand>().AsSingle().NonLazy();
            Container.Bind<CreateUpgradeModelsCommand>().AsSingle().NonLazy();
            Container.Bind<PurchaseUpgradeCommand>().AsSingle().NonLazy();
            Container.Bind<UnlockRegionCommand>().AsSingle().NonLazy();
            Container.Bind<TriggerHapticCommand>().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<PlayerView>().FromInstance(playerView).AsSingle().NonLazy();
            Container.Bind<List<MiningResourceView>>().FromInstance(miningResourceViews).AsSingle().NonLazy();
            Container.Bind<List<WorkerView>>().FromInstance(workerViews).AsSingle().NonLazy();
            Container.Bind<GoldView>().FromInstance(goldView).AsSingle().NonLazy();
            Container.Bind<CarryCapacityView>().FromInstance(carryCapacityView).AsSingle().NonLazy();
            Container.Bind<DepotView>().FromInstance(depotView).AsSingle().NonLazy();
            Container.Bind<UpgradeStationView>().FromInstance(upgradeStationView).AsSingle().NonLazy();
            Container.Bind<UpgradePanelView>().FromInstance(upgradePanelView).AsSingle().NonLazy();
            Container.Bind<List<RegionGateView>>().FromInstance(regionGateViews).AsSingle().NonLazy();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InventoryController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MiningResourceController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WorkerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GoldController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DepotController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UpgradeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RegionController>().AsSingle().NonLazy();
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

            Container.BindFactory<WorkerView, WorkerView.Factory>()
                .FromComponentInNewPrefab(workerViewPrefab)
                .UnderTransform(workerViewContainer);
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
