using _Project.Shared.Initializable;
using _Project.GoblinMine.Game.Player.View;
using _Project.GoblinMine.Game.Resource.Command;
using _Project.GoblinMine.Game.Resource.Configuration;
using _Project.GoblinMine.Game.Resource.Repository;
using _Project.GoblinMine.Game.Resource.View;
using UnityEngine;

namespace _Project.GoblinMine.Game.Resource.Controller
{
    public class ResourceController : IPreInitializable
    {
        private readonly ResourceConfigurationCollection _resourceConfigurationCollection;
        private readonly ResourceRepository _resourceRepository;
        private readonly ResourceViewRepository _resourceViewRepository;
        private readonly CreateResourceModelCommand _createResourceModelCommand;
        private readonly CollectResourceCommand _collectResourceCommand;
        private readonly ResourceView.Factory _resourceViewFactory;

        public ResourceController(
            ResourceConfigurationCollection resourceConfigurationCollection,
            ResourceRepository resourceRepository,
            ResourceViewRepository resourceViewRepository,
            CreateResourceModelCommand createResourceModelCommand,
            CollectResourceCommand collectResourceCommand,
            ResourceView.Factory resourceViewFactory)
        {
            _resourceConfigurationCollection = resourceConfigurationCollection;
            _resourceRepository = resourceRepository;
            _resourceViewRepository = resourceViewRepository;
            _createResourceModelCommand = createResourceModelCommand;
            _collectResourceCommand = collectResourceCommand;
            _resourceViewFactory = resourceViewFactory;
        }

        public void PreInitialize()
        {
            foreach (var config in _resourceConfigurationCollection.Configurations)
            {
                var resource = _createResourceModelCommand.Execute(config);
                _resourceRepository.Resources.Add(resource);

                var resourceView = _resourceViewFactory.Create();
                resourceView.SetMaterial(config.Material);
                resourceView.Id = resource.Id;
                resourceView.OnTriggerStayAction += other => HandleTriggerStay(resourceView, other);
                resourceView.OnTriggerExitAction += other => HandleTriggerExit(resourceView, other);
                
                // For demo purposes, randomly position resources in the scene. 
                // In the real game, we will have predefined spawn points.
                resourceView.SetPosition(new Vector3(
                    UnityEngine.Random.Range(-8f, 8f),
                    0f,
                    UnityEngine.Random.Range(-8f, 8f)));

                _resourceViewRepository.Views.Add(resourceView);
            }
        }

        private void HandleTriggerStay(ResourceView resourceView, Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _)) return;
            
            var resource = _resourceRepository.GetResourceById(resourceView.Id);

            resource.CollectionTimer += Time.deltaTime;
            if (resource.CollectionTimer >= resource.CollectionIntervalSeconds)
            {
                _collectResourceCommand.Execute(resource, resourceView);
                resource.CollectionTimer -= resource.CollectionIntervalSeconds;
            }
        }

        private void HandleTriggerExit(ResourceView resourceView, Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out _)) return;

            var resource = _resourceRepository.GetResourceById(resourceView.Id);
            resource.CollectionTimer = 0f;
        }
    }
}