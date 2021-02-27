  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _healthUnit;
    [SerializeField] private float _distanceBetweenUnits;

    private Player _player;
    public static bool ShouldUpdateHealth;


    private void OnEnable()
    {
        Player.OnDamage += DrawHealthUnits;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        DrawHealthUnits();
    }

    private void OnDisable()
    {
        Player.OnDamage -= DrawHealthUnits;
    }

    private void DrawHealthUnits()
    {
        GameObject[] healthUnits = GameObject.FindGameObjectsWithTag("HealthUnit");
        for (int i = 0; i < healthUnits.Length; i++)
        {
            Destroy(healthUnits[i].gameObject);
        }

        float initialXPosition = 0;

        for (int i = 0; i < _player.Health; i++)
        {
            GameObject healthUnit = Instantiate(_healthUnit, transform, false);
            healthUnit.transform.localPosition = new Vector2(initialXPosition, 0);
            initialXPosition += _distanceBetweenUnits;
        }
    }
}
