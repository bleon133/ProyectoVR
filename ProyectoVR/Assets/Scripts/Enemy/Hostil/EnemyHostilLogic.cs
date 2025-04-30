using UnityEngine;

public class EnemyHostilLogic : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip hostilClip;

    [Header("Detección de movimiento")]
    [SerializeField] private float detectionThreshold = 1f;
    [SerializeField] private float tiempoMaxEspera = 10f;

    private Transform playerCamera;
    private Vector3 lastEulerAngles;
    private float tiempoSinMovimiento = 0f;
    private bool hasAttacked = false;

    private void OnEnable()
    {
        hasAttacked = false;
        tiempoSinMovimiento = 0f;

        // Reproducir audio
        AudioSource source = GetComponentInParent<AudioSource>();
        if (source != null && hostilClip != null)
        {
            source.clip = hostilClip;
            source.Play();
        }

        // Detectar cámara
        Camera cam = Camera.main;
        if (cam != null)
        {
            playerCamera = cam.transform;
            lastEulerAngles = playerCamera.rotation.eulerAngles;
        }
    }

    private void Update()
    {
        if (hasAttacked || playerCamera == null) return;

        Vector3 currentEuler = playerCamera.rotation.eulerAngles;
        float delta = Vector3.Distance(currentEuler, lastEulerAngles);

        // Detectar movimiento de cabeza
        if (delta > detectionThreshold)
        {
            Atacar();
        }
        else
        {
            tiempoSinMovimiento += Time.deltaTime;

            if (tiempoSinMovimiento >= tiempoMaxEspera)
            {
                Debug.Log("El jugador no se movió. El enemigo se retira.");
                Desactivarse();
            }
        }

        lastEulerAngles = currentEuler;
    }

    private void Atacar()
    {
        Debug.Log("¡El enemigo ataca al jugador!");
        hasAttacked = true;
        Desactivarse();
    }

    private void Desactivarse()
    {
        // Puedes desactivar solo el modo hostil, o todo el enemigo
        // Desactiva el GameObject padre (Enemy completo):
        transform.parent.gameObject.SetActive(false);

        // Si prefieres desactivar solo el bloque hostil:
        // gameObject.SetActive(false);
    }
}