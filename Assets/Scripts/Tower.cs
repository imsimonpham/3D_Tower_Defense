using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Unity Setup Fields")]
    [SerializeField] private GameObject _target;
    [SerializeField] private string _enemyTag = "Enemy";
    [SerializeField] private GameObject _towerHead;
    [SerializeField] private GameObject _firePoint;
    [SerializeField] private GameObject _rangeCircle;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private ParticleSystem _laserImpactEffect;
    [SerializeField] private Light _laserLight;
    private Enemy _targetEnemy;

    [Header("Tower's Properties")]
    [SerializeField] private float _range = 15f;
    private float _rotationSpeed = 10f;
    [SerializeField] private float _yOffset;
    
    [Header("Bullet-based Tower")]
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private GameObject _bulletPrefab;
    private float _canFire = 0f;

    [Header("Laser-based Tower")] 
    [SerializeField] private bool _useLaser = false;
    [SerializeField] private float _dps = 30f;
    [SerializeField] private float _slowPct = 0.5f;
    

    void Start()
    {
        InvokeRepeating("ScanForTargets", 0f, 0.3f);
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
                   _laserImpactEffect.Stop();
                   _laserLight.enabled = false;
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
    
    void ShootLaser()
    {
        if (!_lineRenderer.enabled)
        {
            _lineRenderer.enabled = true;
            _laserImpactEffect.Play();
            _laserLight.enabled = true;
        }
        _lineRenderer.SetPosition(0, _firePoint.transform.position);

        RaycastHit hit;
        if (Physics.Raycast(_firePoint.transform.position, _target.transform.position - _firePoint.transform.position, out hit))
        {
            _lineRenderer.SetPosition(1, hit.point);
            //Dmg + slow
            _targetEnemy.TakeDamage(_dps * Time.deltaTime);
            _targetEnemy.Slow(_slowPct);
            
            //Laser Effect
            _laserImpactEffect.transform.position = hit.point;
            Vector3 dir = _firePoint.transform.position - _target.transform.position;
            _laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);
        }
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

        if (prioritizedEnemy != null)
        {
            _target = prioritizedEnemy;
            _targetEnemy = prioritizedEnemy.GetComponent<Enemy>();
        }
        else
        {
            _target = null;
        }
        _target = prioritizedEnemy != null ? prioritizedEnemy : null;
    }
    public float GetTowerRange()
    {
        return _range;
    }

    public GameObject GetTowerRangeCircle()
    {
        return _rangeCircle;
    }
    
    public float GetTowerYOffset()
    {
        return _yOffset;
    }

    public IEnumerator HideRangeCirle()
    {
        yield return new WaitForSeconds(2f);
        _rangeCircle.SetActive(false);
        yield return null;
    }
}
