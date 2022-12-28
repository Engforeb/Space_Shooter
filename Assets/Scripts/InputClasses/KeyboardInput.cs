using UnityEngine;
namespace InputClasses
{
    public class KeyboardInput : IInput
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public bool IsFire { get; private set; }
        
        public void UserInput()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");                
            IsFire = Input.GetButton("Fire1");
        }
    }
}
