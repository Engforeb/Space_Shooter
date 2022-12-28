using System;
using InputClasses;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Mover : MonoBehaviour, IMovable
    {
        //[SerializeField] private Controller selectedController = Controller.Mouse;
        [SerializeField] private float keyboardControllerSpeed;
        
        private KeyboardController _keyboardController;
        
        private Vector3 _offsetDistance;
        private IGetSizeable _screenBounds;
        private IGetSizeable _gameObjectSize;
        private float _lastYPosition;
        private IAnimatable _animator;
        private static readonly int AnimatorMove = Animator.StringToHash("Move");
        private Camera _camera;
        private IShootable[] _shooters;
        
        private enum Controller
        {
            Mouse,
            Keyboard
        };
        
        private IInputtable _userInput;
        private IInput _iInput;
        private float speed = 5; 

        private void Awake()
        {
            _camera = Camera.main;
            _iInput = new KeyboardInput();
            
        }

        private void Start()
        {
            _shooters = GetComponents<IShootable>();
            _screenBounds = gameObject.AddComponent<CurrentScreen>();
            _gameObjectSize = GetComponent<IGetSizeable>();
            _animator = GetComponent<IAnimatable>();
        }

        public void MoveMouse()
        {
            //_userInput.UserInput();

            Vector3 positionWithinScreen = LimitByScreenMouse();

            transform.position = positionWithinScreen;
        
            ForwardChecker();
        }

        private void MoveKeyboard()
        {
            transform.Translate(_iInput.Horizontal * Time.deltaTime * speed, _iInput.Vertical * Time.deltaTime * speed, 0);
            transform.position = LimitByScreenKeyboard();
        }
        
        private void Init()
        {
            // if (selectedController == Controller.Mouse)
            // {
            //     gameObject.AddComponent<MouseController>();
            // }
            // else if (selectedController == Controller.Keyboard)
            // {
            //     _keyboardController = gameObject.AddComponent<KeyboardController>();
            //     _keyboardController.speed = keyboardControllerSpeed;
            // }
            
            //_userInput = GetComponent<IInputtable>();
            
            
            
        }
        
        // private void Awake()
        // {
        //     Init();
        // }

        private void Update()
        {
            // if (selectedController == Controller.Keyboard)
            // {
                 MoveKeyboard();
            // }
            _iInput.UserInput();
        }

        private void OnMouseDrag()
        {
            // if (selectedController == Controller.Mouse)
            // {
                 MoveMouse();
            // }
        }

        private Vector3 LimitByScreenMouse()
        {
            float limitByX = Mathf.Clamp(MousePositionInWorld().x - _offsetDistance.x,
                -_screenBounds.Width + _gameObjectSize.Width / 2, _screenBounds.Width - _gameObjectSize.Width / 2);
            
            float limitByY = Mathf.Clamp(MousePositionInWorld().y - _offsetDistance.y,
                -_screenBounds.Height + _gameObjectSize.Height / 2, _screenBounds.Height - _gameObjectSize.Height / 2);
            
            float limitByZ = MousePositionInWorld().z;
            
            return new Vector3(limitByX, limitByY, limitByZ);
        }

        private Vector2 LimitByScreenKeyboard()
        {
            float limitByX = Mathf.Clamp(transform.position.x,
                -_screenBounds.Width + _gameObjectSize.Width / 2, _screenBounds.Width - _gameObjectSize.Width / 2);
            float limitByY = Mathf.Clamp(transform.position.y,
                -_screenBounds.Height + _gameObjectSize.Height / 2, _screenBounds.Height - _gameObjectSize.Height / 2);
            
            return new Vector2(limitByX, limitByY);
        }
        
        // private float LimitByX()
        // {
        //     return Mathf.Clamp(MousePositionInWorld().x - _offsetDistance.x,
        //         -_screenBounds.Width + _gameObjectSize.Width / 2, _screenBounds.Width - _gameObjectSize.Width / 2);
        // }
        //
        // private float LimitByY()
        // {
        //     return Mathf.Clamp(MousePositionInWorld().y - _offsetDistance.y,
        //         -_screenBounds.Height + _gameObjectSize.Height / 2, _screenBounds.Height - _gameObjectSize.Height / 2);
        // }
        //
        // private float LimitByZ()
        // {
        //     return MousePositionInWorld().z;
        // }
        
        private void ForwardChecker()
        {
            float currentPosition = transform.position.y;

            if (currentPosition > _lastYPosition)
            {
                _animator.Animator.SetTrigger(AnimatorMove);
            }

            _lastYPosition = currentPosition;
        }
        
        private Vector3 MousePositionInWorld()
        {
            return _camera.ScreenToWorldPoint(new Vector3(
                _iInput.Horizontal,
                _iInput.Vertical,
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
    }
}
