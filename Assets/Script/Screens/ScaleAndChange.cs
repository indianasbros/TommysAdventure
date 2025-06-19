using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaleAndChange : MonoBehaviour
{
    public float scaleSpeed = 1f;
    public float maxScale = 2f;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        StartCoroutine(AnimateAndChange());
    }

    System.Collections.IEnumerator AnimateAndChange()
    {
        float elapsed = 0f;

        while (elapsed < 2f)
        {
            transform.localScale = Vector3.Lerp(initialScale, Vector3.one * maxScale, elapsed / 2f);
            elapsed += Time.deltaTime * scaleSpeed;
            yield return null;
        }

        transform.localScale = Vector3.one * maxScale;

        SceneManager.LoadScene("Menu");
    }
}