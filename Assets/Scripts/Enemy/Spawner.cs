using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _shipPrefab;
    [SerializeField] private float _seconds;

    [SerializeField] private Transform[] _positionGrid;
    [SerializeField] private Transform _gridStartPosition;

    private WaitForSeconds _intervalBetweenShips;

    private void Start()
    {
        _intervalBetweenShips = new WaitForSeconds(_seconds);
        StartCoroutine(GetShipsInPlace(_shipPrefab));
    }

    IEnumerator GetShipsInPlace(GameObject shipPrefab)
    {
        for (int i = 0; i < _positionGrid.Length; i++)
        {
            GameObject ship = Instantiate(shipPrefab, _gridStartPosition.position, Quaternion.Euler(0, 0, 180));
            
            ship.transform.DOMove(_positionGrid[i].position, 2);
            yield return _intervalBetweenShips; 
        }

        yield return new WaitForSeconds(1);
        
        EnemyShipBehavior[] ships = GameObject.FindObjectsOfType<EnemyShipBehavior>();

        foreach (var ship in ships)
        {
            ship.isInStartPosition = true;
        }
    }

}
