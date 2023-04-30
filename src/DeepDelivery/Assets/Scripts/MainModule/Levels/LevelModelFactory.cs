using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace MainModule
{
    public sealed class LevelModelFactory
    {
        private readonly EnemySpawnPointFactory _enemySpawnPointFactory;
        private readonly EnemySpawnUpdater _enemySpawnUpdater;
        private readonly ItemFactory _itemFactory;

        public LevelModelFactory(EnemySpawnPointFactory enemySpawnPointFactory, EnemySpawnUpdater enemySpawnUpdater,
            ItemFactory itemFactory)
        {
            _enemySpawnPointFactory = enemySpawnPointFactory;
            _enemySpawnUpdater = enemySpawnUpdater;
            _itemFactory = itemFactory;
        }

        public LevelModel Create(LevelData levelData)
        {
            LevelBehaviour levelBehaviour = Object.Instantiate(levelData.LevelBehaviour);

            EnemySpawnUpdater enemySpawnUpdater = ResetEnemySpawnUpdater(levelBehaviour.EnemySpawnPoints);

            List<Item> items = CreateItems(levelBehaviour.ItemSpawnPoints);

            var levelModel = new LevelModel(levelBehaviour, enemySpawnUpdater, items);
            return levelModel;
        }

        private List<Item> CreateItems(List<ItemSpawnPointBehaviour> itemSpawnPoints)
        {
            List<Item> items = new();
            
            foreach (ItemSpawnPointBehaviour itemSpawnPointBehaviour in itemSpawnPoints)
            {
                Item item = _itemFactory.Create(itemSpawnPointBehaviour);
                items.Add(item);
            }
            
            return items;
        }

        private List<EnemySpawnPoint> CreateEnemySpawnPoints(List<EnemySpawnPointBehaviour> enemySpawnPoints)
        {
            var enemies = new List<EnemySpawnPoint>();

            foreach (EnemySpawnPointBehaviour enemySpawnPoint in enemySpawnPoints)
            {
                EnemySpawnPoint enemy = _enemySpawnPointFactory.Create(enemySpawnPoint);
                enemies.Add(enemy);
            }

            return enemies;
        }

        private EnemySpawnUpdater ResetEnemySpawnUpdater(List<EnemySpawnPointBehaviour> enemySpawnPointBehaviours)
        {
            List<EnemySpawnPoint> enemySpawnPoints = CreateEnemySpawnPoints(enemySpawnPointBehaviours);
            _enemySpawnUpdater.ClearSpawnPoints();
            _enemySpawnUpdater.AddSpawnPoints(enemySpawnPoints);
            _enemySpawnUpdater.SpawnEnemies();
            return _enemySpawnUpdater;
        }
    }
}