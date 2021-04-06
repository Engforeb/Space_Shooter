using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] protected float _backgroundSpeed = 0.5f;
    private SpriteRenderer _spriteRenderer;
    private float _myHeight;
    private GameObject[] _mySiblingsAndI;


    private void Start()
    {
        string myTag = this.gameObject.tag;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myHeight = _spriteRenderer.bounds.size.y;
        _mySiblingsAndI = GameObject.FindGameObjectsWithTag(myTag);
    }
    protected void LateUpdate()
    {
        transform.position += -transform.up * Time.deltaTime * _backgroundSpeed;

        if (transform.position.y <= -_myHeight)
        {
            float moveUpY = MoveUpY(_mySiblingsAndI);
            transform.position = new Vector2(0, moveUpY-0.01f);
        }
    }

    public float MoveUpY(GameObject[] backgrounds)
    {
        float highestY = -100f;

        for (int i = 0; i < BackgroundManager.Instance.backgroundsNumber; i++)
        {
            if (backgrounds[i].transform.position.y > highestY)
            {
                highestY = backgrounds[i].transform.position.y;
            }
        }
        return highestY + BackgroundManager.Instance.BackgroundSize(backgrounds).y;

    }


}
