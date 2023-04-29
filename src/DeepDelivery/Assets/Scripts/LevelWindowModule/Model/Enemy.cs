using SettingsModule;

namespace LevelWindowModule
{
    public class Enemy
    {
        private readonly EnemyBehaviour _enemyBehaviour;

        public Enemy(EnemyBehaviour enemyBehaviour)
        {
            _enemyBehaviour = enemyBehaviour;
        }
    }
}