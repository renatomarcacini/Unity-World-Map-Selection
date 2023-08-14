using System;
using UnityEngine;

public class PlanetLevelFocus : MonoBehaviour
{
    public static PlanetLevelFocus Instance;

    [Header("Settings")]
    [Space(10)]
    [SerializeField] private float _rotationSpeed = 50.0f;

    public static Action OnFocusStarted;
    public static Action OnFocusComplete;

    private Transform _target;
    private bool _isFocusing = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        FocusPlanet();
    }

    private void FocusPlanet()
    {
        if (_target != null && _isFocusing)
        {
            Vector3 targetDirection = Helper.CalculateDifferenceNormalized(_target, Camera.main.transform);
            float minDistanceFocus = 0.02f;
            if (Mathf.Abs(targetDirection.x) > minDistanceFocus)
            {
                if (targetDirection.x > 0)
                    GameAsset.Instance.Planet.Rotate(Vector2.down, -_rotationSpeed * Time.deltaTime);
                else
                    GameAsset.Instance.Planet.Rotate(Vector2.down, _rotationSpeed * Time.deltaTime);
            }
            else
            {
                DisableFocus();
            }
        }
    }

    public void SetTargetAndActiveFocus(Transform target)
    {
        _target = target;
        EnableFocus();
    }

    private void EnableFocus()
    {
        _isFocusing = true;
        OnFocusStarted?.Invoke();
    }

    private void DisableFocus()
    {
        _isFocusing = false;
        OnFocusComplete?.Invoke();
    }
}