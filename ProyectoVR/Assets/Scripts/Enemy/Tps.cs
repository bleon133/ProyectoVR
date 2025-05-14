using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tps : MonoBehaviour
{
    public UnityEvent OnTeleportEnter;
    public UnityEvent OnTeleport;
    public UnityEvent OnTeleportExit;
    private Vector3 _targetDir = Vector3.forward;
    private AudioSource audioSource;
    [SerializeField] private TeleportType teleportType;

    [Header("Arrow settings")]
    [SerializeField] private float arrowDistance = 0.35f;   // qué tan lejos del centro
    [SerializeField] private float arrowHeight = 0.05f;   // altura sobre el suelo

    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnPointerEnterXR()
    {
        /* 1) Calcula la dirección contraria a la mirada */
        _targetDir = CameraPointerManager.Instance.hitPoint - transform.position;
        _targetDir.y = 0f;
        _targetDir = -_targetDir;                             // invertimos

        /* 2) Referencia a la flecha */
        Transform arrow = transform.GetChild(0);

        /* 3) Rota la flecha */
        if (_targetDir.sqrMagnitude > 0.001f)
            arrow.rotation = Quaternion.LookRotation(_targetDir, Vector3.up);

        /* 4) Re-posiciona la flecha: centro + dirNorm * distancia + altura */
        Vector3 dirNorm = _targetDir.normalized;
        arrow.position = transform.position
                        + dirNorm * arrowDistance
                        + Vector3.up * arrowHeight;

        /* 5) Mostrarla y disparar evento */
        arrow.gameObject.SetActive(true);
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
        transform.GetChild(0).gameObject.SetActive(false);
        OnTeleportExit.Invoke();
    }

    private void ExecuteTeleportation()
    {
        GameObject player = TeleportManager.Instance.Player;
        /* ??? POSICIÓN ??? */
        player.transform.position = transform.position;
        /* ??? ROTACIÓN ??? */
        if (_targetDir.sqrMagnitude > 0.001f)
            player.transform.rotation = Quaternion.LookRotation(_targetDir, Vector3.up);
    }
}
