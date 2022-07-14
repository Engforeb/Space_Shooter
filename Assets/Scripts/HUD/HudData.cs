using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace HUD
{
    public class HudData : MonoBehaviour
    {
        [SerializeField] private TMP_Text level;
        [SerializeField] private TMP_Text wave;

        private SpawnManager _spawnManager;

        private void OnEnable()
        {
            _spawnManager = FindObjectOfType<SpawnManager>();
            _spawnManager.WaveChanged += ChangeWaveNumber;
            level.text = SceneManager.GetActiveScene().name;
        }
        private void ChangeWaveNumber(int waveNumber)
        {
            wave.text = "Wave " + waveNumber;
        }

        private void OnDisable()
        {
            _spawnManager.WaveChanged -= ChangeWaveNumber;
        }
    }
}
