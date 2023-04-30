using System;
using System.Collections.Generic;

namespace MainModule
{
    [Serializable]
    public class Progress
    {
        public int LevelIndex = 0;
        public int MaxHp;
        public int CurrentHp;
        public List<GunType> ActiveGuns;

        public Progress(Progress progress)
        {
            LevelIndex = progress.LevelIndex;
            MaxHp = progress.MaxHp;
            CurrentHp = progress.CurrentHp;
            ActiveGuns = new List<GunType>(progress.ActiveGuns);
        }
    }
}