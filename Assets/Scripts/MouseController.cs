using Interfaces;
using UnityEngine;

public class MouseController : MonoBehaviour, IInputtable
{
    private Camera _camera;
    private Vector3 _offsetDistance;
    private IGetSizeable _gameObjectSize;
    private IGetSizeable _screenBounds;
    private IAnimatable _animator;
    private float _lastYPosition;
    private static readonly int AnimatorMove = Animator.StringToHash("Move");
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _camera = Camera.main;
        _screenBounds = gameObject.AddComponent<CurrentScreen>();
        _gameObjectSize = GetComponent<IGetSizeable>();
        _animator = GetComponent<IAnimatable>();
    }

    private void OnMouseDrag()
    {
        UserInput();
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
            Input.mousePosition.x,
            Input.mousePosition.y,
            _camera.WorldToScreenPoint(transform.position).z)
        );
    }
    
    void OnMouseDown()
    {
        _offsetDistance = MousePositionInWorld() - transform.position;
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
