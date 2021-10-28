using System;
using System.Runtime.CompilerServices;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class Mover : MonoBehaviour, IMovable
    {
        [SerializeField] private Controller selectedController = Controller.Mouse;
        [SerializeField] private float keyboardControllerSpeed;
        
        private MouseController _mouseController;
        private KeyboardController _keyboardController;
        
        
        private enum Controller
        {
            Mouse,
            Keyboard
        };
        
        private IInputtable _userInput;
        
        public void Move()
        {
            _userInput.UserInput();
        }
        
        private void Init()
        {   
            
            if (selectedController == Controller.Mouse)
            {
                _mouseController = gameObject.AddComponent<MouseController>();
            }
            else if (selectedController == Controller.Keyboard)
            {
                _keyboardController = gameObject.AddComponent<KeyboardController>();
                _keyboardController.speed = keyboardControllerSpeed;
            }
            
            _userInput = GetComponent<IInputtable>();
        }
        
        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            if (selectedController == Controller.Keyboard)
            {
                Move();
            }
        }

        private void OnMouseDrag()
        {
            if (selectedController == Controller.Mouse)
            {
                Move();
            }
        }
    }
}
