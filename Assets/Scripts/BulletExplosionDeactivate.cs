using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosionDeactivate : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisactivateExplosion());
    }
    private IEnumerator DisactivateExplosion()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
