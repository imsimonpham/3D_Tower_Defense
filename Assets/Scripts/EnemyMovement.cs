using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] private float _startSpeed;
    
    private PlayerStats _playerStats;
    private GameObject _target;
    private GameObject _waypoints;
    private int _waypointIndex = 0;

    void Start()
    {
        _speed = _startSpeed;

        _waypoints = GameObject.Find("Waypoints");
        _target = _waypoints.transform.GetChild(0).gameObject;
        _playerStats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if (_playerStats == null)
        {
            Debug.LogError("Player Stats is null!");
        }
    }

    void Update()
    {
        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(_target.transform.position, transform.position) <= 0.3f)
        {
            GetToNextWaypoint();
        }

        //reset enemy speed when out of range of spire
        _speed = _startSpeed;
    }

    void GetToNextWaypoint()
    {
        if(_waypointIndex >= _waypoints.transform.childCount -1 )
        {
            EndPath();
            return;
        }
        _waypointIndex++;
        _target = _waypoints.transform.GetChild(_waypointIndex).gameObject;
    }

    void EndPath()
    {
        _playerStats.ReduceLives();
        Destroy(this.gameObject);
    }

    public float GetStartSpeed()
    {
        return _startSpeed;
    }
    
    public float SetSpeed(float speed)
    {
        return _speed = speed;
    }
}
