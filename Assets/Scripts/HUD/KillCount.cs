using Enemy;
using TMPro;
using UnityEngine;
namespace HUD
{
    public class KillCount : MonoBehaviour
    {
        private int _killCounter;
        [SerializeField] TextMeshProUGUI killCounterField;

        private void OnEnable()
        {
            killCounterField = GetComponent<TextMeshProUGUI>();
            killCounterField.text = "0";
            EnemyShipBehavior.OnDestroy += Counter;
        }

        private void Counter()
        {
            _killCounter++;
            killCounterField.text = _killCounter.ToString();
        }

        private void OnDisable()
        {
            EnemyShipBehavior.OnDestroy -= Counter;
        }
    }
}
