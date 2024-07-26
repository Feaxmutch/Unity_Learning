using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextEditor : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float _duration;

    private void Start()
    {
        StartCoroutine(EditingText());
    }

    private IEnumerator EditingText()
    {
        Tween tween;

        while (enabled)
        {
            tween = _text.DOText("Изменённый текст", _duration);
            yield return new WaitWhile(() => tween.active);
            tween = _text.DOText(" дополнение", _duration).SetRelative();
            yield return new WaitWhile(() => tween.active);
            tween = _text.DOText("Замена с перебором", _duration, true, ScrambleMode.All);
            yield return new WaitWhile(() => tween.active);
        }
    }
}
