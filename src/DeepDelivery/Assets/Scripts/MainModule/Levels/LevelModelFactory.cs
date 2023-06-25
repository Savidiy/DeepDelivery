using System.Collections.Generic;
using Zenject;
using Object = UnityEngine.Object;

namespace MainModule
{
    public sealed class LevelModelFactory
    {
        private readonly EnemySpawnPointFactory _enemySpawnPointFactory;
        private readonly ItemSpawnPointFactory _itemSpawnPointFactory;
        private readonly ShopFactory _shopFactory;
        private readonly QuestFactory _questFactory;
        private readonly CheckPointFactory _checkPointFactory;
        private readonly IInstantiator _instantiator;

        public LevelModelFactory(EnemySpawnPointFactory enemySpawnPointFactory, ItemSpawnPointFactory itemSpawnPointFactory,
            ShopFactory shopFactory, QuestFactory questFactory, CheckPointFactory checkPointFactory, IInstantiator instantiator)
        {
            _enemySpawnPointFactory = enemySpawnPointFactory;
            _itemSpawnPointFactory = itemSpawnPointFactory;
            _shopFactory = shopFactory;
            _questFactory = questFactory;
            _checkPointFactory = checkPointFactory;
            _instantiator = instantiator;
        }

        public LevelModel Create(LevelData levelData)
        {
            LevelBehaviour levelBehaviour = Object.Instantiate(levelData.LevelBehaviour);

            List<EnemySpawnPoint> enemySpawnPoints = _enemySpawnPointFactory.CreatePoints(levelBehaviour.EnemySpawnPoints);
            List<ItemSpawnPoint> items = _itemSpawnPointFactory.CreatePoints(levelBehaviour.ItemSpawnPoints);
            List<Shop> shops = _shopFactory.CreateShops(levelBehaviour.Shops);
            List<CheckPoint> checkPoints = _checkPointFactory.CreatePoints(levelBehaviour.CheckPoints);
            List<QuestGiver> questGivers = _questFactory.CreateQuestGivers(levelBehaviour.GiveQuests);
            List<QuestTaker> questTakers = _questFactory.CreateQuestTakers(levelBehaviour.TakeQuests);

            var args = new object[] {levelBehaviour, items, shops, questGivers, questTakers, checkPoints, enemySpawnPoints};
            var levelModel = _instantiator.Instantiate<LevelModel>(args);

            return levelModel;
        }
    }
}