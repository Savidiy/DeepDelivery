using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace MainModule
{
    public sealed class LevelModelFactory
    {
        private readonly EnemySpawnPointFactory _enemySpawnPointFactory;
        private readonly EnemySpawnUpdater _enemySpawnUpdater;
        private readonly ItemSpawnPointFactory _itemSpawnPointFactory;
        private readonly ShopFactory _shopFactory;
        private readonly QuestFactory _questFactory;
        private readonly CheckPointFactory _checkPointFactory;

        public LevelModelFactory(EnemySpawnPointFactory enemySpawnPointFactory, EnemySpawnUpdater enemySpawnUpdater,
            ItemSpawnPointFactory itemSpawnPointFactory, ShopFactory shopFactory, QuestFactory questFactory, CheckPointFactory checkPointFactory)
        {
            _enemySpawnPointFactory = enemySpawnPointFactory;
            _enemySpawnUpdater = enemySpawnUpdater;
            _itemSpawnPointFactory = itemSpawnPointFactory;
            _shopFactory = shopFactory;
            _questFactory = questFactory;
            _checkPointFactory = checkPointFactory;
        }

        public LevelModel Create(LevelData levelData)
        {
            LevelBehaviour levelBehaviour = Object.Instantiate(levelData.LevelBehaviour);

            EnemySpawnUpdater enemySpawnUpdater = ResetEnemySpawnUpdater(levelBehaviour.EnemySpawnPoints);

            List<ItemSpawnPoint> items = CreateData(_itemSpawnPointFactory, levelBehaviour.ItemSpawnPoints);
            List<Shop> shops = CreateData(_shopFactory, levelBehaviour.Shops);
            List<CheckPoint> checkPoints = CreateData(_checkPointFactory, levelBehaviour.CheckPoints);
            List<QuestGiver> questGivers = CreateData<QuestGiver, QuestGiveBehaviour>(_questFactory, levelBehaviour.GiveQuests);
            List<QuestTaker> questTakers = CreateData<QuestTaker, QuestTakeBehaviour>(_questFactory, levelBehaviour.TakeQuests);

            var levelModel = new LevelModel(levelBehaviour, enemySpawnUpdater, items, shops, questGivers, questTakers,
                checkPoints);

            return levelModel;
        }

        private EnemySpawnUpdater ResetEnemySpawnUpdater(List<EnemySpawnPointBehaviour> enemySpawnPointBehaviours)
        {
            List<EnemySpawnPoint> enemySpawnPoints = CreateData(_enemySpawnPointFactory, enemySpawnPointBehaviours);
            _enemySpawnUpdater.ClearSpawnPoints();
            _enemySpawnUpdater.AddSpawnPoints(enemySpawnPoints);
            _enemySpawnUpdater.SpawnEnemies();
            return _enemySpawnUpdater;
        }

        private List<T> CreateData<T, TK>(IFactory<T, TK> factory, List<TK> behaviours)
            where T : class
        {
            List<T> data = new();

            foreach (TK behaviour in behaviours)
            {
                T item = factory.Create(behaviour);
                data.Add(item);
            }

            return data;
        }
    }
}