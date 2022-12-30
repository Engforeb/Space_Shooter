using System;
namespace Data
{
    [Serializable]
    public class WorldData
    {
        public string levelToLoad;
        public int waveToLoad;

        public WorldData(string initialLevel)
        {
            levelToLoad = initialLevel;
            waveToLoad = 0;
        }
    }
}
