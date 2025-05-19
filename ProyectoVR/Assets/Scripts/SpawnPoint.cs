using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Tooltip("Nombre único que usarán los teleports para referirse a este punto")]
    public string spawnId = "default";

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + Vector3.up * 0.1f, 0.2f);
        // Flecha que indica la orientación
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * 0.5f);
    }
}
