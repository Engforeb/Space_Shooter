using System.Collections.Generic;
using HUD;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        
        public GameObject CreatePlayer()
        {
            return _assets.Instantiate(AssetPaths.PlayerPath);
        }

        public GameObject CreatePlayer(GameObject at)
        {
            return _assets.Instantiate(AssetPaths.PlayerPath, at.transform.position);
        }

        public SpawnManager CreateSpawnManager()
        {
            return InstantiateRegistered(AssetPaths.SpawnManagerPath);
        }

        public GameObject CreateBullet()
        {
            return _assets.Instantiate(AssetPaths.BulletPath);
        }

        public void CreateHud(SpawnManager spawnManager, string sceneName)
        {
            var hudGO = _assets.Instantiate(AssetPaths.HudPath);
            var hud = hudGO.GetComponent<HudData>();
            hud.Init(spawnManager, sceneName);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter) 
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);    
        }
        private SpawnManager InstantiateRegistered(string prefabPath)
        {
            SpawnManager spawnManager = _assets.Instantiate(prefabPath).GetComponent<SpawnManager>();
            RegisterProgressWatchers(spawnManager);
            return spawnManager;
        }
        private void RegisterProgressWatchers(SpawnManager spawnManager)
        {
            foreach (ISavedProgressReader progressReader in spawnManager.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }
    }

}
