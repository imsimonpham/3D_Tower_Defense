using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 60f;
    [SerializeField] private float _dmg = 50f;
    private GameObject _target;
    [SerializeField] private float _explosionRadius = 0f;
    [SerializeField] GameObject _bulletImpactPrefab;
    

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * _speed, Space.World);
    }

    public void SeekTarget(GameObject target)
    {
        _target = target;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            HitTarget(other.gameObject);
        }
    }

    void HitTarget(GameObject target)
    {
        GameObject bulletImpactEffect = Instantiate(_bulletImpactPrefab, transform.position, Quaternion.identity);
        if (_explosionRadius > 0)
        {
            AOEDamage();
        }
        else
        {
            Damage(target);
        }
        Destroy(bulletImpactEffect, 2f);
        Destroy(this.gameObject);
    }

    void AOEDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Damage(collider.transform.gameObject);
            }
        }
    }

    void Damage(GameObject enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(_dmg);
        }
    }
}
