using System.Collections.Generic;
using UnityEngine;

namespace Background
{
    public class BackgroundCompositor : MonoBehaviour
    {
        public int BackgroundsNumber => backgroundsNumber;
        public float ResizeFactor { get; private set; }

        [SerializeField] private int backgroundsNumber;

        [SerializeField] private GameObject[] backgroundPrefabLayers;
    
        [SerializeField] private GameObject[] backgroundLayerParents; 
    
        private static GameObject[] _sky;
        private static GameObject[] _stars;
        private static GameObject[] _meteors;
        private static GameObject[] _planets;

        private Dictionary<int, GameObject[]> _numberOfLayer;
        

        private void Start()
        {
            ArrangeBackgrounds();
        }

        private void ArrangeBackgrounds()
        {
            _sky = new GameObject[backgroundsNumber];
            _stars = new GameObject[backgroundsNumber];
            _meteors = new GameObject[backgroundsNumber];
            _planets = new GameObject[backgroundsNumber];

            _numberOfLayer = new Dictionary<int, GameObject[]>()
            {
                {0, _sky},
                {1, _stars},
                {2, _meteors},
                {3, _planets}
            };

            for (int i = 0; i < backgroundPrefabLayers.Length; i++)
            {
                InitiateBackgrounds(backgroundPrefabLayers[i], backgroundLayerParents[i], _numberOfLayer[i]);
                GetBackgroundsToStartPosition(_numberOfLayer[i]);
            }
        }

        public Vector2 BackgroundSize(GameObject[] backgrounds)
        {
            if (Camera.main is null) return default;
            var worldScreenHeight = Camera.main.orthographicSize * 2;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            SpriteRenderer spriteRenderer = backgrounds[0].GetComponent<SpriteRenderer>();
            Sprite sprite = spriteRenderer.sprite;
            ResizeFactor = worldScreenWidth / sprite.bounds.size.x;

            foreach (var background in backgrounds)
            {
                background.transform.localScale = new Vector3(ResizeFactor * 1.05f, ResizeFactor * 1.05f, 1);
            }

            var bounds = spriteRenderer.bounds;
            return new Vector2(bounds.size.x, bounds.size.y);

        }

        private void GetBackgroundsToStartPosition(GameObject[] backgrounds)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                if (i == 0)
                {
                    backgrounds[i].transform.position = new Vector2(0, 0);
                }
                else
                {
                    backgrounds[i].transform.position = new Vector2(0, backgrounds[i - 1].transform.position.y + BackgroundSize(backgrounds).y);
                }
            }
        }

        private void InitiateBackgrounds(GameObject prefabLayers, GameObject layers, GameObject[] backgrounds)
        {
            for (int i = 0; i < backgroundsNumber; i++)
            {
                backgrounds[i] = Instantiate(prefabLayers, layers.transform);
            }
        }
    }
}
