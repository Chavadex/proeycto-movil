using Cysharp.Threading.Tasks;

namespace chava.domain
{
    public interface ISceneLoad
    {
        UniTask LoadScene(string sceneName);
        void ActivateScene();
    }
}
