using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    protected float _backgroundSpeed = 0.5f;
    private SpriteRenderer _spriteRenderer;
    private float _myHeight;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myHeight = _spriteRenderer.bounds.size.y;
    }
    protected void LateUpdate()
    {
        transform.position += -transform.up * Time.deltaTime * _backgroundSpeed;

        if (transform.position.y <= -_myHeight)
        {
            var moveUpY = BackgroundManager.Instance.MoveUpY();
            transform.position = new Vector2(0, moveUpY-0.01f);
        }
    }
}
