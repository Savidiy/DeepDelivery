using AudioModule;
using LevelWindowModule;
using MainModule;
using MvvmModule;
using Savidiy.Utils;
using SettingsWindowModule;
using StartWindowModule;
using UiModule;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public GameStaticData GameStaticData;
        public EnemyStaticDataProvider EnemyStaticDataProvider;
        public ItemStaticDataProvider ItemStaticDataProvider;
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
            Container.Bind<PlayerInputShooter>().AsSingle();
            Container.Bind<PlayerDeathChecker>().AsSingle();
            Container.Bind<CameraToPlayerMover>().AsSingle();
            Container.Bind<PlayerInvulnerability>().AsSingle();
            
            Container.Bind<BulletFactory>().AsSingle();
            Container.Bind<BulletHolder>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemyMoveUpdater>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnUpdater>().AsSingle();
            Container.Bind<EnemyHolder>().AsSingle();
            Container.Bind<EnemySpawnPointFactory>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.Bind<CollisionWithEnemyChecker>().AsSingle();
            
            Container.Bind<ShopFactory>().AsSingle();
            Container.Bind<UseShopChecker>().AsSingle();
            Container.Bind<ItemSpawnPointFactory>().AsSingle();
            Container.Bind<ItemBehaviourFactory>().AsSingle();
            Container.Bind<CollisionWithItemsChecker>().AsSingle();
            
            Container.Bind<LevelRestarter>().AsSingle();
            Container.Bind<LevelHolder>().AsSingle();
            Container.Bind<LevelModelFactory>().AsSingle();
            
            Container.Bind<QuestChecker>().AsSingle();
            Container.Bind<QuestFactory>().AsSingle();
            Container.Bind<QuestCompassUpdater>().AsSingle();
            
            Container.Bind<CheckPointFactory>().AsSingle();
            Container.Bind<UseCheckPointChecker>().AsSingle();
            Container.Bind<ProgressProvider>().AsSingle();
            Container.Bind<ProgressUpdater>().AsSingle();
            Container.Bind<ProgressSaveLoad>().AsSingle();

            Container.Bind<CameraProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<MusicVolumeController>().AsSingle();

            Container.Bind<InputSettings>().AsSingle();
            Container.Bind<GameStaticData>().FromInstance(GameStaticData);
            Container.Bind<EnemyStaticDataProvider>().FromInstance(EnemyStaticDataProvider);
            Container.Bind<ItemStaticDataProvider>().FromInstance(ItemStaticDataProvider);
            Container.Bind<AudioLibrary>().FromInstance(AudioLibrary);
        }
    }
}