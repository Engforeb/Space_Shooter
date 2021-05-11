using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _animator;

    private Vector2 _screenBounds;
    private float _playerWidth, _playerHeight;

    private float _lastYPosition;
    private Vector3 _offsetDistance;

    private void Start()
    {
        _lastYPosition = transform.position.y;
        GetScreenAndPlayerBounds();
    }

    private void GetScreenAndPlayerBounds()
    {
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        _playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _playerHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    private void OnMouseDrag()
    {
        MoveWithMouse();
    }

    private void MoveWithMouse()
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
        return Mathf.Clamp(MousePositionInWorld().x - _offsetDistance.x, -_screenBounds.x + _playerWidth, _screenBounds.x - _playerWidth);
    }

    private float LimitByY()
    {
        return Mathf.Clamp(MousePositionInWorld().y - _offsetDistance.y, -_screenBounds.y + _playerHeight, _screenBounds.y - _playerHeight);
    }

    private float LimitByZ()
    {
        return MousePositionInWorld().z;
    }

    private void ForwardChecker()
    {
        float currentPosition = transform.position.y;

        if (currentPosition > _lastYPosition)
        {
            _animator.SetTrigger("Move");
        }

        _lastYPosition = currentPosition;
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
}
