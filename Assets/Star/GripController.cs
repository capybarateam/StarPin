using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripController : MonoBehaviour
{
    StarController star;
    ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        star = GetComponentInParent<StarController>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            star.SetCurrentJoint(this, collision.gameObject);
        }
    }

    public void Detatch()
    {
        var hinge = GetComponent<HingeJoint2D>();
        if (hinge != null)
        {
            //Destroy(hinge.connectedBody.gameObject);
            Destroy(hinge);
        }
    }

    public void Attach(GameObject point)
    {
        var hinge = gameObject.AddComponent<HingeJoint2D>();
        hinge.connectedBody = point.GetComponent<Rigidbody2D>();
        hinge.autoConfigureConnectedAnchor = false;
        hinge.anchor = Vector2.zero;
        hinge.connectedAnchor = Vector2.zero;
    }

    public void EmitParticle()
    {
        particle.Play();
    }
}
