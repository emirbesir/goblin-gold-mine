using System;
using Zenject;
using _Project.Shared.SceneLoader;

namespace _Project._GameName.Bootstrap.Bootstrap.Controller
{
    public class BootstrapController : IInitializable, IDisposable
    {
        public void Initialize()
        {
            InitializeGameScene();
        }

        public void Dispose()
        {
        }

        private void InitializeMenuScene()
        {
            SceneLoaderService.LoadScene(SceneType.Menu);
        }

        private void InitializeGameScene()
        {
            SceneLoaderService.LoadScene(SceneType.Game);
        }
    }
}