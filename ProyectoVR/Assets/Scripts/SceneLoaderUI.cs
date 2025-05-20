using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderUI : MonoBehaviour
{
    public static SceneLoaderUI Instance { get; private set; }

    [Header("Fallback")]
    [Tooltip("Si un botón no envía nombre de escena se usará este.")]
    public string fallbackScene = "Nivel2";

    [Header("Player")]
    public GameObject player;                 // arrástralo una sola vez

    //  ▸ Datos que cruzan la carga
    private static GameObject pendingPlayer;
    private static Vector3 pendingPos;
    private static Vector3 pendingRot;

    // ─────────────────────────────────────────────────────────────
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);        // vive entre escenas
    }

    /// <summary>
    /// Carga la escena indicada y deja al jugador en la posición/rotación dadas.
    /// </summary>
    public void LoadTargetScene(string sceneName,
                                Vector3 spawnPosition,
                                Vector3 spawnRotationEuler)
    {
        string target = string.IsNullOrWhiteSpace(sceneName) ? fallbackScene : sceneName;
        if (string.IsNullOrWhiteSpace(target)) { Debug.LogError("Scene vacía"); return; }
        if (player == null) { Debug.LogError("Falta Player"); return; }

        // Transfiere datos
        pendingPlayer = player;
        pendingPos = spawnPosition;
        pendingRot = spawnRotationEuler;

        DontDestroyOnLoad(pendingPlayer);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(target);
    }

    // ─────────────────────────────────────────────────────────────
    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        if (pendingPlayer != null)
            pendingPlayer.transform.SetPositionAndRotation(
                pendingPos, Quaternion.Euler(pendingRot));

        SceneManager.sceneLoaded -= OnSceneLoaded;
        pendingPlayer = null;
    }
}
