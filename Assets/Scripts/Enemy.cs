using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _enemyIndex;
    [SerializeField] private float _health;
    [SerializeField] private float _startHealth;
    [SerializeField] private float _moneyGain = 15f;
    private GamePlayUI _gamePlayUI;
    private PlayerStats _playerStats; 
    void Start()
    {
        _playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if (_playerStats == null)
        {
            Debug.LogError("Player Stats is null!");
        }
        
        _gamePlayUI = GameObject.Find("GamePlayCanvas").GetComponent<GamePlayUI>();
        if (_gamePlayUI == null)
        {
            Debug.LogError("GamePlay UI is null!");
        }
    }
    public void SetEnemyIndex(int index)
    {
        _enemyIndex = index;
        _health = _startHealth;
    }

    public int GetEnemyIndex()
    {
        return _enemyIndex; 
    }

    public void TakeDamage(float amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _playerStats.GainMoney(_moneyGain);
        _gamePlayUI.UpdateMoneyText(_playerStats.GetMoney() + _moneyGain);
        Destroy(this.gameObject);
    }

    public float GetHealth()
    {
        return _health;
    }
}
