using AudioModule;
using Bootstrap;
using LevelWindowModule;
using MainModule;
using MvvmModule;
using Progress;
using Savidiy.Utils;
using SettingsModule;
using SettingsWindowModule;
using StartWindowModule;
using UiModule;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public GameSettings GameSettings;
        public AudioLibrary AudioLibrary;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Bootstrapper>().AsSingle();

            Container.BindInterfacesTo<PrefabFactory>().AsSingle();
            Container.BindInterfacesTo<ViewFactory>().AsSingle();
            Container.BindInterfacesTo<ViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<TickInvoker>().AsSingle();

            Container.Bind<WindowsRootProvider>().AsSingle();
            Container.BindInterfacesTo<StartWindowPresenter>().AsSingle();
            Container.BindInterfacesTo<SettingsWindowPresenter>().AsSingle();
            Container.BindInterfacesTo<LevelWindowPresenter>().AsSingle();

            Container.Bind<MainStateMachine>().AsSingle();
            Container.Bind<StartMainState>().AsSingle();
            Container.Bind<LevelPlayMainState>().AsSingle();

            Container.Bind<PlayerHolder>().AsSingle();
            Container.Bind<PlayerFactory>().AsSingle();
            Container.Bind<PlayerInputMover>().AsSingle();
            Container.Bind<CameraToPlayerMover>().AsSingle();
            
            Container.Bind<EnemyPrefabProvider>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            
            Container.Bind<LevelRestarter>().AsSingle();
            Container.Bind<LevelHolder>().AsSingle();
            Container.Bind<LevelModelFactory>().AsSingle();
            
            Container.Bind<ProgressProvider>().AsSingle();
            Container.Bind<ProgressSaver>().AsSingle();

            Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<MusicVolumeController>().AsSingle();

            Container.Bind<GameSettings>().FromInstance(GameSettings);
            Container.Bind<AudioLibrary>().FromInstance(AudioLibrary);
        }
    }
}