using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _spawnPos;
    

    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _countdown = 2f;
    private int _waveIndex = 0;
    private int _maxWave = 5;
    private int _enemyIndex;

    [SerializeField] private GamePlayUI _gamePlayUI;

    void Start()
    {
        _gamePlayUI.UpdateWaveText(_waveIndex + 1);
        _enemyIndex = 0;
    }

    void Update()
    {
        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = _timeBetweenWaves;
        }
        if (_waveIndex < _maxWave)
        {
            _countdown -= Time.deltaTime;
            _gamePlayUI.UpdateTimeText(_countdown);
        } 
    }

    IEnumerator SpawnWave()
    {
        _waveIndex++;
        if (_waveIndex > 1)
        {
            _gamePlayUI.UpdateWaveText(_waveIndex);
        }
        Enemy enemy = Instantiate(_enemyPrefab, _spawnPos.transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.SetEnemyIndex(_enemyIndex);
        _enemyIndex++;
        yield return null;
    }

}
