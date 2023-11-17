using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // store a reference to itself
    public static BuildManager instance;
    
    private TowerBlueprint _towerToBuild;
    private TowerBase _selectedTowerBase;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private TowerBaseUI _towerBaseUI;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build manager in scene!");
            return;
        }
        instance = this;
    }

    public bool CanBuild()
    {
        return _towerToBuild != null ? true : false;
    }

    public bool HasMoney()
    {
        return _playerStats.HasEnoughMoney(_towerToBuild.GetCost()) ? true : false;
    }
    
    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        _towerToBuild = tower;
        _selectedTowerBase = null;
        _towerBaseUI.Hide();
    }

    public void SelectTowerBase(TowerBase towerBase)
    {
        if (_selectedTowerBase == towerBase)
        {
            DeselectTowerBase();
            return;
        }
        _selectedTowerBase = towerBase;
        _towerToBuild = null;
        _towerBaseUI.SetTarget(towerBase);
    }

    public void DeselectTowerBase()
    {
        _selectedTowerBase = null;
        _towerBaseUI.Hide();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return _towerToBuild;
    }
}
