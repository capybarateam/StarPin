using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GoalController : MonoBehaviour
{
    public float speedRatio = 0.1f;
    public float range = 1f;
    StarController target;
    bool emitted = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
                if (!emitted)
                {
                    emitted = true;

                    BaseDirector.Get()?.StageClearEffect(true);

                    GetComponentInChildren<ParticleSystem>().Play();
                    target.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(e => e.Play());

                    this.Delay(3, () => {
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
            star.GetComponentInChildren<Animator>().SetBool("Enabled", true);
            GetComponent<AudioSource>().Play();
            target = star;
        }
    }
}
