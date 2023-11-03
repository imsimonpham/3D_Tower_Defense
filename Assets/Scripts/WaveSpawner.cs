using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _spawnPos;

    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _countdown = 2f;


    //private int _numOfEnemiesSpawnEachWave = 1;

    void Update()
    {
        if (_countdown <= 0f)
        {
            SpawnWave();
            _countdown = _timeBetweenWaves;
        }

        _countdown -= Time.deltaTime;
    }

    void SpawnWave()
    {
        //Instantiate(_enemyPrefab, _spawnPos.transform.position, Quaternion.identity);
    }

}
