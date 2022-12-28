using Infrastructure.Services;
using InputClasses;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Mover : MonoBehaviour, IMovable
    {
        [SerializeField] private float keyboardInputSpeed;

        private Vector3 _offsetDistance;
        private IGetSizeable _screenBounds;
        private IGetSizeable _gameObjectSize;
        private float _lastYPosition;
        
        private IAnimatable _animator;
        private static readonly int AnimatorMove = Animator.StringToHash("Move");
        
        private Camera _camera;

        private float _leftBorder;
        private float _rightBorder;
        private float _topBorder;
        private float _bottomBorder;
        
        private IInput _iInput;

        public void Init()
        {
            _camera = Camera.main;

            _iInput = AllServices.Container.Single<IInput>();
            
            _gameObjectSize = GetComponent<IGetSizeable>();
            _animator = GetComponent<IAnimatable>();

            var currentScreen = AllServices.Container.Single<CurrentScreen>();
            var borders = currentScreen.BoundsForObject(_gameObjectSize);
            
            _leftBorder = borders.Left;
            _rightBorder = borders.Right;
            _topBorder = borders.Top;
            _bottomBorder = borders.Bottom;
        }

        public void Move()
        {
            Vector3 positionWithinScreen = LimitByScreen();
            transform.position = positionWithinScreen;
            ForwardChecker();
        }

        private void Update()
        {
            _iInput.UserInput();
        }
        
        private void OnMouseDrag()
        {
            Move();
        }

        private Vector2 LimitByScreen()
        {
            float limitByX = Mathf.Clamp(MousePositionInWorld().x - _offsetDistance.x, _leftBorder, _rightBorder);
            float limitByY = Mathf.Clamp(MousePositionInWorld().y - _offsetDistance.y, _bottomBorder, _topBorder);
            
            return new Vector3(limitByX, limitByY);
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
        
        private Vector3 MousePositionInWorld()
        {
            return _camera.ScreenToWorldPoint(new Vector3(
                _iInput.Horizontal,
                _iInput.Vertical,
                _camera.WorldToScreenPoint(transform.position).z)
            );
        }
        
        public void OnMouseDown()
        {
            _offsetDistance = MousePositionInWorld() - transform.position;
        }
    }
}
