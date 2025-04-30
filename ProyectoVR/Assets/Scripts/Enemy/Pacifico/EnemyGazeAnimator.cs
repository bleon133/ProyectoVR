using UnityEngine;

public class EnemyGazeAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void OnPointerEnterXR()
    {
        if (animator != null)
        {
            animator.SetBool("Mirando", true);
            Debug.Log("El enemigo está siendo mirado");
        }
    }

    public void OnPointerExitXR()
    {
        if (animator != null)
        {
            animator.SetBool("Mirando", false);
            Debug.Log("El enemigo dejó de ser mirado");
        }
    }
}
