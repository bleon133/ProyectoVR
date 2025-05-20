using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Asigna nombre de escena y spawn para este botón UI.
/// </summary>
[RequireComponent(typeof(Button))]
public class SceneButton : MonoBehaviour
{
    [Header("Escena y spawn")]
    public string sceneName;               // destino
    public Vector3 spawnPosition = new(0, 1, 0);
    public Vector3 spawnRotationEuler;

    private void Awake()
    {
        // Listener → llama al singleton con los parámetros de este botón
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (SceneLoaderUI.Instance == null)
            {
                Debug.LogError("SceneLoaderUI no encontrado en la primera escena");
                return;
            }

            SceneLoaderUI.Instance.LoadTargetScene(
                sceneName,
                spawnPosition,
                spawnRotationEuler);
        });
    }
}
