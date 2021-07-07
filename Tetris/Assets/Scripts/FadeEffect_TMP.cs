using System.Collections;
using UnityEngine;
using TMPro;

public class FadeEffect_TMP : MonoBehaviour
{
    [SerializeField]
    private float           fadeTime = 0.5f;
    private TextMeshProUGUI fadeText;

    private void Awake()
    {
        fadeText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine("FadeLoop");
    }

    private IEnumerator FadeLoop()
    {
        while ( true )
        {
            yield return StartCoroutine(Fade(1, 0));

            yield return StartCoroutine(Fade(0, 1));
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while ( percent < 1 )
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color    = fadeText.color;
            color.a        = Mathf.Lerp(start, end, percent);
            fadeText.color = color;

            yield return null;
        }
    }
}
