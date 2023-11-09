using UnityEngine;
using TMPro;
public class Shop : MonoBehaviour
{
    BuildManager _buildManager;

    [SerializeField] private TowerBlueprint _turret;
    [SerializeField] private TowerBlueprint _cannon;
    [SerializeField] private TowerBlueprint _spire;
    [SerializeField] private TMP_Text _turretCostText;
    [SerializeField] private TMP_Text _cannonCostText;
    [SerializeField] private TMP_Text _spireCostText;
    
    void Start()
    {
        _buildManager = BuildManager.instance;
        UpdateTowerCostUI();
    }

    public void SelectTurret()
    {
        Debug.Log("Turret selected");
        _buildManager.SelectTowerToBuild(_turret);
    }

    public void SelectCannon()
    {
        Debug.Log("Cannon selected");
        _buildManager.SelectTowerToBuild(_cannon);
    }

    public void SelectSpire()
    {
        Debug.Log("Spire selected");
        _buildManager.SelectTowerToBuild(_spire);
    }
    
    private void UpdateTowerCostUI()
    {
        _turretCostText.text = "$" + _turret.GetCost();
        _cannonCostText.text = "$" + _cannon.GetCost();
        _spireCostText.text = "$" + _spire.GetCost();
    }
}
