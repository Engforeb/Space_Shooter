using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public class BackgroundCompositor : MonoBehaviour
    {
        public float ResizeFactor { get; private set; }

        [SerializeField] private int backgroundsInLayer;
        [SerializeField] private GameObject[] layerPrefabs;
        [SerializeField] private GameObject[] layerParents;
    
        private GameObject[] _sky;
        private GameObject[] _stars;
        private GameObject[] _meteors;
        private GameObject[] _planets;

        private Dictionary<int, GameObject[]> _layerByIndex;
        private Camera _camera;
        private float _backgroundsHeight;
        private float _screenHeight;
        private float _screenWidth;
        private float _offset;

        private void Awake()
        {
            _camera = Camera.main;
            var screenAdjustmentData = ScreenAdjustmentData();
            _backgroundsHeight = screenAdjustmentData.BackgroundsHeight;
            _offset = screenAdjustmentData.Offset;
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

        private (float BackgroundsHeight, float Offset) ScreenAdjustmentData()
        {
            _screenHeight = _camera.orthographicSize * 2;
            _screenWidth = _screenHeight / Screen.height * Screen.width;
        
            SpriteRenderer spriteRenderer = layerPrefabs[0].GetComponent<SpriteRenderer>();
            Sprite sprite = spriteRenderer.sprite;
            
            ResizeFactor = _screenWidth / sprite.bounds.size.x;
            var backgroundsHeight = spriteRenderer.bounds.size.y * ResizeFactor;
            var offset = (_screenHeight - backgroundsHeight) * 0.5f;

            return (backgroundsHeight, offset);
        }

        private void InitiateBackgrounds(GameObject prefabLayers, GameObject layersToPass, GameObject[] backgrounds)
        {
            for (int i = 0; i < backgroundsInLayer; i++)
            {
                backgrounds[i] = Instantiate(prefabLayers, layersToPass.transform);
                backgrounds[i].GetComponent<IMoveUppable>().Init(_backgroundsHeight, _offset);
                backgrounds[i].GetComponent<IResizable>().Resize(ResizeFactor);
            }
        }

        private void GetBackgroundsToStartPosition(GameObject[] backgrounds)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (i == 0)
                    backgrounds[i].transform.position = new Vector2(0, -_offset);
                else
                    backgrounds[i].transform.position = new Vector2(0, backgrounds[i - 1].transform.position.y + _backgroundsHeight);
            }
        }

    }
}
