using Infrastructure.Services;
using UnityEngine;
namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        GameObject CreatePlayer(GameObject at);
        SpawnManager CreateSpawnManager();
        void CreateHud(SpawnManager spawnManager, string sceneName);
    }
}
