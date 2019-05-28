using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GoalController : MonoBehaviour
{
    public static GameObject latestGoal;

    public GameObject goalTarget;
    public GameObject goalSprite;

    public float speedRatio = 0.1f;
    public float range = 1f;
    StarController target;
    bool emitted = false;

    AudioSource audioSource;
    public float durationA = 2;
    public float durationB = 1;
    public float durationC = 3;

    // Start is called before the first frame update
    void Start()
    {
        latestGoal = gameObject;

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
                audioSource.volume = Mathf.Clamp(audioSource.volume - Time.deltaTime / (durationA + durationB), 0, 1);

                if (!emitted)
                {
                    emitted = true;

                    var pointManager = GameDirector.Get(transform)?.pointManager;
                    int level = 1;
                    if (pointManager?.IsGotAllImportantPoints() ?? false)
                    {
                        level++;
                        if (pointManager?.IsGotAllPoints() ?? false)
                            level++;
                    }
                    if (SceneSelector.Get() != null)
                        StageAchievement.SetCleared(SceneSelector.Get().CurrentScene, level);

                    StageDirector.Get()?.StageClearEffect(true, level);

                    // エフェクトを出す
                    GetComponentInChildren<ParticleSystem>().Play();
                    //target.GetComponentsInChildren<ParticleSystem>().ToList().ForEach(e => e.Play());

                    this.Delay(durationA, () =>
                    {
                        CameraController.Get().Targetter.SetTarget(goalTarget);

                        var achieved = pointManager?.IsGotAllImportantPoints() ?? false;
                        if (achieved)
                            this.Delay(durationB, () =>
                            {
                                StageDirector.Get()?.StageAchieveEffect(true);
                                if (goalSprite)
                                    goalSprite.GetComponent<Animator>().SetBool("Enabled", true);
                            });
                        this.Delay(durationC + (achieved ? durationB : 0), () =>
                        {
                            StageDirector.Get()?.StageClearEffect(false, level);
                            StageDirector.Get()?.StageAchieveEffect(false);
                            GameDirector.Get(transform).EndGame();
                        });
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
            //star.enablegrip = false;
            star.GetComponent<Rigidbody2D>().simulated = false;
            //star.AttachToJoint(gameObject);
            star.DetachAll();
            star.GetComponentInChildren<Animator>().SetBool("Enabled", true);
            var manager = GameDirector.Get(transform)?.pointManager;
            if (manager != null)
                manager.health = manager.maxHealth;
            audioSource.Play();
            target = star;
        }
    }
}
