using System.Collections.Generic;

namespace MainModule
{
    public class PlayerGunHandler : IProgressWriter
    {
        public List<GunType> ActiveGuns { get; } = new();

        public PlayerGunHandler(ProgressUpdater progressUpdater)
        {
            progressUpdater.Register(this);
        }
        
        public bool HasGun(GunType gunType)
        {
            return ActiveGuns.Contains(gunType);
        }

        public void AddGun(GunType gunType)
        {
            ActiveGuns.Add(gunType);
        }
        
        public void LoadProgress(Progress progress)
        {
            ActiveGuns.Clear();
            ActiveGuns.AddRange(progress.Player.ActiveGuns);
        }

        public void UpdateProgress(Progress progress)
        {
            progress.Player.ActiveGuns = new List<GunType>(ActiveGuns);
        }
    }
}