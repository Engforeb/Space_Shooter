using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    private int _killCounter;
    [SerializeField] TextMeshProUGUI _killCounterField;


    private void OnEnable()
    {
        _killCounterField = GetComponent<TextMeshProUGUI>();
        _killCounterField.text = "0";
        EnemyShipBehavior.OnDestroy += Counter;
    }

    private void Counter()
    {
        _killCounter++;
        _killCounterField.text = _killCounter.ToString();
    }

    private void OnDisable()
    {
        EnemyShipBehavior.OnDestroy -= Counter;
    }
}
