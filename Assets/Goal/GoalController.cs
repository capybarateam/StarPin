using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GoalController : MonoBehaviour
{
    public float speedRatio = 0.1f;
    public float range = 1f;
    StarController target;
    bool emitted = false;
    public string NextStage;

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
                    GetComponent<ParticleSystem>().Play();
                    GameObject.Find("StageClear").GetComponent<Animator>().SetBool("Enabled", true);
                    target.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(e => e.Play());
                    Invoke("OnNextStage", 3);
                }
            }
        }
    }

    void OnNextStage()
    {
        GameObject.Find("GameDirector").GetComponent<GameDirector>().EndGame();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var star = collision.GetComponentInParent<StarController>();
        if (star)
        {
            star.enablegrip = false;
            star.DetachAll();
            star.GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<AudioSource>().Play();
            target = star;
        }
    }
}
