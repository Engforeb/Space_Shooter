using System;
using System.Collections;
using Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Enemy
{
    public class SpawnManager : MonoBehaviour, ISavedProgress
    {
        public event Action<int> WaveChanged;
    
        public int Wave { get => _wave; set => _wave = value; }
    
        [SerializeField] private Spawner[] spawners;

        private int _wave;

        private ISavedLoadService _savedLoadService;

        private void Start()
        {
            _savedLoadService = AllServices.Container.Single<ISavedLoadService>();
            StartCoroutine(SpawnerEnumerator());
        }

        private IEnumerator SpawnerEnumerator()
        {
            while(true)
            {
                for (int i = _wave; i < spawners.Length; i++)
                {
                    spawners[i].gameObject.SetActive(true);
                    WaveChanged?.Invoke(spawners[i].ID);
                    _wave = i;
                    _savedLoadService.SaveProgress();
                    int i1 = i;
                    yield return new WaitUntil(() => spawners[i1].gameObject.activeSelf == false);
                }
            }
        }
        public void UpdateProgress(PlayerProgress progress) => 
            progress.worldData.waveToLoad = _wave;
    
        public void LoadProgress(PlayerProgress progress)
        {
            if (SceneManager.GetActiveScene().name == progress.worldData.levelToLoad)
            {
                _wave = progress.worldData.waveToLoad;
            }
        }
    }
}
