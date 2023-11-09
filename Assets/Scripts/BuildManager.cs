using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // store a reference to itself
    public static BuildManager instance;
    
    private TowerBlueprint _towerToBuild;
    [SerializeField] private GameObject _buildEffectPrefab;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GamePlayUI _gameplayUI;
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
    }

    public void BuildTowerOn (TowerBase towerBase)
    {
        int cost = _towerToBuild.GetCost();
        if (_playerStats.HasEnoughMoney(cost))
        {
            _playerStats.SpendMoney(cost);
            _gameplayUI.UpdateMoneyText(_playerStats.GetMoney());
            Debug.Log("Tower built, money left: " +  _playerStats.GetMoney());
        }
        else
        {
            Debug.Log("Don't have enough money");
            return;
        }
        GameObject tower = Instantiate(_towerToBuild.GetPrefab(), towerBase.GetBuildPos(_towerToBuild), Quaternion.identity);
        GameObject buildEffect = Instantiate(_buildEffectPrefab, towerBase.GetBuildPos(_towerToBuild), Quaternion.identity);
        towerBase.SetExistingTower(tower);
        Destroy(buildEffect, 2f);
    }
}
