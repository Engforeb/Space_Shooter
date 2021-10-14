using UnityEngine;

namespace Background
{
    public class BackgroundCompositor : MonoBehaviour
    {
        public static BackgroundCompositor Instance { get; private set; }
        public int BackgroundsNumber => backgroundsNumber;
        public float ResizeFactor { get; private set; }

        [SerializeField] private int backgroundsNumber;

        // Individual sprites of backgrounds
        [SerializeField] private GameObject[] backgroundPrefabLayers;
    
        // Parent game objects
        [SerializeField] private GameObject[] backgroundLayers; 
    
        private GameObject[] _sky;
        private GameObject[] _stars;
        private GameObject[] _meteors;
        private GameObject[] _planets;

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            _sky = new GameObject[backgroundsNumber];
            _stars = new GameObject[backgroundsNumber];
            _meteors = new GameObject[backgroundsNumber];
            _planets = new GameObject[backgroundsNumber];

            InitiateBackgrounds(backgroundPrefabLayers[0], backgroundLayers[0], _sky);
            GetBackgroundsToStartPosition(_sky);

            InitiateBackgrounds(backgroundPrefabLayers[1], backgroundLayers[1], _stars);
            GetBackgroundsToStartPosition(_stars);

            InitiateBackgrounds(backgroundPrefabLayers[2], backgroundLayers[2], _meteors);
            GetBackgroundsToStartPosition(_meteors);

            InitiateBackgrounds(backgroundPrefabLayers[3], backgroundLayers[3], _planets);
            GetBackgroundsToStartPosition(_planets);
        }

        public Vector2 BackgroundSize(GameObject[] backgrounds)
        {
            if (!(Camera.main is null))
            {
                float worldScreenHeight = Camera.main.orthographicSize * 2;
                float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

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

            return default;
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
