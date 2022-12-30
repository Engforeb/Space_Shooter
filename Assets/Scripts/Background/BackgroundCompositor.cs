using System.Collections.Generic;
using Infrastructure.Services;
using UnityEngine;

namespace Background
{
    public class BackgroundCompositor : MonoBehaviour
    {
        [SerializeField] private int backgroundsInLayer;
        [SerializeField] private GameObject[] layerPrefabs;
        [SerializeField] private GameObject[] layerParents;
    
        private GameObject[] _sky;
        private GameObject[] _stars;
        private GameObject[] _meteors;
        private GameObject[] _planets;

        private Dictionary<int, GameObject[]> _layerByIndex;
        private float _backgroundsHeight;
        private float _screenHeight;
        private float _screenWidth;
        private float _offset;

        private IScreenAdjustable _screenAdjustable;

        private void Awake()
        {
            _screenAdjustable = AllServices.Container.Single<IScreenAdjustable>();
            _backgroundsHeight = _screenAdjustable.BackgroundsHeight;
            _offset = _screenAdjustable.VerticalOffset;
        }

        private void Start()
        {
            InitializeBackgrounds();
        }

        private void InitializeBackgrounds()
        {
            CreateBackgroundObjects();
            InstantiateAndArrangeBackgrounds();
        }
        private void InstantiateAndArrangeBackgrounds()
        {
            for (int i = 0; i < layerPrefabs.Length; i++)
            {
                InitiateBackgrounds(layerPrefabs[i], layerParents[i], _layerByIndex[i]);
                GetBackgroundsToStartPosition(_layerByIndex[i]);
            }
        }
        private void CreateBackgroundObjects()
        {
            _sky = new GameObject[backgroundsInLayer];
            _stars = new GameObject[backgroundsInLayer];
            _meteors = new GameObject[backgroundsInLayer];
            _planets = new GameObject[backgroundsInLayer];

            _layerByIndex = new Dictionary<int, GameObject[]>()
            {
                { 0, _sky },
                { 1, _stars },
                { 2, _meteors },
                { 3, _planets }
            };
        }

        private void InitiateBackgrounds(GameObject prefabLayers, GameObject layersToPass, GameObject[] backgrounds)
        {
            for (int i = 0; i < backgroundsInLayer; i++)
            {
                backgrounds[i] = Instantiate(prefabLayers, layersToPass.transform);
                backgrounds[i].GetComponent<IMoveUppable>().Init();
                backgrounds[i].GetComponent<IResizable>().Resize();
            }
        }

        private void GetBackgroundsToStartPosition(GameObject[] backgrounds)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (i == 0)
                {
                    backgrounds[i].transform.position = new Vector2(0, -_offset);
                }
                else
                {
                    backgrounds[i].transform.position = new Vector2(0, backgrounds[i - 1].transform.position.y + _backgroundsHeight);
                }
            }
        }
    }
}
