using UnityEngine;

public class TowerBaseUI : MonoBehaviour
{
    private TowerBase _towerBase;
    [SerializeField] private GameObject _towerBaseUICanvas;

    public void SetTarget(TowerBase towerBase)
    {
        _towerBase = towerBase;
        transform.position = _towerBase.GetCurrentBuildPos(_towerBase.GetExistingTower().GetComponent<Tower>());
        _towerBaseUICanvas.SetActive(true);
        _towerBase.ShowRangeCircle(_towerBase.GetExistingTower());
    }

    public void Hide()
    {
        _towerBaseUICanvas.SetActive(false);
        _towerBase.HideRangeCircle(_towerBase.GetExistingTower());
    }
}
