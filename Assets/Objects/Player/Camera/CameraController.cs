using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool mainCamera;
    static GameObject mainCameraObject;

    public Targetter Targetter
    {
        get
        {
            return GetComponent<Targetter>();
        }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (mainCamera)
            mainCameraObject = gameObject;
    }

    private void Start()
    {
        if (mainCameraObject != null && mainCameraObject != gameObject)
            Destroy(gameObject);
    }

    public static CameraController Get()
    {
        var cam = GameObject.Find("Main Camera");
        if (!cam)
            cam = GameObject.Find("Camera");
        return cam.GetComponent<CameraController>();
    }


}
