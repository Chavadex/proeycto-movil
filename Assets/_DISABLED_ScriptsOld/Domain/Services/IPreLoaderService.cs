using Cysharp.Threading.Tasks;

namespace chava.domain
{
    public interface IPreLoaderService
    {
        UniTask Preload();
    }
}
