using System.Threading;
using Cysharp.Threading.Tasks;
using _Project.GoblinMine.Game.MiningResource.Configuration;
using _Project.GoblinMine.Game.MiningResource.Model;
using _Project.GoblinMine.Game.MiningResource.View;

namespace _Project.GoblinMine.Game.MiningResource.Command
{
    public class RespawnMiningResourceCommand
    {
        public async UniTask Execute(
            MiningResourceModel resource,
            MiningResourceView resourceView,
            MiningResourceConfiguration configuration,
            CancellationToken cancellationToken)
        {
            await UniTask.Delay(
                (int)(configuration.RespawnDelaySeconds * 1000),
                cancellationToken: cancellationToken);

            resource.RemainingDurability = configuration.Durability;
            resource.CollectionTimer = 0f;
            resourceView.SetActive(true);
        }
    }
}
