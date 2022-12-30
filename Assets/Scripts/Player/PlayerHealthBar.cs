using UnityEngine;
namespace Player
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject healthUnit;
        [SerializeField] private float distanceBetweenUnits;

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
                GameObject thisHealthUnit = Instantiate(this.healthUnit, transform, false);
                thisHealthUnit.transform.localPosition = new Vector2(initialXPosition, 0);
                initialXPosition += distanceBetweenUnits;
            }
        }
    }
}
