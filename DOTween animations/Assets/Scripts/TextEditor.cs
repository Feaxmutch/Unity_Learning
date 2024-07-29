using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : DOTweenAnimator
{
    [SerializeField] private Text _text;

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.SetLoops(-1, LoopType.Restart);
        sequence.Insert(0, _text.DOText("��������� �����", Duration));
        sequence.Insert(Duration, _text.DOText(" ����������", Duration).SetRelative());
        sequence.Insert(Duration * 2, _text.DOText("������ � ���������", Duration, true, ScrambleMode.All));
    }
}
