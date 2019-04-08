using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConnectorController : MonoBehaviour
{
    public GameObject connectionA;
    public GameObject connectionB;

    public float depth;

    // Start is called before the first frame update
    void Start()
    {
        if (connectionA && connectionB)
        {
            connectionA.AddComponent<ConnectorJoint>().controller = this;
            connectionB.AddComponent<ConnectorJoint>().controller = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleAttached()
    {
        if (connectionA && connectionB)
            ConnectTo(connectionA, connectionB);
    }

    public void ConnectTo(GameObject connectionA, GameObject connectionB)
    {
        var lineRenderer = GetComponent<LineRenderer>();
        if (connectionA.GetComponent<PointController>().touched && connectionB.GetComponent<PointController>().touched)
        {
            var posA = connectionA.transform.position - transform.position;
            var posB = connectionB.transform.position - transform.position;

            posA.z = posB.z = depth;

            lineRenderer.SetPosition(0, posA);
            lineRenderer.SetPosition(1, posB);
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
