using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public event Action<int> WaveChanged;
    
    [SerializeField] Spawner[] _spawners;

    private void Start()
    {
        StartCoroutine(SpawnerEnumerator());
    }

    private IEnumerator SpawnerEnumerator()
    {
        while(true)
        {
            for (int i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].gameObject.SetActive(true);
                WaveChanged?.Invoke(_spawners[i].ID);
                yield return new WaitUntil(() => _spawners[i].gameObject.activeSelf == false);
            }
        }
    }
}
