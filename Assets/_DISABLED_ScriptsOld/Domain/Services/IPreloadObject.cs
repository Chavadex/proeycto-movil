using Cysharp.Threading.Tasks;

namespace chava.domain
{
    public interface IPreloadObject
    {
        UniTask Preload();
    }
}
