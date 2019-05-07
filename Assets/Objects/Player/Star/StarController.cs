using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class StarController : MonoBehaviour
{
    public static GameObject latestStar;

    List<GripController> grips;
    Rigidbody2D rigid;
    public float speed = 1;
    int vel = 1;
    public bool enablegrip;
    public GameObject currentJoint = null;
    float timer;

    public float hp;
    public int colorIndex;

    void Awake()
    {
        latestStar = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        grips = GetComponentsInChildren<GripController>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Grip"))
            enablegrip = true;
        if (Input.GetButtonUp("Grip"))
            enablegrip = false;
        rigid.angularVelocity = vel * speed;
        timer += Time.deltaTime;

        var manager = GameDirector.Get(transform)?.pointManager;
        if (manager != null)
        {
            hp = Mathf.Clamp((float)manager.health / manager.maxHealth, 0, 1);
            colorIndex = manager.colorIndex;
        }
        else
        {
            hp = 1;
            colorIndex = 0;
        }
    }

    public void DetachAll()
    {
        foreach (var grip in grips)
            DetachFromJoint(grip);
    }

    public void DetachFromJoint(GripController grip)
    {
        var hinge = grip.GetComponent<HingeJoint2D>();
        if (hinge != null)
        {
            ExecuteEvents.Execute<IDetachable>(
                target: hinge.connectedBody.gameObject,
                eventData: null,
                functor: (reciever, eventData) => reciever.OnDetached(this)
            );
        }
        grip.Detatch();
    }

    public void SetCurrentJoint(GripController grip, GameObject point, bool force = false)
    {
        if (((enablegrip && timer > 0.3f) || currentJoint == null) && currentJoint != point)
        {
            AttachToJoint(grip, point, force);
        }
    }

    public void AttachToJoint(GripController grip, GameObject point, bool force = false)
    {
        bool canceled = false;
        ExecuteEvents.Execute<IAttachable>(
            target: point,
            eventData: null,
            functor: (reciever, eventData) => reciever.CheckAttachable(this, ref canceled)
        );
        if (force || !canceled)
        {
            GetComponent<AudioSource>().Play();
            DetachAll();
            grip.Attach(point);
            //enablegrip = false;
            timer = 0;
            grip.EmitParticle();
            currentJoint = point;

            ExecuteEvents.Execute<IAttachable>(
                target: point,
                eventData: null,
                functor: (reciever, eventData) => reciever.OnAttached(this)
            );
        }
    }

    public void AttachToJoint(GameObject point, bool force = false)
    {
        AttachToJoint(grips[0], point, force);
    }
}
