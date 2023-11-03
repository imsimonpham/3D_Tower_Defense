using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private GameObject[] _waypoints;

    private void Start()
    {
        //waypoints = new GameObject[arraySize];
        _waypoints = new GameObject[transform.childCount];
        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = transform.GetChild(i).gameObject;
        }
    }

    public GameObject[] GetWayPointsCount()
    {
        return _waypoints;
    }
}
