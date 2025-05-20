using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance { get; private set; }

    [Header("Jugador")]
    public GameObject Player;            // arrástralo aquí

    private readonly List<TeleportPoint> allTeleports = new();
    private GameObject lastTeleportPoint;

    private int currentFloorLayer;       // layer activo

    //──────────────────────────────────────────────────────────────
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 1) Piso/layer de la escena que arranca el juego
        currentFloorLayer = MapSceneNameToFloor(SceneManager.GetActiveScene().name);
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    //──────────────────────────────────────────────────────────────
    public void RegisterTeleport(TeleportPoint tp)
    {
        allTeleports.Add(tp);

        // 2) Actívalo o no en el mismo momento del registro
        tp.gameObject.SetActive(tp.gameObject.layer == currentFloorLayer);
    }

    // Llamado automáticamente al terminar de cargar cualquier escena
    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        // Si tus escenas se llaman igual que los layers (“Piso1”, “Piso2”…)
        currentFloorLayer = LayerMask.NameToLayer(s.name);

        // Si NO coinciden, haz tu propio mapeo:
        // currentFloorLayer = (s.name == "NivelSubterraneo") ? LayerMask.NameToLayer("Piso2") : LayerMask.NameToLayer("Piso1");

        RefreshTeleportActivation();
        lastTeleportPoint = null;
    }

    //──────────────────────────────────────────────────────────────
    public void ChangeFloor(int floorLayer)
    {
        currentFloorLayer = floorLayer;
        RefreshTeleportActivation();
    }

    private int MapSceneNameToFloor(string sceneName)
    {
        // Si el nombre de la escena ES igual al nombre del layer (“Piso1”, “Piso2”…)
        int layer = LayerMask.NameToLayer(sceneName);
        if (layer != -1) return layer;

        // ───── Mapeo manual ─────
        // Ejemplo: escena “NivelSubterraneo” → layer “Piso2”
        switch (sceneName)
        {
            case "NivelSubterraneo": return LayerMask.NameToLayer("Piso2");
            case "NivelTerrestre": return LayerMask.NameToLayer("Piso1");
            // añade las combinaciones que necesites
            default: return LayerMask.NameToLayer("Piso1");   // fallback
        }
    }

    // Activa sólo los teleports cuyo GameObject esté en el layer actual
    private void RefreshTeleportActivation()
    {
        for (int i = allTeleports.Count - 1; i >= 0; i--)
        {
            TeleportPoint tp = allTeleports[i];
            if (!tp) { allTeleports.RemoveAt(i); continue; }       // limpiamos zombies
            tp.gameObject.SetActive(tp.gameObject.layer == currentFloorLayer);
        }
    }

    // Mantén la lógica de exclusión del portal usado
    public void DisableTeleportPoint(GameObject teleportGO)
    {
        if (lastTeleportPoint) lastTeleportPoint.SetActive(true);

        teleportGO.SetActive(false);
        lastTeleportPoint = teleportGO;

#if UNITY_EDITOR
        Player?.GetComponent<CardboardSimulator>()?.UpdatePlayerPositonSimulator();
#endif
    }
}
