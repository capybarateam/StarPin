using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speedRatio = 0.1f;
    public float range = 1f;

    GameObject targetObj;
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
        targetObj = obj;
        target = obj?.GetComponent<StarController>() ?? null;
    }

    public Vector3? GetTargetPosition()
    {
        if (target)
            return target.currentJoint != null ? target.currentJoint.transform.position : target.transform.position;
        else if (targetObj)
            return targetObj.transform.position;
        else
            return null;
    }

    public void MoveImmediately()
    {
        var pos = GetTargetPosition();
        if (pos.HasValue)
            transform.position = new Vector3(pos.Value.x, pos.Value.y, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        var pos = GetTargetPosition();
        if (pos.HasValue)
        {
            if (Vector2.Distance(transform.position, pos.Value) > range)
            {
                Vector2 tpos = Vector2.Lerp(transform.position, pos.Value, speedRatio * 60 * Time.deltaTime);
                transform.position = new Vector3(tpos.x, tpos.y, transform.position.z);
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
