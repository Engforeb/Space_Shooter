using System;
using System.Collections;
using Background;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    public class Spawner : MonoBehaviour
    {
        public int ID => id;
        public static Action OnAllInPlace { get; set; }
        public static Action OnAllShipsKilled;

        [SerializeField] private int id;
        [SerializeField] GameObject shipPrefab;
        [SerializeField] private float seconds;
        [SerializeField] private Transform[] positionGrid;
        [SerializeField] private Transform gridStartPosition;
        [SerializeField] private float timeToGetToPosition;
        [SerializeField] private GameObject positions;
        [SerializeField] private GameObject maneuvering;
        
        private BackgroundCompositor _backgroundCompositor;

        private int _killedShips;
        private WaitForSeconds _intervalBetweenShips;

        private Vector3 _initialScale;
        private Vector3 _initialPosition;

        private void OnEnable()
        {
            _backgroundCompositor = FindObjectOfType<BackgroundCompositor>();
            
            var localScale = positions.transform.localScale;
            _initialScale = localScale;
            localScale *= _backgroundCompositor.ResizeFactor;
            positions.transform.localScale = localScale;

            var transform1 = transform;
            var position = transform1.position;
            _initialPosition = position;
            position = new Vector3(0, position.y / _backgroundCompositor.ResizeFactor, 0);
            transform1.position = position;

            StartCoroutine(GetShipsInPlace(shipPrefab));
            
            EnemyShipBehavior.OnDestroy += KilledShipsCounter;
        }

        private void OnDisable()
        {
            EnemyShipBehavior.OnDestroy -= KilledShipsCounter;
            positions.transform.localScale = _initialScale;
            transform.position = _initialPosition;
        }

        private void Start() => 
            _intervalBetweenShips = new WaitForSeconds(seconds);

        private IEnumerator GetShipsInPlace(GameObject shipPfb)
        {
            for (int i = 0; i < positionGrid.Length; i++)
            {
                GameObject ship = Instantiate(shipPfb, gridStartPosition.position, Quaternion.Euler(0, 0, 180));
                ship.transform.SetParent(maneuvering.transform, true);
            
                if (i != positionGrid.Length - 1)
                {
                    ship.transform.DOMove(positionGrid[i].position, timeToGetToPosition);
                    yield return _intervalBetweenShips;
                }
                else if (i == positionGrid.Length - 1)
                {
                    Tween getToPosition = ship.transform.DOMove(positionGrid[i].position, timeToGetToPosition);
                    getToPosition.OnComplete(AllInPosition);
                }
            }
        }

        private void AllInPosition() => 
            OnAllInPlace?.Invoke();

        private void KilledShipsCounter()
        {
            _killedShips++;
            if (_killedShips % positionGrid.Length == 0)
            {
                OnAllShipsKilled?.Invoke();
                gameObject.SetActive(false);
            }   
        }
    }
}
