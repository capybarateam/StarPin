using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignObserver : MonoBehaviour
{
    public GameObject observee;
    Animator animator;
    ISelectDirector director;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        director = SelectDirector.Get();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Enabled", director.IsSelected(observee));
    }
}
