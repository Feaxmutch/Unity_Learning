using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _score.Changed += WhriteScore;
    }

    private void WhriteScore()
    {
        _text.SetText($"{_score.Value}");
    }
}
