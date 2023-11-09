using UnityEngine;

[System.Serializable]

public class TowerBlueprint
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _cost;
    [SerializeField] private float _yOffset;

    public GameObject GetPrefab()
    {
        return _prefab;
    }

    public int GetCost()
    {
        return _cost;
    }

    public float GetYOffset()
    {
        return _yOffset;
    }
}
