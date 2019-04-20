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
    [HideInInspector]
    public GameObject currentJoint = null;
    float timer;

    public float hp;

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
            hp = Mathf.Clamp((float)manager.health / manager.maxHealth, 0, 1);
        else
            hp = 1;
    }

    public void DetachAll()
    {
        foreach (var grip in grips)
            grip.Detatch();
    }

    public void SetCurrentJoint(GripController grip, GameObject point)
    {
        if (((enablegrip && timer > 0.3f) || currentJoint == null) && currentJoint != point)
        {
            bool canceled = false;
            ExecuteEvents.Execute<IAttachable>(
                target: point,
                eventData: null,
                functor: (reciever, eventData) => reciever.CheckAttachable(this, ref canceled)
            );
            if (!canceled)
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
    }
}
