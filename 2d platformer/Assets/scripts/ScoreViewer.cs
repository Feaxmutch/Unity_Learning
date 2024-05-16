using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private string _textBeforeValue;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.ScoreChanged += DisplayScore;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= DisplayScore;
    }

    private void Start()
    {
        _textMeshPro.text = _textBeforeValue + (int)default;
    }

    private void DisplayScore()
    {
        _textMeshPro.text = _textBeforeValue + _player.Score;
    }
}
