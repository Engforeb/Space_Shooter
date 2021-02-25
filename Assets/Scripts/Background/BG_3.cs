using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_3 : MonoBehaviour
{
    [SerializeField] private GameObject _bg1;
    private void Update()
    {
        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0, _bg1.transform.position.y + 10, 0);
        }
    }
}
