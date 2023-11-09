using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    // Update is called once per frame
    void Update()
    {
        if (_playerStats.GetLives() <= 0)
        {
            EndGame();
            return;
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over!");
    }
}
