using Interfaces;
using UnityEngine;

public class KeyboardController : MonoBehaviour, IInputtable
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public float speed;

    private IShootable[] _shooters;
    
    private IGetSizeable _gameObjectSize;
    private IGetSizeable _screenBounds;
    private IAnimatable _animator;
    private float _lastYPosition;
    private static readonly int AnimatorMove = Animator.StringToHash("Move");

    private void Awake()
    {
        _shooters = GetComponents<IShootable>();
        _screenBounds = gameObject.AddComponent<CurrentScreen>();
        _gameObjectSize = GetComponent<IGetSizeable>();
        _animator = GetComponent<IAnimatable>();
    }

    public void UserInput()
    {
        Horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        Vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(Horizontal, Vertical, 0);
        transform.position = new Vector2(LimitByX(), LimitByY());
        ShootOnCtrl();
        ForwardChecker();
    }
    
    private float LimitByX()
    {
        return Mathf.Clamp(transform.position.x,
            -_screenBounds.Width + _gameObjectSize.Width / 2, _screenBounds.Width - _gameObjectSize.Width / 2);
    }

    private float LimitByY()
    {
        return Mathf.Clamp(transform.position.y,
            -_screenBounds.Height + _gameObjectSize.Height / 2, _screenBounds.Height - _gameObjectSize.Height / 2);
    }
    
    

    private void ShootOnCtrl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            foreach (var shooter in _shooters)
            {
                shooter.Shoot();
            }
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
