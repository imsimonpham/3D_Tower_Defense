using UnityEngine;

public class TowerBase : MonoBehaviour
{
    private Color _hoverColor;
    private Color _errorColor;
    private Color _originalColor;
    private Renderer _renderer;
   
    [Header("Optional")]
    [SerializeField] private GameObject _existingTower;
    
    private BuildManager _buildManager;
    private PlayerStats _playerStats;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _hoverColor = Color.green;
        _errorColor = Color.red;
        _buildManager = BuildManager.instance;
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
        
        _buildManager.BuildTowerOn(this);
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
