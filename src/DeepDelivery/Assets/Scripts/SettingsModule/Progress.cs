using System;

namespace SettingsModule
{
    [Serializable]
    public class Progress
    {
        public int LevelIndex = 0;
        public int MaxHp;
        public int CurrentHp;

        public Progress(Progress progress)
        {
            LevelIndex = progress.LevelIndex;
            MaxHp = progress.MaxHp;
            CurrentHp = progress.CurrentHp;
        }
    }
}