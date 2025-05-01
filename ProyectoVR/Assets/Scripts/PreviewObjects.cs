using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObjects : MonoBehaviour
{
    #region ---------- PREVIEW DE OBJETOS ----------
    private readonly List<GameObject> _children = new();

    private void Awake()
    {
        foreach (Transform child in transform)
            _children.Add(child.gameObject);

        HideAll();
    }

    private void HideAll()
    {
        foreach (var go in _children) go.SetActive(false);
    }

    /// <summary>Activa solo el hijo indicado y apaga los demás.</summary>
    public void ShowObject(int index)
    {
        if (index < 0 || index >= _children.Count) return;

        for (int i = 0; i < _children.Count; i++)
            _children[i].SetActive(i == index);
    }
    #endregion


    #region ---------- TELE-TRANSPORTE ----------
    [System.Serializable]
    public struct TeleportPoint
    {
        public string name;          // Nombre legible en Inspector
        public Vector3 position;     // Coordenadas a las que enviar
        public Vector3 eulerAngles;  // Dirección en la que mirar
    }

    [Header("Teleport Settings")]
    [Tooltip("Transform del Player que va a moverse.")]
    [SerializeField] private Transform player;

    [Tooltip("Lista de destinos editables en el Inspector.")]
    [SerializeField] private List<TeleportPoint> teleportPoints = new();

    private Vector3 _savedPos;
    private Quaternion _savedRot;
    private bool _hasSaved;

    /// <summary>Envía al player al punto con índice dado.</summary>
    public void TeleportTo(int index)
    {
        if (player == null || index < 0 || index >= teleportPoints.Count) return;

        if (!_hasSaved)                   // Guarda la posición original solo la primera vez
        {
            _savedPos = player.position;
            _savedRot = player.rotation;
            _hasSaved = true;
        }

        var tp = teleportPoints[index];
        player.SetPositionAndRotation(tp.position, Quaternion.Euler(tp.eulerAngles));
    }

    /// <summary>Vuelve a la posición y rotación guardadas.</summary>
    public void ReturnToSaved()
    {
        if (!_hasSaved || player == null) return;

        player.SetPositionAndRotation(_savedPos, _savedRot);
        _hasSaved = false;
    }
    #endregion
}
