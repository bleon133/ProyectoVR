using UnityEngine;
using UnityEngine.SceneManagement;

public class JUGAR : MonoBehaviour
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
                SceneManager.LoadScene("Piso1Mike");
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
