using UnityEngine;
using UnityEngine.EventSystems;

public class SceneCleaner : MonoBehaviour
{
    void Awake()
    {
        // Mant�n solo el primer AudioListener
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        for (int i = 1; i < listeners.Length; i++)
            listeners[i].enabled = false;

        // Mant�n solo el primer EventSystem
        EventSystem[] es = FindObjectsOfType<EventSystem>();
        for (int i = 1; i < es.Length; i++)
            es[i].gameObject.SetActive(false);
    }
}
