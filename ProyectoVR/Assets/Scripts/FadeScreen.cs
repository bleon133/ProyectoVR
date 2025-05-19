using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance;      // Singleton rápido
    [SerializeField] private CanvasGroup cg; // CanvasGroup asignado desde el inspector

    void Awake() => Instance = this;

    public IEnumerator FadeOut(float t)
    {
        for (float a = 0; a <= 1; a += Time.unscaledDeltaTime / t)
        {
            cg.alpha = a;
            yield return null;
        }
        cg.alpha = 1;
    }

    public IEnumerator FadeIn(float t)
    {
        for (float a = 1; a >= 0; a -= Time.unscaledDeltaTime / t)
        {
            cg.alpha = a;
            yield return null;
        }
        cg.alpha = 0;
    }
}
