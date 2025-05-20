using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance;
    [SerializeField] private CanvasGroup cg;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Asegura que haya un CanvasGroup
        if (cg == null)
            cg = GetComponent<CanvasGroup>();

        if (cg == null)
        {
            Debug.LogError("FadeScreen: no se encontró CanvasGroup");
            enabled = false;       // evita más errores
            return;
        }

        cg.alpha = 0;              // comienza transparente
        DontDestroyOnLoad(gameObject);
    }

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