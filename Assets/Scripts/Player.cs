using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audio;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _leftSocket;
    [SerializeField] private Transform _rightSocket;

    [Range(0, 1)] [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _leftMuzzleFlash, _rightMuzzleFlash;

    private Vector2 _screenBounds;
    private float _playerWidth;
    private float _playerHeight;

    private float _lastYPosition;
    private Vector3 _offsetDistance;

    private void Start()
    {
        GetScreenAndPlayerBounds();

        _lastYPosition = transform.position.y;
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

    void OnMouseDown()
    {
        _offsetDistance = MousePositionInWorld() - transform.position;
        StartCoroutine(ContinousShoot());
    }

    private Vector3 MousePositionInWorld()
    {
        return _camera.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            _camera.WorldToScreenPoint(transform.position).z)
            );
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

    private IEnumerator ContinousShoot()
    {
        while (Input.GetMouseButton(0))
        {
            _leftMuzzleFlash.SetActive(true);
            _audio.Play();
            Instantiate(_bullet, _leftSocket.transform.position, Quaternion.identity, _leftSocket.transform);

            _rightMuzzleFlash.SetActive(true);
            _audio.Play();
            Instantiate(_bullet, _rightSocket.transform.position, Quaternion.identity, _rightSocket.transform);

            yield return new WaitForSeconds(0.05f); //to quench fire immediately

            _leftMuzzleFlash.SetActive(false);
            _rightMuzzleFlash.SetActive(false);

            yield return new WaitForSeconds(_fireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _animator.SetTrigger("Explosion");
            SoundManager.Instance.PlayerExplosion();
            Destroy(this.gameObject, 1f);
        }
    }

}
