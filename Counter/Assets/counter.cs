using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class counter : MonoBehaviour
{
    private bool _isRunning = true;

    [SerializeField] private Text _text;
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(RunCountering(_delay));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isRunning = !_isRunning;
        }
    }

    private IEnumerator RunCountering(float delay)
    {
        int count = 0;
        var wait = new WaitForSeconds(delay);

        while (true)
        {
            DisplayCount(count);
            yield return wait;
            yield return new WaitUntil(() => _isRunning);
            count++;
        }
    }

    private void DisplayCount(int count)
    {
        _text.text = count.ToString();
    }
}
