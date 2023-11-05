using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _range = 15f;
    [SerializeField] private string _enemyTag = "Enemy";

    [SerializeField] private GameObject _turretHead;
    [SerializeField] private GameObject _gunPoint;
    private float _rotationSpeed = 10f;

    void Start()
    {
        InvokeRepeating("ScanForTargets", 0f, 0.3f);
    }

    void Update()
    {
       if(_target == null)
       {
            return;
       }
       RotateTurretHead();
    }

    void RotateTurretHead()
    {
        Vector3 dir = _target.transform.position - transform.position;
        Quaternion lookToRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_turretHead.transform.rotation, lookToRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        _turretHead.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void ScanForTargets()
    {
        // Find all colliders in the turret's range
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);

        // Target the enemy based on index
        int smallestEnemyIndex = int.MaxValue;
        GameObject prioritizedEnemy = null;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                int enemyIndex = enemy.GetEnemyIndex();

                if (enemyIndex < smallestEnemyIndex)
                {
                    smallestEnemyIndex = enemyIndex;
                    prioritizedEnemy = enemy.gameObject;
                }
            }
        }

        if (prioritizedEnemy != null)
        {
            _target = prioritizedEnemy;
        } else
        {
            _target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
            Handles.color =  Color.yellow;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
        #endif
        if(_target != null) { 
            Gizmos.color = Color.red;      
            Gizmos.DrawLine(_gunPoint.transform.position, _target.transform.position);
        }
    }
}
