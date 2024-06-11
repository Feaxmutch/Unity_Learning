using System.Collections;
using UnityEngine;

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _interpolatingSpeed;

    private float _minOffset = 0.01f;
    private Coroutine _sliderInterpolating;

    protected override void ShowHealth()
    {
        if(_sliderInterpolating != null)
        {
            StopCoroutine(_sliderInterpolating);
        }

        _sliderInterpolating = StartCoroutine(SliderInterpolating());
    }

    private IEnumerator SliderInterpolating()
    {
        float startValue = _slider.value;
        float endValue = GetNormmalizedHealth();
        float interpolatingValue = 0;
        float minInterpolatingValue = 0;
        float maxInterpolatingValue = 1;

        while (_slider.value != endValue)
        {
            float offset = Time.deltaTime * _interpolatingSpeed * (maxInterpolatingValue - interpolatingValue);
            offset = Mathf.Max(_minOffset, offset);
            interpolatingValue += offset;
            interpolatingValue = Mathf.Clamp(interpolatingValue, minInterpolatingValue, maxInterpolatingValue);
            _slider.value = Mathf.Lerp(startValue, endValue, interpolatingValue);
            yield return null;
        }

        _sliderInterpolating = null;
    }
}
