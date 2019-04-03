using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePoint : MonoBehaviour
{
    public bool pointActive;
    public GameObject connection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttached()
    {
        pointActive = true;
        gameObject.AddComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, connection.transform.position });
    }
}
