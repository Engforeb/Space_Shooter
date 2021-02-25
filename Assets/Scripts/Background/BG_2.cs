using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_2 : MonoBehaviour
{
    [SerializeField] private GameObject _bg3;
    private void Update()
    {
        if (transform.position.y <= -10)
        {
            transform.position = new Vector3(0, _bg3.transform.position.y + 10, 0);
        }
    }
}
