using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TeleportPoint : MonoBehaviour
{
    public UnityEvent OnTeleportEnter;
    public UnityEvent OnTeleport;
    public UnityEvent OnTeleportExit;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false); //Desactivar la flecha
    }

    public void OnPointerEnterXR()
    {
        OnTeleportEnter?.Invoke();
    }

    public void OnPointerClickXR()
    {
        ExecuteTeleportation();
        OnTeleport?.Invoke();
        TeleportManager.Instance.DisableTeleportPoint(gameObject);
    }

    public void OnPointerExitXR()
    {
        OnTeleportExit.Invoke();
    }

    private void ExecuteTeleportation()
    {
        GameObject player = TeleportManager.Instance.Player;

        /* ???????? POSICIÓN ???????? */
        Vector3 destino = CameraPointerManager.Instance.hitPoint;
        destino.y = transform.position.y;      // iguala la altura del TP para evitar hundirse o flotar
        player.transform.position = destino;

        /* ???????? ROTACIÓN ???????? */
        Vector3 dir = destino - transform.position;   // radial desde el centro
        dir.y = 0f;
        if (dir.sqrMagnitude > 0.001f)
        {
            player.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }

}
