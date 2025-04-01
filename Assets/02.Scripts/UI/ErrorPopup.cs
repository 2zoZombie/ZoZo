using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorPopup : MonoBehaviour
{
    public TextMeshProUGUI errorText;
    Coroutine coroutine;

    private void Awake()
    {
        UIManager.Instance.errorPopup = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowErrorMessage(string text)
    {
        gameObject.SetActive(true);

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(CoroutineShowErrorMessage(text));
    }

    public IEnumerator CoroutineShowErrorMessage(string text)
    {
        SetErrorText(text);
        yield return new WaitForSeconds(1f);
        coroutine = null;
        gameObject.SetActive(false);
    }

    void SetErrorText(string text)
    {
        errorText.text = text;

    }
}
