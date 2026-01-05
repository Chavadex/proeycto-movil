using chava.app.Repository;
using chava.app.Server;
using chava.app.Services;
using chava.domain;
using chava.interfaceadapter;
using UnityEngine;
using Zenject;
using chava.utilities;


public class AppInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IEventDispatcher>().To<EventDispatcher>().AsSingle().NonLazy();
        Container.Bind<ISerializer>().To<UnitySerializer>().AsSingle().NonLazy();
        Container.Bind(typeof(IGateWay), typeof(IPreloadObject)).WithId(1).To<SaveFileGateway>().AsSingle().NonLazy();
        Container.Bind(InjectionHelpers.GetInterfaces(typeof(LocalRepository))).WithId(2).To<LocalRepository>().AsSingle().NonLazy();
        Container.Bind<IPreLoaderService>().To<PreloadService>().AsSingle().NonLazy();
        Container.Bind<ISceneLoad>().To<UnitySceneLoad>().AsSingle().NonLazy();

        Container.Bind<LogoViewModel>().To<LogoViewModel>().AsSingle().NonLazy();
        // Container.BindInterfacesAndSelfTo<LogoPresenter>().AsSingle().NonLazy();
        Container.Bind(InjectionHelpers.GetInterfaces<LogoPresenter>()).WithId(3).To<LogoPresenter>().AsSingle()
            .NonLazy();

        Container.Bind<AppInitializationUseCase>().AsSingle().NonLazy();
    }
}
