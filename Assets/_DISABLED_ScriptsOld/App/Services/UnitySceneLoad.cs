using chava.domain;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace chava.app.Services
{
    public class UnitySceneLoad : ISceneLoad
    {
        private AsyncOperation _asyncOperation;
        public async UniTask LoadScene(string sceneName)
        {
            await Task.Delay(500);
            _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            _asyncOperation.allowSceneActivation = false;//<<<----

            while (_asyncOperation.progress <= 0.89f)
            {
                await UniTask.Yield();
            }

        }

        public void ActivateScene()
        {
            Debug.Log("Activating Scene");
            _asyncOperation.allowSceneActivation = true;
        }
    }
}
