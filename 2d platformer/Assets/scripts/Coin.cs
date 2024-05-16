using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _initialScoreValue;

    public int ScoreValue { get; private set; }

    private void Start()
    {
        ScoreValue = _initialScoreValue;
    }
}
