using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Shared.SceneLoader
{
    public static class SceneLoaderService
    {
        public static void LoadScene(SceneType sceneType)
        {
            var sceneName = sceneType.ToString();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public static void LoadSceneAdditively(SceneType sceneType)
        {
            var sceneName = sceneType.ToString();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public static async UniTask LoadSceneAsync(
            SceneType sceneType,
            IProgress<float> progress = null,
            CancellationToken cancellationToken = default)
        {
            var sceneName = sceneType.ToString();
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            if (asyncOperation == null)
                throw new Exception($"Failed to start loading scene '{sceneName}'.");

            while (!asyncOperation.isDone)
            {
                cancellationToken.ThrowIfCancellationRequested();
                progress?.Report(asyncOperation.progress);

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            progress?.Report(1f);
        }

        public static async UniTask LoadSceneAsyncAdditively(
            SceneType sceneType,
            IProgress<float> progress = null,
            CancellationToken cancellationToken = default)
        {
            var sceneName = sceneType.ToString();
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            if (asyncOperation == null)
                throw new Exception($"Failed to start loading scene '{sceneName}'.");

            while (!asyncOperation.isDone)
            {
                cancellationToken.ThrowIfCancellationRequested();
                progress?.Report(asyncOperation.progress);

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            progress?.Report(1f);
        }

        public static async UniTask UnloadScene(SceneType sceneType)
        {
            var sceneName = sceneType.ToString();
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);

            if (asyncOperation == null)
            {
                throw new Exception($"Failed to unload scene '{sceneName}'.");
            }

            await asyncOperation.ToUniTask();
        }
    }
}