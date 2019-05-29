using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class GameDirector : MonoBehaviour
{
    public PointManager pointManager;

    private float showWorldTime = 3.0f;
    private float showGoalTime = 2.0f;

    private enum CameraType
    {
        ShowWorld,
        ShowGoal,
        ShowPlay
    }

    private CameraType camType;

    private float countTime = 0.0f;

    bool achieveImportant;

    // Start is called before the first frame update
    private void Start()
    {
        var name = SceneSelector.GetCurrentSceneName();
        if (name != null)
        {
            var music = MusicController.Get();
            if (music != null)
            {
                int bgmId = name.GetHashCode() % music.PG.Length;
                var selector = SceneSelector.Get();
                if (selector != null && selector.CurrentScene is Stage)
                {
                    var id = ((Stage)selector.CurrentScene).bgmId;
                    if (id >= 0)
                        bgmId = id;
                };

                music.ChangeSound(music.PG[bgmId]);
            }
        }

        pointManager = GetComponent<PointManager>();
        camType = CameraType.ShowWorld;

        this.Delay(.1f, () =>
        {
            CameraController.Get().Targetter.SetTarget(GetComponentInChildren<GoalController>().goalTarget);
        });
        StartGame();
    }

    // Update is called once per frame
    private void Update()
    {
        float per = (float)pointManager.allPoints.Count(e => e.rawTouched) / pointManager.allPoints.Count;
        MusicController.Get()?.ApplyParamater("Scene", per);

        if (!achieveImportant && pointManager.IsGotAllImportantPoints())
        {
            achieveImportant = true;
            StartCoroutine(AchieveEffect());
        }

        if (camType.Equals(CameraType.ShowWorld))
        {
            countTime += Time.deltaTime;
            if (countTime >= showWorldTime)
            {
                CameraController.Get().Targetter.SetTarget(GoalController.latestGoal);
                camType = CameraType.ShowGoal;
                countTime = 0.0f;
            }
        }
        else if (camType.Equals(CameraType.ShowGoal))
        {
            countTime += Time.deltaTime;
            if (countTime >= showGoalTime)
            {
                CameraController.Get().Targetter.SetTarget(StarController.latestStar);
                camType = CameraType.ShowPlay;
                countTime = 0.0f;
            }
        }
    }

    IEnumerator AchieveEffect()
    {
        CameraController.Get().Targetter.SetTarget(GetComponentInChildren<GoalController>().goalTarget);
        yield return new WaitForSeconds(showWorldTime / 2);
        var prefabEffect = GetComponentInChildren<GoalController>().GetComponentInChildren<ParticleSystem>();
        foreach (var point in pointManager.allImportantPoints)
        {
            var p = Instantiate(prefabEffect, point.transform);
            p.Play();
        }
        yield return new WaitForSeconds(showWorldTime);
        CameraController.Get().Targetter.SetTarget(StarController.latestStar);
    }

    public void StartGame()
    {
    }

    public void EndGame()
    {
        StageSelector.Get().LoadNextStage();
    }

    public static GameDirector Get(Transform t)
    {
        return t.GetComponentInParent<GameDirector>();
    }
}
