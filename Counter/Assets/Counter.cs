using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float _delay;

    private bool _isRunning = false;
    private int _count = 0;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_delay);
        StartCoroutine(RunCounting());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isRunning = !_isRunning;
        }
    }

    private IEnumerator RunCounting()
    {
        while (true)
        {
            DisplayCount();
            yield return _wait;
            yield return new WaitWhile(() => _isRunning == false);
            _count++;
        }
    }

    private void DisplayCount()
    {
        _text.text = _count.ToString();
    }
}
