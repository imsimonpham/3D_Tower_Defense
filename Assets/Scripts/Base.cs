using UnityEngine;

public class Base : MonoBehaviour
{
    private Color _hoverColor;
    private Color _originalColor;
    private Renderer _renderer;

    private GameObject _existingTurret;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _hoverColor = Color.green;
    }

    void OnMouseDown()
    {
        if (_existingTurret != null)
        {
            Debug.Log("Can't build");
            return;
        }

        // build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        Vector3 buildPos = new Vector3(transform.position.x, transform.position.y + 0.825f, transform.position.z);
        Instantiate(turretToBuild, buildPos, Quaternion.identity);
    }

    void OnMouseEnter()
    {
        _renderer.material.color = _hoverColor;
    }

    void OnMouseExit()
    {
        _renderer.material.color = _originalColor;
    }
}
