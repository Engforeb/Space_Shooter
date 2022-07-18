using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Enemy;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour, ISavedProgress
{
    public event Action<int> WaveChanged;
    
    public int Wave { get; set; }
    
    [SerializeField] private Spawner[] spawners;

    private void Start()
    {
        StartCoroutine(SpawnerEnumerator());
    }

    private IEnumerator SpawnerEnumerator()
    {
        while(true)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].gameObject.SetActive(true);
                WaveChanged?.Invoke(spawners[i].ID);
                yield return new WaitUntil(() => spawners[i].gameObject.activeSelf == false);
            }
        }
    }
    public void UpdateProgress(PlayerProgress progress) => 
        progress.worldData.waveToLoad = Wave;
    
    public void LoadProgress(PlayerProgress progress)
    {
        if (SceneManager.GetActiveScene().name == progress.worldData.levelToLoad) 
            Wave = progress.worldData.waveToLoad;
    }
}
