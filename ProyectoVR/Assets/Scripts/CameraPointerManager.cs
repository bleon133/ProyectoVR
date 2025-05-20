using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraPointerManager : MonoBehaviour
{
    public static CameraPointerManager Instance;

    [SerializeField] private GameObject pointer;           // ← se reasignará
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0, 1)]
    [SerializeField] private float disPointerObject = 0.95f;

    private const float _maxDistance = 10;
    private GameObject _gazedAtObject;
    private readonly string interactableTag = "Interactable";
    private float scaleSize = 0.025f;

    [HideInInspector] public Vector3 hitPoint;

    // ────────────────────────────────────────────────
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);     // destruye el duplicado *completo*, no sólo el componente
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);   // el manager vive entre escenas
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        // 1) Busca de nuevo el pointer si ya no es válido
        if (pointer == null || !pointer)          // el operador ! detecta “zombies”
            pointer = GameObject.FindWithTag("Pointer"); // o búscalo por nombre

        // 2) Limpia cualquier referencia a objetos destruidos
        _gazedAtObject = null;
    }

    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
    }

    private void GazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
    }

    public void Update()
    {
        // Si el pointer no existe aún, espera a que la escena lo cree
        if (pointer == null || !pointer) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            hitPoint = hit.point;

            // Detectar cambio de objeto
            if (_gazedAtObject != hit.transform.gameObject)
            {
                if (_gazedAtObject)         // evita MissingReference
                    _gazedAtObject.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);

                _gazedAtObject = hit.transform.gameObject;

                if (_gazedAtObject)         // por si el objeto se destruye entre frames
                    _gazedAtObject.SendMessage("OnPointerEnterXR", null, SendMessageOptions.DontRequireReceiver);

                GazeManager.Instance.StartGazeSelection();
            }

            PointerOnGaze(hit.point);                   // siempre actualiza el puntero
            if (!hit.transform.CompareTag(interactableTag))
                GazeManager.Instance.CancelGazeSelection();
        }
        else
        {
            if (_gazedAtObject)
                _gazedAtObject.SendMessage("OnPointerExitXR", null, SendMessageOptions.DontRequireReceiver);

            _gazedAtObject = null;
            PointerOutGaze();
        }

        // Trigger físico Cardboard
        if (Google.XR.Cardboard.Api.IsTriggerPressed && _gazedAtObject)
            _gazedAtObject.SendMessage("OnPointerClickXR", null, SendMessageOptions.DontRequireReceiver);
    }


    private void PointerOnGaze(Vector3 hitPoint)
    {
        if (!pointer) return;                            // protección extra
        float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        pointer.transform.parent.position =
            CalculatePointerPosition(transform.position, hitPoint, disPointerObject);
    }


    private void PointerOutGaze()
    {
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
        GazeManager.Instance.CancelGazeSelection();
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        float x = p0.x + t * (p1.x - p0.x); 
        float y = p0.y + t * (p1.y - p0.y); 
        float z = p0.z + t * (p1.z - p0.z); 

        return new Vector3(x, y, z);
    }
}
