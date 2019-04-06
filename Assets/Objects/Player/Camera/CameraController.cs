using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedRatio = 0.1f;
    public float range = 1f;

    StarController target;

    public bool mainCamera;
    static GameObject mainCameraObject;

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

    public void SetTarget(GameObject obj)
    {
        target = obj.GetComponent<StarController>();
    }

    public void MoveImmediately()
    {
        if (target)
        {
            var pos = target.currentJoint != null ? target.currentJoint.transform.position : target.transform.position;

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (target != null && target.currentJoint != null)
        {
            if (Vector2.Distance(transform.position, target.currentJoint.transform.position) > range)
            {
                Vector2 pos = Vector2.Lerp(transform.position, target.currentJoint.transform.position, speedRatio * 60 * Time.deltaTime);
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            }
        }
    }

    public static CameraController Get()
    {
        var cam = GameObject.Find("Main Camera");
        if (!cam)
            cam = GameObject.Find("Camera");
        return cam.GetComponent<CameraController>();
    }
}
