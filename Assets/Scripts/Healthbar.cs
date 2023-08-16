using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Target _target;
    [SerializeField] private Image _bar;
    
    private Coroutine _runningCoroutine;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _target.healthChanged += ManageCoroutine;
    }

    private void OnDisable()
    {
        _target.healthChanged -= ManageCoroutine;
    }

    private void ManageCoroutine(float newValue, Color newColor) 
    {
        if(_runningCoroutine != null) 
            StopCoroutine(_runningCoroutine );

        _runningCoroutine = StartCoroutine(ChangeSmoothly(newValue, newColor));
    }

    private IEnumerator ChangeSmoothly(float newValue, Color newColor)
    {
        float colorChange = 0.15f;
        var waitTime = new WaitForEndOfFrame();

        while (_slider.value != newValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, newValue, colorChange);
            _bar.color = newColor;          
            yield return waitTime;
        }
    }
}
