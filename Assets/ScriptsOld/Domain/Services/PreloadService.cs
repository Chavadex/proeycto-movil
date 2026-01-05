using chava.domain;
using Cysharp.Threading.Tasks;
using Zenject;

namespace chava.domain
{
    public class PreloadService : IPreLoaderService
    {
        private IPreloadObject _localRepositoryPreload;
        //private IPreloadObject _IAPPreload;

        public PreloadService([Inject(Id = 2)] IPreloadObject localRepositoryPreload)
        {
            _localRepositoryPreload = localRepositoryPreload;
        }
        public UniTask Preload()
        {
            _localRepositoryPreload.Preload();

            return UniTask.CompletedTask;
        }
    }
}