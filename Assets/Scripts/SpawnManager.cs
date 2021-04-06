using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
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
                yield return new WaitUntil(() => _spawners[i].gameObject.activeSelf == false);
            }
        }
    }
}
