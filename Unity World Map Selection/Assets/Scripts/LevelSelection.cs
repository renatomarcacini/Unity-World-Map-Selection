using TMPro;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public static LevelSelection Instance;

    [Header("Elements")]
    [Space(10)]
    [SerializeField] private RectTransform _hoverPanelLevel;

    [SerializeField] private TextMeshProUGUI _titleLevelText;
    [SerializeField] private TextMeshProUGUI _infoLevelText;

    [Header("Settings")]
    [Space(10)]
    [SerializeField] private Vector3 _hoverPanelOffset;

    private Camera _mainCamera;
    private LevelButton _currentLevelButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _hoverPanelLevel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_currentLevelButton == null)
            return;
        ActiveAndPositionHoverPanel();

        Vector3 targetDirection = Helper.CalculateDifferenceNormalized(_currentLevelButton.transform, Camera.main.transform);

        if (Mathf.Abs(targetDirection.x) > GameAsset.Instance.MinDistanceToSelectButton)
        {
            _currentLevelButton = null;
            _hoverPanelLevel.gameObject.SetActive(false);
        }
    }

    private void ActiveAndPositionHoverPanel()
    {
        if (!_hoverPanelLevel.gameObject.activeSelf)
            _hoverPanelLevel.gameObject.SetActive(true);

        AdjustHoverPanelPosition(_currentLevelButton.transform.position);
    }

    private void AdjustHoverPanelPosition(Vector3 targetPosition)
    {
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(targetPosition);
        screenPosition.y = Screen.height - screenPosition.y;

        float panelWidth = _hoverPanelLevel.rect.width;
        float panelHeight = _hoverPanelLevel.rect.height;
        screenPosition.x = screenPosition.x + panelWidth * _hoverPanelOffset.x;
        screenPosition.y = Screen.height - screenPosition.y + panelHeight * _hoverPanelOffset.y;

        _hoverPanelLevel.transform.position = screenPosition;
    }

    public void SetCurrentAndInfoLevel(LevelButton levelButton)
    {
        _currentLevelButton = levelButton;
        UpdateLevelInfo();
    }

    private void UpdateLevelInfo()
    {
        _titleLevelText.text = _currentLevelButton.GetTitle();
        _infoLevelText.text = _currentLevelButton.GetInfo();
    }

    public LevelButton GetCurrentLevel()
    {
        return _currentLevelButton;
    }
}