using UnityEngine;
namespace InputClasses
{
    public interface IInput
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public bool IsFire { get; }

        public void UserInput();
    }
}
