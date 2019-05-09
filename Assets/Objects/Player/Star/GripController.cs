using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripController : MonoBehaviour
{
    StarController star;
    ParticleSystem particle;
    HashSet<GameObject> hittings;

    // Start is called before the first frame update
    void Start()
    {
        star = GetComponentInParent<StarController>();
        particle = GetComponentInChildren<ParticleSystem>();
        hittings = new HashSet<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            hittings.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            hittings.Remove(collision.gameObject);
        }
    }

    public void GetHittingObjects(ref HashSet<GameObject> objects)
    {
        foreach (var hitting in hittings)
            if (hitting != null)
                objects.Add(hitting);
    }

    public (GameObject, float)? GetNearestObject()
    {
        HashSet<GameObject> hittings = new HashSet<GameObject>();
        GetHittingObjects(ref hittings);

        (GameObject, float)? nearest = null;
        foreach (var hitting in hittings)
        {
            var length = (transform.position - hitting.transform.position).sqrMagnitude;
            if (!nearest.HasValue || nearest.Value.Item2 > length)
                nearest = (hitting, length);
        }

        return nearest;
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
