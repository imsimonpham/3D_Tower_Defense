using UnityEngine;
using System.Collections;
using System;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _spawnPos;

    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _countdown = 2f;
    private int _waveIndex = 0;
    private int _maxWave = 5;

    [SerializeField] private GamePlayUI _gamePlayUI;

    void Start()
    {
        _gamePlayUI.UpdateWaveText(_waveIndex + 1);
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
        Instantiate(_enemyPrefab, _spawnPos.transform.position, Quaternion.identity);
        yield return null;
    }

}
