using System;
using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _startPos;
    [SerializeField] private GameObject _enemyContainer;
    
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
        
        for (int i = 0; i < _waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(_startPos.transform.position.x, _startPos.transform.position.y - 1f, _startPos.transform.position.z);
        Enemy enemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity).GetComponent<Enemy>();
        enemy.transform.parent = _enemyContainer.transform;
        enemy.SetEnemyIndex(_enemyIndex);
        _enemyIndex++;
    }

}
