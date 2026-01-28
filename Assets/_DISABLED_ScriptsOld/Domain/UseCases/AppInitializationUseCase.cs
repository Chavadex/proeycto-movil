using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using Zenject;
using chava.domain;

namespace chava.domain
{
    public class AppInitializationUseCase
    {
        private readonly IPreLoaderService _preLoaderService;
        private readonly ISceneLoad _sceneLoad;
        private readonly ILocalDataAccess _localDataAccess;
        private readonly IShowView _logoShowView;
        private readonly IEventDispatcher _eventDispatcher;

        public AppInitializationUseCase(IPreLoaderService preLoaderService,
            ISceneLoad sceneLoad, [Inject(Id = 2)] ILocalDataAccess localDataAccess,
            [Inject(Id = 3)] IShowView logoShowView, IEventDispatcher eventDispatcher)
        {
            _preLoaderService = preLoaderService;
            _sceneLoad = sceneLoad;
            _localDataAccess = localDataAccess;
            _logoShowView = logoShowView;
            _eventDispatcher = eventDispatcher;
            Initialize().Forget();
        }

        private async UniTaskVoid Initialize()
        {
            var preload = _preLoaderService.Preload();
            var sceneLoad = _sceneLoad.LoadScene("Menu");
            await Task.Delay(2000);
            await UniTask.WhenAll(preload, sceneLoad);
            if (!_localDataAccess.IsNewUser())
            {
                _localDataAccess.SetDefaultConfig();
            }

            //initializing rest of app . ....... 

            // _sceneLoad.ActivateScene();
            _logoShowView.Show();
            _eventDispatcher.Dispatch(new AppInitialized("success"));
        }
    }
}
