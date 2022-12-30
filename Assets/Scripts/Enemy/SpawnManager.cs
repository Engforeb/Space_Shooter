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
        [SerializeField] private Spawner[] spawners;

        private ISavedLoadService _savedLoadService;

        public int Wave
        {
            get;
            set;
        }

        private void Start()
        {
            _savedLoadService = AllServices.Container.Single<ISavedLoadService>();
            StartCoroutine(SpawnerEnumerator());
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.worldData.waveToLoad = Wave;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (SceneManager.GetActiveScene().name == progress.worldData.levelToLoad)
            {
                Wave = progress.worldData.waveToLoad;
            }
        }

        public event Action<int> WaveChanged;

        private IEnumerator SpawnerEnumerator()
        {
            while (true)
            {
                for (int i = Wave; i < spawners.Length; i++)
                {
                    spawners[i].gameObject.SetActive(true);
                    WaveChanged?.Invoke(spawners[i].ID);
                    Wave = i;
                    _savedLoadService.SaveProgress();
                    int i1 = i;
                    yield return new WaitUntil(() => spawners[i1].gameObject.activeSelf == false);
                }
            }
        }
    }
}
