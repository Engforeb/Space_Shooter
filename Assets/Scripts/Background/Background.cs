using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float _backgroundSpeed;
    void Update()
    {
        transform.position += -transform.up * Time.deltaTime * _backgroundSpeed;
    }
}
