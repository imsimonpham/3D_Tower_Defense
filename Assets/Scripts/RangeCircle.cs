using UnityEngine;

public class RangeCircle : MonoBehaviour
{
    [SerializeField] private LineRenderer _circleRenderer;
    [SerializeField] private Tower _parent;
    private int _steps = 1000;
    private float _startWidth = 0.1f;
    private float _endWidth = 0.1f;

    void Update()
    {
        DrawCircle(_parent.GetTowerRange());
    }

    // steps: how many straight lines to create the circle
    public void DrawCircle(float radius)
    {
        _circleRenderer.positionCount = _steps;
        _circleRenderer.startWidth = _startWidth;
        _circleRenderer.endWidth = _endWidth;

        for (int currentStep = 0; currentStep < _steps; currentStep++)
        {
            float circProgress = (float)currentStep / (_steps - 1);
            float currentRadian = circProgress * 2 * Mathf.PI;
            float zScaled = Mathf.Cos(currentRadian);
            float xScaled = Mathf.Sin(currentRadian);

            float z = zScaled * radius; 
            float x = xScaled * radius;

            Vector3 pos = new Vector3(x, 0.8f, z) + _parent.transform.position;
            _circleRenderer.SetPosition(currentStep, pos) ;
        }
    }
}
