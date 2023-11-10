using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject _gameOverUI;
    void Update()
    {
        if (_playerStats.GetLives() <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        _gameOverUI.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
        {
            string activeScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(activeScene);
        }
    }
}
