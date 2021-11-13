using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;

public class Maneuverer : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation _tweens;
    [SerializeField] private bool _move, _scale, _rotate;

    List<Tween> _allTweens;

    private void Start()
    {
        _allTweens = _tweens.GetTweens();
    }
    private void OnEnable()
    {
        Spawner.OnAllInPlace += StartTweens;
        Spawner.OnAllShipsKilled += RewindAndPause;
    }

    private void OnDisable()
    {
        Spawner.OnAllInPlace -= StartTweens;
        Spawner.OnAllShipsKilled -= RewindAndPause;
    }

    private void StartTweens()
    {
        if (_move)
        {
            _tweens.DOPlayAllById("Move");
        }

        if (_scale)
        {
            _tweens.DOPlayAllById("Scale");
        }

        if (_rotate)
        {
            _tweens.DOPlayAllById("Rotate");
        }
    }

    private void RewindAndPause()
    {
        DOTween.RewindAll();
        DOTween.PauseAll();
    }
}
