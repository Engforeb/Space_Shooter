using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Spawner[] _spawners;

    private void Start()
    {
        //_spawners = GameObject.FindObjectsOfType<Spawner>();
        //Debug.Log("Spawners: " + _spawners.Length);

        //foreach (var spawner in _spawners)
        //{
        //    spawner.gameObject.SetActive(false);
        //}
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
        

        //if (_spawners[0].gameObject.activeSelf)
        //    _spawners[0].gameObject.SetActive(false);
        //else if (!_spawners[0].gameObject.activeSelf)
        //    _spawners[0].gameObject.SetActive(true);

            //if (_spawners[1].gameObject.activeSelf)
            //    _spawners[1].gameObject.SetActive(false);
            //else if (!_spawners[1].gameObject.activeSelf)
            //    _spawners[1].gameObject.SetActive(true);
    }

    //private void OnEnable()
    //{
    //    Spawner.OnAllShipsKilled += TurnOnNextSpawner;
    //}

    //private void Disable()
    //{
    //    Spawner.OnAllShipsKilled -= TurnOnNextSpawner;
    //}

}
