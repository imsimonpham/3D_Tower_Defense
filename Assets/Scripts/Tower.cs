using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    [SerializeField] private GameObject _target;
    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private GameObject _towerHead;
    [SerializeField] private GameObject _firePoint;
    [SerializeField] private GameObject _rangeCircle;

    [Header("Tower's Properties")]
    [SerializeField] private float _range = 15f;
    private float _rotationSpeed = 10f;
    
    [Header("Bullet-based Tower")]
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private GameObject _bulletPrefab;
    private float _canFire = 0f;

    [Header("Laser-based Tower")] 
    [SerializeField] private bool _useLaser = false;
    [SerializeField] private LineRenderer _lineRenderer;

    void Start()
    {
        InvokeRepeating("ScanForTargets", 0f, 0.3f);
        _rangeCircle.SetActive(true);
    }

    void Update()
    {
       if(_target == null)
       {
           if (_useLaser)
           {
               if (_lineRenderer.enabled)
               {
                   _lineRenderer.enabled = false;
               }
           }
           return;
       }

       RotateTowerHead();

       if (_useLaser)
       {
           ShootLaser();
       }
       else
       {
           if(_canFire <= 0f)
           {
               ShootBullets();
               _canFire = 1f / _fireRate;
           }
           _canFire -= Time.deltaTime;   
       }
    }

    private void OnMouseDown()
    {
        //_rangeCircle.SetActive(true);
        //Debug.Log("Turret Clicked");
    }

    void ShootLaser()
    {
        if (!_lineRenderer.enabled)
        {
            _lineRenderer.enabled = true;
        }
        _lineRenderer.SetPosition(0, _firePoint.transform.position);
        _lineRenderer.SetPosition(1, _target.transform.position);
    }

    void ShootBullets()
    {
        GameObject bulletGO = Instantiate(_bulletPrefab, _firePoint.transform.position, _firePoint.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) {
            bullet.SeekTarget(_target);
        }
    }

    void RotateTowerHead()
    {
        Vector3 dir = _target.transform.position - transform.position;
        Quaternion lookToRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(_towerHead.transform.rotation, lookToRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        _towerHead.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void ScanForTargets()
    {
        // Find all colliders in the tower's range
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);
        
        // Target the enemy based on index
        int smallestEnemyIndex = int.MaxValue;
        GameObject prioritizedEnemy = null;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(_enemyTag))
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
        _target = prioritizedEnemy != null ? prioritizedEnemy : null;
    }
    public float GetTowerRange()
    {
        return _range;
    }
}