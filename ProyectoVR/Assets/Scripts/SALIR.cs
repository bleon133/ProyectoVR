using UnityEngine;

public class SALIR : MonoBehaviour
{
    private bool isGazing = false;
    private float gazeDuration = 2f;
    private float timer = 0f;

    void Update()
    {
        if (isGazing)
        {
            timer += Time.deltaTime;
            if (timer >= gazeDuration)
            {
                Application.Quit();
                Debug.Log("La aplicación se ha cerrado (esto solo se ve en la build, no en el editor).");
                isGazing = false;
                timer = 0f;
            }
        }
    }

    public void StartGaze()
    {
        isGazing = true;
        timer = 0f;
    }

    public void EndGaze()
    {
        isGazing = false;
        timer = 0f;
    }
}

