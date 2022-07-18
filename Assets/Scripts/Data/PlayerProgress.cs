using System;
namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData worldData;
        public PlayerProgress(string initialLevel)
        {
            worldData = new WorldData(initialLevel);
        }
    }
}
