using HUD;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreatePlayer() =>
            _assets.Instantiate(AssetPaths.PlayerPath);

        public GameObject CreatePlayer(GameObject at) =>
            _assets.Instantiate(AssetPaths.PlayerPath, at.transform.position);

        public SpawnManager CreateSpawnManager() => 
            _assets.Instantiate(AssetPaths.SpawnManagerPath).GetComponent<SpawnManager>();

        public void CreateHud(SpawnManager spawnManager, string sceneName)
        {
            var hudGO = _assets.Instantiate(AssetPaths.HudPath);
            var hud = hudGO.GetComponent<HudData>();
            hud.Init(spawnManager, sceneName);
        }
    }

}
