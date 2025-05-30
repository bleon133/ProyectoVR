using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayExit : MonoBehaviour
{
    [Tooltip("Nombre de la escena a cargar (debe estar añadida en Build Settings).")]
    [SerializeField] private string sceneName = "Piso1Mike";

    private const float Delay = 4f;   // segundos

    /// <summary>
    /// Llama a este método desde el evento **On Click** del botón.
    /// </summary>
    public void TriggerLoad()
    {
        StartCoroutine(LoadCoroutine());
    }

    private IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(Delay);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Llama a este método desde el evento **On Click** del botón.
    /// </summary>
    public void TriggerQuit()
    {
        StartCoroutine(QuitCoroutine());
    }

    private IEnumerator QuitCoroutine()
    {
        yield return new WaitForSeconds(Delay);
        Application.Quit();
        Debug.Log("La aplicación se ha cerrado (esto solo se ve en la build, no en el Editor).");
    }
}
