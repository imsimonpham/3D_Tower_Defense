using UnityEngine;

public class TowerBase : MonoBehaviour
{
    private Color _hoverColor;
    private Color _errorColor;
    private Color _originalColor;
    private Renderer _renderer;
   
    [Header("Optional")]
    [SerializeField] private GameObject _existingTower;

    private TowerBlueprint _towerBlueprint;
    private bool _isUpgraded = false;
    private BuildManager _buildManager;
    private PlayerStats _playerStats;
    private GamePlayUI _gamePlayUI;
    [SerializeField] private GameObject _buildEffectPrefab;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _hoverColor = Color.green;
        _errorColor = Color.red;
        _buildManager = BuildManager.instance;
        
        _gamePlayUI = GameObject.Find("GamePlayUI").GetComponent<GamePlayUI>();
        if (_gamePlayUI == null)
        {
            Debug.LogError("GamePlay UI is null!");
        }
        
        _playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if (_playerStats == null)
        {
            Debug.LogError("GamePlay UI is null!");
        }
    }

    void OnMouseDown()
    {
        if (_existingTower != null)
        {
            _buildManager.SelectTowerBase(this);
            return;
        }
        
        if (!_buildManager.CanBuild()) {
            Debug.Log("Tower is null");
            return;
        }
        BuildTower(_buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerBlueprint blueprint)
    {
        int cost = blueprint.GetCost();
        if (_playerStats.HasEnoughMoney(cost))
        {
            _playerStats.SpendMoney(cost);
            _gamePlayUI.UpdateMoneyText(_playerStats.GetMoney());
            Debug.Log("Tower built, money left: " +  _playerStats.GetMoney());
        }
        else
        {
            Debug.Log("Don't have enough money");
            return;
        }
        GameObject tower = Instantiate(blueprint.GetPrefab(), GetBuildPos(blueprint), Quaternion.identity);
        GameObject buildEffect = Instantiate(_buildEffectPrefab, GetBuildPos(blueprint), Quaternion.identity);
        SetExistingTower(tower);
        _towerBlueprint = blueprint;
        //StartCoroutine(tower.GetComponent<Tower>().HideRangeCirle());
        Destroy(buildEffect, 2f);
    }

    public void UpgradeTower()
    {
        if (_isUpgraded)
        {
            Debug.Log("Tower has already been upgraded");
            return;
        }
        int cost = _towerBlueprint.GetUpgradeCost();
        if (_playerStats.HasEnoughMoney(cost))
        {
            _playerStats.SpendMoney(cost);
            _gamePlayUI.UpdateMoneyText(_playerStats.GetMoney());
            Debug.Log("Tower built, money left: " +  _playerStats.GetMoney());
        }
        else
        {
            Debug.Log("Don't have enough money to upgrade");
            return;
        }
        //destroy old tower
        Destroy(_existingTower);
        
        //build new tower
        GameObject tower = Instantiate(_towerBlueprint.GetUpgradePrefab(), GetBuildPos(_towerBlueprint), Quaternion.identity);
        GameObject buildEffect = Instantiate(_buildEffectPrefab, GetBuildPos(_towerBlueprint), Quaternion.identity);
        SetExistingTower(tower);
        //StartCoroutine(tower.GetComponent<Tower>().HideRangeCirle());
        Destroy(buildEffect, 2f);
        _isUpgraded = true;
    }

    public Vector3 GetBuildPos (TowerBlueprint tower)
    {
        Vector3 buildPos = new Vector3(base.transform.position.x, base.transform.position.y + tower.GetYOffset(), base.transform.position.z);
        return buildPos; 
    }
    
    public Vector3 GetCurrentBuildPos (Tower tower)
    {
        Vector3 buildPos = new Vector3(base.transform.position.x, base.transform.position.y + tower.GetTowerYOffset(), base.transform.position.z);
        return buildPos; 
    }

    public GameObject GetExistingTower()
    {
        return _existingTower;
    }

    public TowerBlueprint GetTowerBlueprint()
    {
        return _towerBlueprint;
    }

    public void SetExistingTower(GameObject tower)
    {
        _existingTower = tower;
    }

    public void ShowRangeCircle(GameObject tower)
    {
        GameObject circle = tower.GetComponent<Tower>().GetTowerRangeCircle();
        circle.SetActive(true);
    }
    
    public void HideRangeCircle(GameObject tower)
    {
        GameObject circle = tower.GetComponent<Tower>().GetTowerRangeCircle();
        circle.SetActive(false);
    }

    void OnMouseEnter()
    {
        //prevent overlay ui from prohibiting player from clicking code - TO DO 
        if (!_buildManager.CanBuild())
        {
            return;
        }

        if (!_buildManager.HasMoney())
        {
            _renderer.material.color = _errorColor;
        }
        else
        {
            _renderer.material.color = _hoverColor;
        }
    }

    void OnMouseExit()
    {
        _renderer.material.color = _originalColor;
    }
}
