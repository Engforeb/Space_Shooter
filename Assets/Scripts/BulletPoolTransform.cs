using UnityEngine;

public class BulletPoolTransform : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
