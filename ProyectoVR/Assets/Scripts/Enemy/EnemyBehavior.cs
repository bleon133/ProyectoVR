using UnityEngine;

public enum EnemyRole { Pasivo, Hostil}

public class EnemyBehavior : MonoBehaviour
{
    public EnemyRole rol;

    [Header("Modos del enemigo")]
    public GameObject pasivoObj;
    public GameObject hostilObj;

    private void OnEnable()
    {
        ActivarSolo(rol);
    }

    private void ActivarSolo(EnemyRole rolActivo)
    {
        // Desactiva todos
        pasivoObj?.SetActive(false);
        hostilObj?.SetActive(false);

        // Activa solo el correspondiente
        switch (rolActivo)
        {
            case EnemyRole.Pasivo:
                pasivoObj?.SetActive(true);
                break;
            case EnemyRole.Hostil:
                hostilObj?.SetActive(true);
                break;
        }
    }
}
