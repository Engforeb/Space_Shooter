﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _shipPrefab;
    [SerializeField] private float _seconds;

    [SerializeField] private Transform[] _positionGrid;
    [SerializeField] private Transform _gridStartPosition;

    [SerializeField] private float _timeToGetToPosition;
    [SerializeField] private WaitForSeconds _intervalBetweenShips;

    [SerializeField] private GameObject _positions;
    [SerializeField] private GameObject _maneuvering;

    [SerializeField] [Range(0, 10)] private float _rotationSpeed;

    private int _killedShips;

    public delegate void AllKilledShips();
    public static event AllKilledShips OnAllShipsKilled;

    private Vector3 _initialScale;
    private Vector3 _initialPosition;

    public delegate void AllInPlace();
    public static event AllInPlace OnAllInPlace;

    private void OnEnable()
    {
        _initialScale = _positions.transform.localScale;
        _positions.transform.localScale *= BackgroundManager.Instance.ResizeFactor;

        _initialPosition = this.transform.position;
        this.transform.position = new Vector3(0, this.transform.position.y / BackgroundManager.Instance.ResizeFactor, 0);

        StartCoroutine(GetShipsInPlace(_shipPrefab));
        EnemyShipBehavior.OnDestroy += KilledShipsCounter;
    }

    private void OnDisable()
    {
        EnemyShipBehavior.OnDestroy -= KilledShipsCounter;
        _positions.transform.localScale = _initialScale;
        this.transform.position = _initialPosition;
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
            ship.transform.SetParent(_maneuvering.transform, true);
            
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
        if (OnAllInPlace != null)
        {
            OnAllInPlace();
        }
        
        //EnemyShipBehavior[] ships = GameObject.FindObjectsOfType<EnemyShipBehavior>();

        //foreach (var ship in ships)
        //{
        //    ship.isInStartPosition = true;
        //}
    }

    private void KilledShipsCounter()
    {
        _killedShips++;
        if (_killedShips % _positionGrid.Length == 0)
        {
            if (OnAllShipsKilled != null)
            {
                OnAllShipsKilled();
            }
            
            this.gameObject.SetActive(false);
        }   
    }

}