using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Slider))]

public class Healthbar : MonoBehaviour
{
    private Coroutine _runningCoroutine;
    private UnityEngine.UI.Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<UnityEngine.UI.Slider>();
    }

    public void ManageCoroutine(float newValue, Color transitionColor) 
    {
        if(_runningCoroutine != null) 
            StopCoroutine(_runningCoroutine );

        _runningCoroutine = StartCoroutine(ChangeSmoothly(newValue, transitionColor));
    }

    private IEnumerator ChangeSmoothly(float newValue, Color transitionColor)
    {
        float colorChange = 0.15f;

        while (_slider.value != newValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, newValue, colorChange);
            _slider.transform.Find("Fill Area").Find("Fill").GetComponent<UnityEngine.UI.Image>().color = transitionColor;          
            yield return new WaitForEndOfFrame();
        }
    }
}
