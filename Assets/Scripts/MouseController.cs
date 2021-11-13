using System;
using Interfaces;
using UnityEngine;

public class MouseController : MonoBehaviour, IInputtable
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    
    private Camera _camera;
    private Vector3 _offsetDistance;
    private IGetSizeable _gameObjectSize;
    private IGetSizeable _screenBounds;
    private IAnimatable _animator;
    private IShootable[] _shooters;
    private float _lastYPosition;
    private static readonly int AnimatorMove = Animator.StringToHash("Move");
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _camera = Camera.main;
        _screenBounds = gameObject.AddComponent<CurrentScreen>();
        _gameObjectSize = GetComponent<IGetSizeable>();
        _animator = GetComponent<IAnimatable>();
        _shooters = GetComponents<IShootable>();
    }

    public void UserInput()
    {
        Vector3 updatedMousePositionInWorldWithinScreenBounds = new Vector3(
            LimitByX(),
            LimitByY(),
            LimitByZ()
        );

        transform.position = updatedMousePositionInWorldWithinScreenBounds;
        
        ForwardChecker();
    }

    private void Update()
    {
        Horizontal = Input.mousePosition.x;
        Vertical = Input.mousePosition.y;
    }

    private float LimitByX()
    {
        return Mathf.Clamp(MousePositionInWorld().x - _offsetDistance.x,
            -_screenBounds.Width + _gameObjectSize.Width / 2, _screenBounds.Width - _gameObjectSize.Width / 2);
    }

    private float LimitByY()
    {
        return Mathf.Clamp(MousePositionInWorld().y - _offsetDistance.y,
            -_screenBounds.Height + _gameObjectSize.Height / 2, _screenBounds.Height - _gameObjectSize.Height / 2);
    }

    private float LimitByZ()
    {
        return MousePositionInWorld().z;
    }
    
    private Vector3 MousePositionInWorld()
    {
        return _camera.ScreenToWorldPoint(new Vector3(
            Horizontal,
            Vertical,
            _camera.WorldToScreenPoint(transform.position).z)
        );
    }
    
    void OnMouseDown()
    {
        _offsetDistance = MousePositionInWorld() - transform.position;
        foreach (var shooter in _shooters)
        {
            shooter.Shoot();
        }
    }
    
    private void ForwardChecker()
    {
        float currentPosition = transform.position.y;

        if (currentPosition > _lastYPosition)
        {
            _animator.Animator.SetTrigger(AnimatorMove);
        }

        _lastYPosition = currentPosition;
    }
}
