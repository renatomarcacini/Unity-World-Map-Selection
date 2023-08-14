using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [Header("Elements")]
    [Space(10)]
    [SerializeField] private TextMeshPro _indexLevelText;
    [SerializeField] private Animator _levelButtonAnimator;
    [SerializeField] private AnimationClip _clickClip;

    [Header("Settings")]
    [Space(10)]
    [SerializeField] private int _index;
    [SerializeField] private string _title;
    [SerializeField] private int _amountOfPlayers = 4;
    [SerializeField] private int _ticketPrice = 2000;
    [SerializeField] private int _rewardPrice = 10000;

    private void Awake()
    {
        _title = $"Level {_index}";
        _indexLevelText.text = _index.ToString();
    }

    private void OnMouseDown()
    {
        Vector3 targetDirection = Helper.CalculateDifferenceNormalized(transform, Camera.main.transform);

        if (Mathf.Abs(targetDirection.x) > GameAsset.Instance.MinDistanceToSelectButton)
            return;

        LevelSelection.Instance.SetCurrentAndInfoLevel(this);
        PlanetLevelFocus.Instance.SetTargetAndActiveFocus(transform);
        _levelButtonAnimator.Play(_clickClip.name);
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetInfo()
    {
        return $"Players: {_amountOfPlayers}\nTicket: ${_ticketPrice}\nReward: ${_rewardPrice}";
    }
}