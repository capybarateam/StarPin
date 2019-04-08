﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GoalController : MonoBehaviour
{
    public float speedRatio = 0.1f;
    public float range = 1f;
    StarController target;
    bool emitted = false;

    AudioSource audioSource;
    public float duration = 3;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            var pos = Vector2.Lerp(target.transform.position, transform.position, speedRatio);
            target.transform.position = new Vector3(pos.x, pos.y, transform.position.z);

            if (Vector2.Distance(target.transform.position, transform.position) < range)
            {
                audioSource.volume = Mathf.Clamp(audioSource.volume - Time.deltaTime / duration, 0, 1);

                if (!emitted)
                {
                    emitted = true;

                    BaseDirector.Get()?.StageClearEffect(true);

                    GetComponentInChildren<ParticleSystem>().Play();
                    target.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(e => e.Play());

                    this.Delay(duration, () =>
                    {
                        BaseDirector.Get()?.StageClearEffect(false);
                        GameDirector.Get(transform).EndGame();
                    });
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var star = collision.GetComponentInParent<StarController>();
        if (star)
        {
            star.enablegrip = false;
            star.DetachAll();
            star.GetComponent<Rigidbody2D>().simulated = false;
            star.currentJoint = gameObject;
            star.GetComponentInChildren<Animator>().SetBool("Enabled", true);
            audioSource.Play();
            target = star;
        }
    }
}