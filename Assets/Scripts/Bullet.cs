using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 60f;
    private GameObject _target;

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
            Destroy(other.gameObject);
            GameObject bulletImpactEffect = Instantiate(_bulletImpactPrefab, transform.position, Quaternion.identity);
            Destroy(bulletImpactEffect, 1.5f);
            Destroy(this.gameObject);
        }
    }

    
}
