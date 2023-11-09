using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _waveText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _liveText;
    [SerializeField] private TMP_Text _moneyText;
    private int _maxWave = 5;

    public void UpdateWaveText(int waveIndex)
    {
        _waveText.text = "Wave " + waveIndex + "/" + _maxWave;
    }

    public void UpdateTimeText(float time)
    {
        _timeText.text = "Next Wave In " + string.Format("{0:0}", time);
    }

    public void UpdateLiveText(int life)
    {
        _liveText.text = "Lives: " + life;
    }
    
    public void UpdateMoneyText(float money)
    {
        _moneyText.text = "$" + money;
    }
}
