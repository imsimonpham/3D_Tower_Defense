using UnityEngine;

[System.Serializable]

public class TowerBlueprint
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _upgradePrefab;
    [SerializeField] private int _cost;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private float _yOffset;

    public GameObject GetPrefab()
    {
        return _prefab;
    }

    public int GetCost()
    {
        return _cost;
    }
    
    public GameObject GetUpgradePrefab()
    {
        return _upgradePrefab;
    }

    public int GetUpgradeCost()
    {
        return _upgradeCost;
    }

    public float GetYOffset()
    {
        return _yOffset;
    }
}
