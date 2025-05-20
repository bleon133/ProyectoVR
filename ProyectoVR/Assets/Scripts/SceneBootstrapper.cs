using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapper : MonoBehaviour
{
    [SerializeField] private string firstScene = "Piso1";
    void Start() => SceneManager.LoadSceneAsync(firstScene, LoadSceneMode.Additive);
}
