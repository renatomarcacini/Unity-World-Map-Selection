using UnityEngine;

public class GameAsset : MonoBehaviour
{
    public static GameAsset Instance;
    public Transform Planet;

    public float MinDistanceToSelectButton = 0.25f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
