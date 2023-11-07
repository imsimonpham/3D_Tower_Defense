using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // store a reference to itself
    public static BuildManager instance;
    
    private GameObject _turretToBuild;
    [SerializeField] private GameObject _simpleTurretPrefab;

    void Start()
    {
        if (instance != null) {
            Debug.LogError("More than one build manager in scene!");
            return;
        }

        instance = this;

        _turretToBuild = _simpleTurretPrefab;
    }

    public GameObject GetTurretToBuild() { return _turretToBuild; }
}
