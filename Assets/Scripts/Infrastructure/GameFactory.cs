using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }
        public GameObject CreatePlayer() => 
            _assets.Instantiate(AssetPaths.PlayerPath);

        public GameObject CreatePlayer(GameObject at) => 
            _assets.Instantiate(AssetPaths.PlayerPath, at.transform.position);

        public void CreateHud() => 
            _assets.Instantiate(AssetPaths.HudPath);
    }

}
