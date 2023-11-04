using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] TMP_Text _waveText;
    [SerializeField] TMP_Text _timeText;
    private int _maxWave = 5;

    public void UpdateWaveText(int waveIndex)
    {
        _waveText.text = "Wave " + waveIndex + "/" + _maxWave;
    }

    public void UpdateTimeText(float time)
    {
        _timeText.text = "Incoming Wave In " + string.Format("{0:0.00}", time);
    }
}
