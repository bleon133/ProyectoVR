using UnityEngine;

public class BlendTreeController : MonoBehaviour
{
    [Header("Referencia al Animator")]
    [SerializeField] private Animator animator;

    [Header("Valor del parámetro 'Alternar'")]
    [Range(0f, 1f)]
    [SerializeField] private float alternarValue = 0f;

    void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Alternar", alternarValue);
        }
    }
}