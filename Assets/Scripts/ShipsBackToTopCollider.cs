using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsBackToTopCollider : MonoBehaviour
{
    private Vector2 _screenBounds;
    private float _enemyHeight;
    [SerializeField] private GameObject _enemy;

    private void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _enemyHeight = _enemy.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.position = new Vector2(collision.transform.position.x, _screenBounds.y + _enemyHeight);
        }
    }
}
