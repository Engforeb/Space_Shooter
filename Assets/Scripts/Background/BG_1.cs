using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_1 : MonoBehaviour
{
    [SerializeField] private GameObject _bg2;
    private void Update()
    {
        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0, _bg2.transform.position.y + 10, 0);
        }
    }
}
