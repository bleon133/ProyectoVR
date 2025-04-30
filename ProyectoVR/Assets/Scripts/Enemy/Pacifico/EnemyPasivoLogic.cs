using UnityEngine;

public class EnemyPasivoLogic : MonoBehaviour
{
    public AudioClip pasivoClip;

    private void OnEnable()
    {
        AudioSource source = GetComponentInParent<AudioSource>();
        if (source != null && pasivoClip != null)
        {
            source.clip = pasivoClip;
            source.Play();
        }
    }
}