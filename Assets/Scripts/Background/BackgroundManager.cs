using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    private static BackgroundManager _instance;
    public static BackgroundManager Instance => _instance;
    private void Awake()
    {
        _instance = this;
    }

    public int backgroundsNumber => _backgroundsNumber;
    public float ResizeFactor => _resizeFactor;

    [SerializeField] private int _backgroundsNumber;

    //individual sprites of backgrounds
    [SerializeField] private GameObject[] _backgroundPrefabLayers = new GameObject[4];
    
    //parent game objects
    [SerializeField] private GameObject[] _backgroundLayers = new GameObject[4]; 
    
    private GameObject[] _sky; //background of three sprites
    private GameObject[] _stars;
    private GameObject[] _meteors;
    private GameObject[] _planets;

    private float _resizeFactor;
    

    private void Start()
    {
        _sky = new GameObject[_backgroundsNumber];
        _stars = new GameObject[_backgroundsNumber];
        _meteors = new GameObject[_backgroundsNumber];
        _planets = new GameObject[_backgroundsNumber];

        InitiateBackgrounds(_backgroundPrefabLayers[0], _backgroundLayers[0], _sky);
        GetBackgroundsToStartPosition(_sky);

        InitiateBackgrounds(_backgroundPrefabLayers[1], _backgroundLayers[1], _stars);
        GetBackgroundsToStartPosition(_stars);

        InitiateBackgrounds(_backgroundPrefabLayers[2], _backgroundLayers[2], _meteors);
        GetBackgroundsToStartPosition(_meteors);

        InitiateBackgrounds(_backgroundPrefabLayers[3], _backgroundLayers[3], _planets);
        GetBackgroundsToStartPosition(_planets);
    }

    public Vector2 BackgroundSize(GameObject[] backgrounds)
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        SpriteRenderer spriteRenderer = backgrounds[0].GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        _resizeFactor = worldScreenWidth / sprite.bounds.size.x;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.localScale = new Vector3(_resizeFactor, _resizeFactor, 1);
        }

        return new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
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

    

    private void InitiateBackgrounds(GameObject backgroundPrefabLayers, GameObject backgroundLayers, GameObject[] backgrounds)
    {
        for (int i = 0; i < _backgroundsNumber; i++)
        {
            backgrounds[i] = Instantiate(backgroundPrefabLayers, backgroundLayers.transform);
        }
    }
}
