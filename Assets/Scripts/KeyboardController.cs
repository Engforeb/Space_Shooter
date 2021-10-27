using System;
using Interfaces;
using UnityEngine;

public class KeyboardController : MonoBehaviour, IInputtable
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public float speed;

    private IShootable[] _shooters;

    private void Awake()
    {
        _shooters = GetComponents<IShootable>();
    }

    public void UserInput()
    {   
        Horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        Vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(Horizontal, Vertical, 0);
    }

    private void ShootOnCtrl()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("Ctrl pressed");
            foreach (var shooter in _shooters)
            {
                shooter.Shoot();
            }
        }
    }
}
