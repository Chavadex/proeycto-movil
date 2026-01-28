using chava.app;
using chava.domain;
using chava.entities;
using Cysharp.Threading.Tasks;
using chava.app.Server;
using chava.app.Server.DTO;
using chava.domain;
using chava.entities;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace chava.app.Repository
{
    public class LocalRepository : ILocalDataAccess, IPreloadObject, IDisposable
    {
        private readonly IGateWay _gateway;
        private readonly IPreloadObject _preloadObject;
        private readonly IEventDispatcher _eventDispatcher;
        public LocalRepository([Inject(Id = 1)] IGateWay gateway,
            [Inject(Id = 1)] IPreloadObject preloadObject, IEventDispatcher eventDispatcher)
        {
            _gateway = gateway;
            _preloadObject = preloadObject;
            _eventDispatcher = eventDispatcher;
            _eventDispatcher.Subscribe<AppInitialized>(OnAppInitialized);

        }

        private void OnAppInitialized(ISignal signal)
        {
            Debug.Log("LocalRepository received AppInitialized signal.");
            // Handle the signal as needed
            if (signal is AppInitialized appInitialized)
            {
                Debug.Log($"AppInitialized signal message: {appInitialized.Status}");
            }
        }

        public UniTask Preload()
        {
            _preloadObject.Preload();
            return UniTask.CompletedTask;
        }
        public bool IsNewUser()
        {
            return _gateway.Contains<User>();
        }

        public void SetDefaultConfig()
        {
            var user = new User(false);
            _gateway.Set(user);
            //otros sets defaults.
            _gateway.Save();
        }

        public void SetHighScore(string name, int score)
        {

        }

        public LeaderBoardData GetHighScores()
        {
            return new LeaderBoardData();
        }

        public void Flush()
        {
            _gateway.Save();
        }

        public void Dispose()
        {
            _eventDispatcher?.Unsubscribe<AppInitialized>(OnAppInitialized); //<----
        }
    }
}
