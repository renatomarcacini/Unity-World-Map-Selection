using UnityEngine;

public class OrbitPlanet : MonoBehaviour
{
    [Header("Settings")]
    [Space(10)]
    [SerializeField, Range(0,100)] private float _rotationSpeed = 50f;
    [SerializeField, Range(0,10)] private float _accelerationSpped = 2f;
    [SerializeField, Range(0,10)] private float _desaccelerationSpeed = 2f;

    private float _lastRotationX;
    private float _xRotation;
    private bool _isMousePressed;

    private bool _canDrag = true;

    private void OnEnable()
    {
        PlanetLevelFocus.OnFocusStarted += DisableDrag;
        PlanetLevelFocus.OnFocusComplete += EnableDrag;
    }

    private void OnDisable()
    {
        PlanetLevelFocus.OnFocusStarted -= DisableDrag;
        PlanetLevelFocus.OnFocusComplete -= EnableDrag;
    }

    private void Update()
    {
        if (!_canDrag)
            return;

        RotatePlanet();
        DesaccelerateRotate();
    }

    private void RotatePlanet()
    {
        if (Input.GetMouseButton(0))
        {
            _isMousePressed = true;
            _xRotation = Input.GetAxis("Mouse X") * _rotationSpeed;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isMousePressed = false;
        }

        GameAsset.Instance.Planet.Rotate(Vector2.down, Mathf.Lerp(_lastRotationX, _xRotation, _accelerationSpped * Time.deltaTime));
    }

    private void DesaccelerateRotate()
    {
        if (!_isMousePressed)
            _xRotation = Mathf.Lerp(_xRotation, 0, _desaccelerationSpeed * Time.deltaTime);
    }

    private void EnableDrag()
    {
        _xRotation = 0;
        _canDrag = true;
    }

    private void DisableDrag()
    {
        _canDrag = false;
    }

}