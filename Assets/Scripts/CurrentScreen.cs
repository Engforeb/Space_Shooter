using System;
using Interfaces;
using UnityEngine;

public class CurrentScreen : MonoBehaviour, IGetSizeable
{
    public float Width { get; private set; }
    public float Height { get; private set; }
    
    private Camera _camera;
    private Vector3 _screenBounds;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        Width = _screenBounds.x;
        Height = _screenBounds.y;
    }
}
