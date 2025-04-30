using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public void OnPointerEnterXR()
    {
    }

    public void OnPointerClickXR()
    {
        SpawnEnemigo();
    }

    public void OnPointerExitXR()
    {
    }

    private void SpawnEnemigo()
    {
        Vector3 spawnPosition = transform.position + transform.forward * 2f;
        spawnPosition.y = transform.position.y - 1.5f;

        GameObject enemy = EnemyPoolManager.Instance.GetEnemyFromPool();
        enemy.SetActive(false);

        enemy.transform.position = spawnPosition;

        Vector3 lookDirection = Camera.main.transform.position - enemy.transform.position;
        lookDirection.y = 0f;
        if (lookDirection != Vector3.zero)
            enemy.transform.rotation = Quaternion.LookRotation(lookDirection);

        EnemyBehavior behavior = enemy.GetComponent<EnemyBehavior>();
        if (behavior != null)
        {
            behavior.rol = EnemyRole.Pasivo;
        }

        Animator animator = enemy.GetComponent<Animator>();

        enemy.SetActive(true);

        if (animator != null)
        {
            animator.SetBool("Pasivo", true);
            animator.Update(0f);
            Debug.Log("Parámetro 'Pasivo' en Animator (verificado): " + animator.GetBool("Pasivo"));
        }
        else
        {
            Debug.LogWarning("No se encontró Animator en el clon activo.");
        }

        Debug.Log("Enemigo generado en modo PASIVO");
    }
}