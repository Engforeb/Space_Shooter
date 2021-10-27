using Interfaces;
using UnityEngine;

namespace Player
{
    public class Mover : MonoBehaviour, IMovable
    {
        private IInputtable _userInput;
        
        public void Move()
        {
            _userInput.UserInput();
        }
        
        private void Init()
        {
            _userInput = GetComponent<IInputtable>();
        }
        
        private void Awake()
        {
            Init();
        }

        private void OnMouseDrag()
        {
            Move();
        }
    }
}
