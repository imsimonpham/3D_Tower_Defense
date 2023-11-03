using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    private float _startSpeed = 7f;

    private GameObject _target;
    private GameObject _waypoints;
    private int _waypointIndex = 0;

    void Start()
    {
        _speed = _startSpeed;

        _waypoints = GameObject.Find("Waypoints");

        _target = _waypoints.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir.normalized * _speed *Time.deltaTime, Space.World);

        if (Vector3.Distance(_target.transform.position, transform.position) <= 0.3f)
        {
            GetToNextWaypoint();
        }
    }

    void GetToNextWaypoint()
    {
        if(_waypointIndex >= _waypoints.transform.childCount -1 )
        {
            Destroy(this.gameObject);
            return;
        }
        _waypointIndex++;
        _target = _waypoints.transform.GetChild(_waypointIndex).gameObject;
    }
}
