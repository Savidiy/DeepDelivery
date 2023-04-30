using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace MainModule
{
    public sealed class LevelModelFactory
    {
        private readonly EnemySpawnPointFactory _enemySpawnPointFactory;
        private readonly EnemySpawnUpdater _enemySpawnUpdater;
        private readonly ItemFactory _itemFactory;
        private readonly ShopFactory _shopFactory;
        private readonly QuestFactory _questFactory;

        public LevelModelFactory(EnemySpawnPointFactory enemySpawnPointFactory, EnemySpawnUpdater enemySpawnUpdater,
            ItemFactory itemFactory, ShopFactory shopFactory, QuestFactory questFactory)
        {
            _enemySpawnPointFactory = enemySpawnPointFactory;
            _enemySpawnUpdater = enemySpawnUpdater;
            _itemFactory = itemFactory;
            _shopFactory = shopFactory;
            _questFactory = questFactory;
        }

        public LevelModel Create(LevelData levelData)
        {
            LevelBehaviour levelBehaviour = Object.Instantiate(levelData.LevelBehaviour);

            EnemySpawnUpdater enemySpawnUpdater = ResetEnemySpawnUpdater(levelBehaviour.EnemySpawnPoints);

            List<Item> items = CreateData(_itemFactory, levelBehaviour.ItemSpawnPoints);
            List<Shop> shops = CreateData(_shopFactory, levelBehaviour.Shops);
            List<QuestGiver> questGivers = CreateData<QuestGiver, QuestGiveBehaviour>(_questFactory, levelBehaviour.GiveQuests);
            List<QuestTaker> questTakers = CreateData<QuestTaker, QuestTakeBehaviour>(_questFactory, levelBehaviour.TakeQuests);

            var levelModel = new LevelModel(levelBehaviour, enemySpawnUpdater, items, shops, questGivers, questTakers);
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

        private List<T> CreateData<T, TK>(IFactory<T, TK> factory, List<TK> shopBehaviours)
        {
            List<T> shops = new();
            
            foreach (TK shopBehaviour in shopBehaviours)
            {
                T shop = factory.Create(shopBehaviour);
                shops.Add(shop);
            }
            
            return shops;
        }
    }
}