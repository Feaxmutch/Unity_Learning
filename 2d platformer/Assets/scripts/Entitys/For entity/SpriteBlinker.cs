using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteBlinker : MonoBehaviour
{
    private Material _spriteMaterial;
    private Coroutine _blink;

    private Color _defaultColor;

    private void Awake()
    {
        _spriteMaterial = GetComponent<SpriteRenderer>().material;
        _defaultColor = _spriteMaterial.color;
    }

    public void TryBlink(Color colorMultiplyer, float frequency)
    {
        if (_blink == null)
        {
            _blink = StartCoroutine(Blink(colorMultiplyer, frequency));
        }
    }

    private IEnumerator Blink(Color colorMultiplyer, float frequency)
    {
        WaitForSeconds wait = new(frequency);
        _spriteMaterial.color = _defaultColor * colorMultiplyer;
        yield return wait;
        _spriteMaterial.color = _defaultColor;
        yield return wait;
        _blink = null;
    }
}
