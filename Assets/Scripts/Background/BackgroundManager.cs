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

    [SerializeField] private int _backgroundsNumber;
    [SerializeField] private GameObject _backgroundPrefab;
    
    private GameObject[] _backgrounds;

    private SpriteRenderer _spriteRenderer;
    private Sprite _sprite;

    private void Start()
    {
        _backgrounds = new GameObject[_backgroundsNumber];

        for (int i = 0; i < _backgroundsNumber; i++)
        {
            _backgrounds[i] = Instantiate(_backgroundPrefab, this.transform);
        }

        _spriteRenderer = _backgrounds[0].GetComponent<SpriteRenderer>();
        _sprite = _spriteRenderer.sprite;

        GetBackgroundsToStartPosition();
    }

    private Vector2 BackgroundSize()
    {
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        float resizeFactor = worldScreenWidth / _sprite.bounds.size.x;

        for (int i = 0; i < _backgrounds.Length; i++)
        {
            _backgrounds[i].transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
        }

        return new Vector2(_spriteRenderer.bounds.size.x, _spriteRenderer.bounds.size.y);
    }

    private void GetBackgroundsToStartPosition()
    {
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            if (i == 0)
            {
                _backgrounds[i].transform.position = new Vector2(0, 0);
            }
            else
            {
                _backgrounds[i].transform.position = new Vector2(0, _backgrounds[i - 1].transform.position.y + BackgroundSize().y);
            }
        }
    }

    public float MoveUpY()
    {
        float highestY = -100f;

        for (int i = 0; i < _backgroundsNumber; i++)
        {
            if (_backgrounds[i].transform.position.y > highestY)
            {
                highestY = _backgrounds[i].transform.position.y;
            }
        }
        return highestY + BackgroundSize().y;
    }
}
