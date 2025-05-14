using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;

    [Header("Player Footstep Settings (Crujido de Madera)")]
    public AudioClip dangerousTeleportSound;
    [Range(0f, 1f)] public float dangerousSoundVolume = 1f;
    public AudioSource playerFootAudioSource; // Asignar en Inspector (hijo del jugador)

    [Header("Player Footstep Randomization")]
    public Vector2 footVolumeRange = new Vector2(0.8f, 1f);
    public Vector2 footPitchRange = new Vector2(0.95f, 1.05f);

    [Header("Enemy Footstep Settings (Pisadas Aleatorias)")]
    public AudioClip footstepsSound;
    [Range(0f, 1f)] public float footstepsSoundVolume = 1f;
    public Transform playerTransform; // Asignar en Inspector
    public GameObject audioEmitterPrefab; // Prefab con AudioSource configurado en 3D

    private GameObject currentFootstepEmitter = null;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Reproduce el crujido de madera bajo los pies del jugador, con volumen y pitch aleatorios.
    /// </summary>
    public void PlayPlayerFootCreak()
    {
        if (dangerousTeleportSound != null && playerFootAudioSource != null)
        {
            float randomVolume = Random.Range(footVolumeRange.x, footVolumeRange.y);
            float randomPitch = Random.Range(footPitchRange.x, footPitchRange.y);

            playerFootAudioSource.pitch = randomPitch;
            playerFootAudioSource.PlayOneShot(dangerousTeleportSound, randomVolume);
        }
        else
        {
            Debug.LogWarning("[SoundManager] Faltan referencias para reproducir el sonido de madera.");
        }
    }

    /// <summary>
    /// Reproduce las pisadas del enemigo en una posición aleatoria alrededor del jugador.
    /// </summary>
    public void PlayEnemyFootsteps()
    {
        if (footstepsSound == null || playerTransform == null || audioEmitterPrefab == null)
        {
            Debug.LogWarning("[SoundManager] Faltan referencias para reproducir pisadas del enemigo.");
            return;
        }

        // Si ya existe un emisor activo, no reproducir otro paso aún
        if (currentFootstepEmitter != null)
        {
            Debug.Log("Esperando a que el sonido de pasos actual termine.");
            return;
        }

        StartCoroutine(PlayFootstepsWithDelay(1.0f));
    }

    private System.Collections.IEnumerator PlayFootstepsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 randomOffset = Random.insideUnitSphere * Random.Range(2f, 8f);
        randomOffset.y = 0f;

        Vector3 spawnPosition = playerTransform.position + randomOffset;

        currentFootstepEmitter = Instantiate(audioEmitterPrefab, spawnPosition, Quaternion.identity);

        AudioSource emitterSource = currentFootstepEmitter.GetComponent<AudioSource>();
        if (emitterSource != null)
        {
            emitterSource.clip = footstepsSound;
            emitterSource.volume = footstepsSoundVolume;
            emitterSource.spatialBlend = 1f;
            emitterSource.Play();
        }

        float destroyDelay = footstepsSound.length;
        Destroy(currentFootstepEmitter, destroyDelay);

        // Asegurarse de limpiar la referencia después del sonido
        yield return new WaitForSeconds(destroyDelay);
        currentFootstepEmitter = null;
    }
}