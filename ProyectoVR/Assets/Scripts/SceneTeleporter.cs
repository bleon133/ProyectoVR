using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using NaughtyAttributes;

public class SceneTeleporter : MonoBehaviour
{
    [Header("Destino")]
    [Scene]                     // Atributo de UnityEngine (2019.3+) para elegir escena en el Inspector
    public string targetScene;  // Nombre exacto de la escena destino (debe estar en Build Settings)
    public string targetSpawnId;// Identificador del punto de aparición dentro de la escena destino

    [Header("Opciones")]
    public bool keepSourceSceneActive = true; // Si false: desactiva renderización pero no descarga
    public float fadeTime = .25f;             // Para una transición elegante (opcional)

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine(TeleportRoutine(other.gameObject));
    }

    private IEnumerator TeleportRoutine(GameObject player)
    {
        // 1. Fundido opcional
        if (fadeTime > 0) yield return FadeScreen.Instance.FadeOut(fadeTime);

        // 2. Cargar escena destino si no está cargada
        if (!SceneManager.GetSceneByName(targetScene).isLoaded)
        {
            yield return SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
        }

        // 3. Buscar el punto de aparición en la escena destino
        Scene dst = SceneManager.GetSceneByName(targetScene);
        SpawnPoint spawn = FindSpawnInScene(dst, targetSpawnId);
        if (spawn == null)
        {
            Debug.LogError($"Spawn ID «{targetSpawnId}» no encontrado en {targetScene}");
            yield break;
        }

        // 4. Mover y re-orientar al jugador
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc) cc.enabled = false; // Evitar que el controlador rechace la reubicación
        player.transform.SetPositionAndRotation(spawn.transform.position, spawn.transform.rotation);
        if (cc) cc.enabled = true;

        // 5. Ajustar escena activa (necesario para iluminación/baked GI)
        SceneManager.SetActiveScene(dst);

        // 6. Opcional: desactivar u ocultar la escena de origen
        if (!keepSourceSceneActive)
        {
            Scene src = gameObject.scene;
            SetSceneObjectsActive(src, false);
        }

        // 7. Fundido de entrada
        if (fadeTime > 0) yield return FadeScreen.Instance.FadeIn(fadeTime);
    }

    private SpawnPoint FindSpawnInScene(Scene scene, string id)
    {
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            foreach (SpawnPoint s in root.GetComponentsInChildren<SpawnPoint>(true))
                if (s.spawnId == id) return s;
        }
        return null;
    }

    private void SetSceneObjectsActive(Scene scene, bool active)
    {
        foreach (GameObject root in scene.GetRootGameObjects())
            root.SetActive(active);
    }

    // Debajo de SetSceneObjectsActive(...)
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
    [ContextMenu("Test Teleport")]   // clic derecho en el inspector para probar
#endif
    public void TeleportViaUIButton()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            StartCoroutine(TeleportRoutine(player));
        else
            Debug.LogError("No se encontró ningún objeto con tag 'Player'");
    }

}
