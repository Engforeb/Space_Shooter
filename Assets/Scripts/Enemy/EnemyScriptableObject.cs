using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New Ship", menuName = "Enemy Ship")]
public class EnemyScriptableObject : ScriptableObject
{
    public string shipName;
    public float speed;
    public int wounded;
    public int damaged;
}
