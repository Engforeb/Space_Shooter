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

    [SerializeField] private float _timeToGetToPosition;
    [SerializeField] private WaitForSeconds _intervalBetweenShips;

    private int _killedShips;

    public delegate void AllKilledShips();
    public static event AllKilledShips OnAllShipsKilled;


    private void OnEnable()
    {
        StartCoroutine(GetShipsInPlace(_shipPrefab));
        EnemyShipBehavior.OnDestroy += KilledShipsCounter;
    }

    private void OnDisable()
    {
        EnemyShipBehavior.OnDestroy -= KilledShipsCounter;
    }

    private void Start()
    {
        _intervalBetweenShips = new WaitForSeconds(_seconds);
    }

    IEnumerator GetShipsInPlace(GameObject shipPrefab)
    {
        for (int i = 0; i < _positionGrid.Length; i++)
        {
            GameObject ship = Instantiate(shipPrefab, _gridStartPosition.position, Quaternion.Euler(0, 0, 180));
            
            if (i != _positionGrid.Length - 1)
            {
                ship.transform.DOMove(_positionGrid[i].position, _timeToGetToPosition);
                yield return _intervalBetweenShips;
            }
            else if (i == _positionGrid.Length - 1)
            {
                Tween getToPosition = ship.transform.DOMove(_positionGrid[i].position, _timeToGetToPosition);
                getToPosition.OnComplete(() => AllInPosition());
            }
        }
    }

    private void AllInPosition()
    {
        EnemyShipBehavior[] ships = GameObject.FindObjectsOfType<EnemyShipBehavior>();

        foreach (var ship in ships)
        {
            ship.isInStartPosition = true;
        }
    }

    private void KilledShipsCounter()
    {
        _killedShips++;
        if (_killedShips % _positionGrid.Length == 0)
        {
            this.gameObject.SetActive(false);
        }   
    }

}
