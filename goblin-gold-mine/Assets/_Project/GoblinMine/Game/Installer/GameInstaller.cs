using _Project.GoblinMine.Game.Bootstrap;
using GoblinMine.Game.Player.Command;
using GoblinMine.Game.Player.Configuration;
using GoblinMine.Game.Player.Controller;
using GoblinMine.Game.Player.Repository;
using GoblinMine.Game.Player.View;
using UnityEngine;
using Zenject;

namespace _Project.GoblinMine.Game.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Configurations")]
        [SerializeField] private PlayerConfiguration playerConfiguration;

        [Header("Views")]
        [SerializeField] private PlayerView playerView;

        [Header("External References")]
        [SerializeField] private DynamicJoystick dynamicJoystick;

        public override void InstallBindings()
        {
            BindConfigurations();
            BindRepositories();
            BindCommands();
            BindViews();
            BindControllers();
            BindExternalReferences();
        }

        private void BindConfigurations()
        {
            Container.Bind<PlayerConfiguration>().FromInstance(playerConfiguration).AsSingle().NonLazy();
        }

        private void BindRepositories()
        {
            // Container.Bind<AbilityRepository>().AsSingle().NonLazy();
            Container.Bind<PlayerRepository>().AsSingle().NonLazy();

        }

        private void BindCommands()
        {
            // Container.Bind<SelectAbilityCommand>().AsSingle().NonLazy();
            Container.Bind<CreatePlayerModelCommand>().AsSingle().NonLazy();
            Container.Bind<MovePlayerCommand>().AsSingle().NonLazy();
            Container.Bind<GetNormalizedMoveDirectionCommand>().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.Bind<PlayerView>().FromInstance(playerView).AsSingle().NonLazy();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        }

        private void BindExternalReferences()
        {
            Container.Bind<DynamicJoystick>().FromInstance(dynamicJoystick).AsSingle().NonLazy();
        }
    }
}