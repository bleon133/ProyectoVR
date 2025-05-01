using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum TeleportType
{
    Normal,
    Peligroso,
    Seguro
}

public class Tps : MonoBehaviour
{
    public UnityEvent OnTeleportEnter;
    public UnityEvent OnTeleport;
    public UnityEvent OnTeleportExit;
    private AudioSource audioSource;
    [SerializeField] private TeleportType teleportType;

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnPointerEnterXR()
    {
        OnTeleportEnter?.Invoke();
    }

    public void OnPointerClickXR()
    {
        ExecuteTeleportation();
        OnTeleport?.Invoke();

        EnemyProb.Instance.RegisterTeleport(teleportType);

        TeleportManager.Instance.DisableTeleportPoint(gameObject);
    }

    public void OnPointerExitXR()
    {
        OnTeleportExit.Invoke();
    }

    private void ExecuteTeleportation()
    {
        GameObject player = TeleportManager.Instance.Player;
        player.transform.position = transform.position;
        Camera camera = player.GetComponentInChildren<Camera>();
        float rotY = transform.rotation.eulerAngles.y - camera.transform.localEulerAngles.y;
        player.transform.rotation = Quaternion.Euler(0, rotY, 0);
    }
}
