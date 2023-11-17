using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerBaseUI : MonoBehaviour
{
    private TowerBase _towerBase;
    [SerializeField] private GameObject _towerBaseUICanvas;
    [SerializeField] private TMP_Text _upgradeCostText;
    [SerializeField] private Button _upgradeBtn;
    [SerializeField] private TMP_Text _upgradeText;

    public void SetTarget(TowerBase towerBase)
    {
        _towerBase = towerBase;
        transform.position = _towerBase.GetCurrentBuildPos(_towerBase.GetExistingTower().GetComponent<Tower>());
        _upgradeCostText.text = "$" + towerBase.GetTowerBlueprint().GetUpgradeCost();
        _towerBase.ShowRangeCircle(_towerBase.GetExistingTower());
    }

    public void Hide()
    {
        _towerBaseUICanvas.SetActive(false);
        //_towerBase.HideRangeCircle(_towerBase.GetExistingTower());
    }

    public void Upgrade()
    {
        _towerBase.UpgradeTower();
        _upgradeText.text = "UPGRADED";
        _upgradeBtn.interactable = false;
        _towerBaseUICanvas.SetActive(true);
        BuildManager.instance.DeselectTowerBase();
    }
}
