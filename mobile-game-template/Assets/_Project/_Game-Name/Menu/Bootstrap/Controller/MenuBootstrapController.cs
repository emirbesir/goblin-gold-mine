using System.Collections.Generic;
using _Project.Shared.Initializable;
using Zenject;

namespace _Project._GameName.Menu.Bootstrap
{
    public class MenuBootstrapController : IInitializable
    {
        private readonly List<IPreInitializable> _preInitializables;
        private readonly List<IPostInitializable> _postInitializables;

        public MenuBootstrapController(
            List<IPreInitializable> preInitializables,
            List<IPostInitializable> postInitializables)
        {
            _preInitializables = preInitializables;
            _postInitializables = postInitializables;
        }

        public void Initialize()
        {
            foreach (var preInitializer in _preInitializables)
            {
                preInitializer.PreInitialize();
            }

            foreach (var postInitializer in _postInitializables)
            {
                postInitializer.PostInitialize();
            }
        }
    }
}