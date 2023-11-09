using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _money;
    [SerializeField] private float _startMoney;
    [SerializeField] private int _lives;
    [SerializeField] private int _startLives;
    [SerializeField] private GamePlayUI _gamePlayUI;
    void Start()
    {
        _money = _startMoney;
        _lives = _startLives;
    }

    public float GetMoney()
    {
        return _money;
    }

    public bool HasEnoughMoney(float amount)
    {
        return _money >= amount ? true : false;
    }

    public void SpendMoney(float amount)
    {
        _money -= amount;
    }

    public void GainMoney(float amount)
    {
        _money += amount;
    }

    public int GetLives()
    {
        return _lives;
}

    public void ReduceLives()
    {
        _lives--;
        if (_lives <= 0)
        {
            _lives = 0;
        }
        _gamePlayUI.UpdateLiveText(_lives);
    }
        
}
